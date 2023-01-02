using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.ViewModels;
using MupenUtilitiesRedux.ViewModels.Localization;
using MupenUtilitiesRedux.Views.Avalonia.Services;

namespace MupenUtilitiesRedux.Views.Avalonia;

public partial class MainWindow : Window, IDialogService
{
	public MainWindow()
	{
		Instance = this;

		InitializeComponent();

		var filesService = new FilesService();
		LocalizationManagerViewModel = new LocalizationManagerViewModel(filesService);


		//MainViewModel = new MainViewModel(LocalizationManagerViewModel, filesService, this);

		DataContext = this;
	}

	public static MainWindow Instance { get; private set; }

	public MainViewModel MainViewModel { get; }
	public LocalizationManagerViewModel LocalizationManagerViewModel { get; }

	public void ShowError(string content)
	{
		throw new NotImplementedException();
	}

	private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
	{
		await LocalizationManagerViewModel.Load();
	}
}