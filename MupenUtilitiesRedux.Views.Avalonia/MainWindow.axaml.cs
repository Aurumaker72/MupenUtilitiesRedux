using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.ViewModels;
using MupenUtilitiesRedux.Views.Avalonia.Dialogs;
using MupenUtilitiesRedux.Views.Avalonia.Services;

namespace MupenUtilitiesRedux.Views.Avalonia;

public partial class MainWindow : Window,  IDialogService, ILocalizationService
{
    public static Window Window { private set; get; }

    public MainViewModel MainViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        Window = this;
        MainViewModel = new(new FilesService(), this, new TimerService(Dispatcher.UIThread), this);
        DataContext = this;
    }

    void IDialogService.ShowError(string content)
    {
        new ErrorDialog()
        {
            Text = content
        }.ShowDialog(this);
    }

    string? ILocalizationService.GetStringOrDefault(string key, string? @default)
    {
        return "unknown";
    }

    private void ExitMenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}