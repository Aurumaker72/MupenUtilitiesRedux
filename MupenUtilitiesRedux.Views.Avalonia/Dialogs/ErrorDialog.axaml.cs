using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MupenUtilitiesRedux.Views.Avalonia.Dialogs;

public partial class ErrorDialog : Window
{
    public string Text { get; init; }

    public ErrorDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        DataContext = this;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}