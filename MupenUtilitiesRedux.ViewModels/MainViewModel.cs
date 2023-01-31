using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MupenUtilitiesRedux.Models;
using MupenUtilitiesRedux.Models.Interfaces;
using MupenUtilitiesRedux.Models.Options;
using MupenUtilitiesRedux.Models.Serializers;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;
using MupenUtilitiesRedux.ViewModels.Localization;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
///     A <see langword="class" /> which represents the application's root ViewModel
/// </summary>
public partial class MainViewModel : ObservableObject
{
	private readonly IDialogService _dialogService;
	private readonly IFilesService _filesService;
	private readonly LocalizationManagerViewModel _localizationManagerViewModel;
	private readonly ITimerService _timerService;

	private IMovieSerializer _movieSerializer;
	
	public MainViewModel(LocalizationManagerViewModel localizationManagerViewModel, IFilesService filesService,
		IDialogService dialogService, ITimerService timerService)
	{
		_filesService = filesService;
		_dialogService = dialogService;
		_timerService = timerService;
		_localizationManagerViewModel = localizationManagerViewModel;
		_movieSerializer = new ReflectionMovieSerializer();
	}

	public MovieViewModel? CurrentMovie { get; private set; }
	public bool IsMovieLoaded => CurrentMovie != null;

	[RelayCommand]
	private async void LoadMovie()
	{
		var file = await _filesService.TryPickOpenFileAsync(new[] { "m64" });

		if (file == null) return;

		var bytes = await file.ReadAllBytes();

		if (bytes == null) _dialogService.ShowError(_localizationManagerViewModel.LocalizationData.FileReadFailure);

		var movie = _movieSerializer.Deserialize(bytes,
			new MovieDeserializationOptions { SimplifyNullTerminators = true });

		CurrentMovie = new MovieViewModel(movie, _timerService);
		OnPropertyChanged(nameof(CurrentMovie));
		OnPropertyChanged(nameof(IsMovieLoaded));
		SaveMovieCommand.NotifyCanExecuteChanged();
	}

	[RelayCommand(CanExecute = nameof(IsMovieLoaded))]
	private async void SaveMovie()
	{
		var file = await _filesService.TryPickSaveFileAsync("movie", ("Movie", new[] { "m64" }));

		if (file == null) return;

		var bytes = _movieSerializer.Serialize(CurrentMovie.Movie);

		await using var stream = await file.OpenStreamForWriteAsync();

		stream.Write(bytes);
	}
}