using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.ViewModels;
using MupenUtilitiesRedux.Views.WPF.Bindings;
using MupenUtilitiesRedux.Views.WPF.Services;

namespace MupenUtilitiesRedux.Views.WPF;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IDialogService, IDispatcherService, ILocalizationService
{
    public static MainViewModel MainViewModel { get; private set; }
    public static SettingsViewModel SettingsViewModel { get; private set; }
    internal LocalSettings LocalSettings { get; }


    public MainWindow()
    {
        InitializeComponent();

        var filesService = new FilesService();
        var timerService = new TimerService(Dispatcher);

        try
        {
            LocalSettings = LocalSettings.FromJson(File.ReadAllText("localsettings.json"));
        }
        catch
        {
            Debug.WriteLine("Failed to load settings, falling back to defaults...");
            LocalSettings = LocalSettings.Default;
        }

        SettingsViewModel = new SettingsViewModel(LocalSettings);

        LocalSettings.OnSettingChanged += delegate(object? sender, string key)
        {
            if (key == nameof(SettingsViewModel.Theme))
            {
                if (SettingsViewModel.Theme.Equals("Light", StringComparison.InvariantCultureIgnoreCase))
                    ;
                else if (SettingsViewModel.Theme.Equals("Dark", StringComparison.InvariantCultureIgnoreCase))
                    ; // TODO: implement themes
            }

            if (key == nameof(SettingsViewModel.Culture))
                (this as IDispatcherService).Execute(delegate
                {
                    var culture = CultureInfo.GetCultureInfo(SettingsViewModel.Culture);
                    Thread.CurrentThread.CurrentCulture =
                        Thread.CurrentThread.CurrentUICulture =
                            LocalizationSource.Instance.CurrentCulture = culture;
                });
        };

        LocalSettings.InvokeOnSettingChangedForAllKeys();

        MainViewModel = new MainViewModel(filesService, this, timerService, this);

        DataContext = this;
    }


    public void ShowError(string content)
    {
        MessageBox.Show(content, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void Execute(Action action)
    {
        Dispatcher.Invoke(action);
    }

    public string? GetStringOrDefault(string key, string? @default = null)
    {
        try
        {
            return LocalizationSource.Instance[key] ?? @default;
        }
        catch
        {
            return @default;
        }
    }
}