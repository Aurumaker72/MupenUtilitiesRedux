﻿using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using MupenUtilitiesRedux.Services;

namespace MupenUtilitiesRedux.ViewModels;

public sealed class SettingsViewModel : ObservableObject
{
    private readonly ILocalSettingsService _localSettingsService;


    public SettingsViewModel(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public List<string> RecentMoviePaths
    {
        get => _localSettingsService.Get<List<string>>(nameof(RecentMoviePaths));
        set => SetSettingsProperty(nameof(RecentMoviePaths), value);
    }

    public string Culture
    {
        get => _localSettingsService.Get<string>(nameof(Culture));
        set => SetSettingsProperty(nameof(Culture), value);
    }

    public string Theme
    {
        get => _localSettingsService.Get<string>(nameof(Theme));
        set => SetSettingsProperty(nameof(Theme), value);
    }

    private void SetSettingsProperty<T>(string key, T value, [CallerMemberName] string? callerMemberName = null)
    {
        ArgumentNullException.ThrowIfNull(callerMemberName);
        _localSettingsService.Set(key, value);
        OnPropertyChanged(callerMemberName);
    }
}