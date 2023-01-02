using System.Windows;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.ViewModels;
using MupenUtilitiesRedux.ViewModels.Localization;
using MupenUtilitiesRedux.Views.WPF.Services;

namespace MupenUtilitiesRedux.Views.WPF;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IDialogService
{
	public MainWindow()
	{
		Instance = this;

		InitializeComponent();

		var filesService = new FilesService();
		var timerService = new TimerService(Dispatcher);
		LocalizationManagerViewModel = new LocalizationManagerViewModel(filesService);


		MainViewModel = new MainViewModel(LocalizationManagerViewModel, filesService, this, timerService);

		DataContext = this;
	}

	public static MainWindow Instance { get; private set; }

	public MainViewModel MainViewModel { get; }
	public LocalizationManagerViewModel LocalizationManagerViewModel { get; }

	public void ShowError(string content)
	{
		MessageBox.Show(content, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	}

	private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
	{
		await LocalizationManagerViewModel.Load();
	}
}