﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using MupenUtilitiesRedux.Services;

namespace MupenUtilitiesRedux.Views.Avalonia.Services;

internal sealed class LocalSettings : ILocalSettingsService
{
    public static LocalSettings Default = new()
    {
        _settings = new Dictionary<string, object>
        {
            { "RecentMoviePaths", new List<string>() },
            { "Culture", "en-US" },
            { "Theme", "Light" }
        }
    };

    private Dictionary<string, object> _settings = new();

    private LocalSettings()
    {
    }

    public event EventHandler<string>? OnSettingChanged;

    public T Get<T>(string key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));

        if (!_settings.TryGetValue(key, out var value))
            throw new ArgumentException($"Attempted to retrieve nonexistent key \"{key}\"");

        if (value is T valueAsT)
            return valueAsT;

        throw new ArgumentException($"Requested type was {typeof(T)}, but got {value.GetType()}");
    }

    public void Set<T>(string key, T value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));

        _settings[key] = value;
        OnSettingChanged?.Invoke(this, key);
    }

    public void InvokeOnSettingChangedForAllKeys()
    {
        foreach (var keyValuePair in _settings) OnSettingChanged?.Invoke(this, keyValuePair.Key);
    }

    public static LocalSettings FromJson(string json)
    {
        var settings = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        ArgumentNullException.ThrowIfNull(settings);

        // all values on `settings` are JSONElements because their type is unknown
        // we need to cast and finish those up before returning

        // prune all keys which dont exist anymore
        foreach (var pair in settings.Where(pair => !Default._settings.ContainsKey(pair.Key)))
        {
            settings.Remove(pair.Key);
            Debug.WriteLine($"Pruned removed key \"{pair.Key}\"");
        }


        // backwards-compatibility:
        // if the internal default dictionary has pairs (settings) which dont exist in the (possibly older) settings file,
        // create them and give them the default value
        foreach (var defaultPair in Default._settings)
            if (!settings.TryGetValue(defaultPair.Key, out var value))
            {
                settings[defaultPair.Key] = defaultPair.Value;
                Debug.WriteLine($"Merged new key \"{defaultPair.Key}\"");
            }

        // cast everything to the correlated type
        foreach (var pair in settings)
        {
            var intendedType = Default._settings[pair.Key].GetType();
            dynamic newValue = ((JsonElement)pair.Value).Deserialize(intendedType);
            Convert.ChangeType(newValue, intendedType);
            settings[pair.Key] = newValue;
        }

        return new LocalSettings
        {
            _settings = settings
        };
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(_settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}