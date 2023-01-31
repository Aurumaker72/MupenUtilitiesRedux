using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MupenUtilitiesRedux.Models.Interfaces;
using MupenUtilitiesRedux.Models.Options;
using MupenUtilitiesRedux.Models.Serializers;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
///     A <see langword="class" /> which represents the application's root ViewModel
/// </summary>
public partial class MainViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IFilesService _filesService;
    private readonly ITimerService _timerService;
    private readonly ILocalizationService _localizationService;

    private readonly IMovieSerializer _movieSerializer;

    public MainViewModel(IFilesService filesService,
        IDialogService dialogService, ITimerService timerService, ILocalizationService localizationService)
    {
        _filesService = filesService;
        _dialogService = dialogService;
        _timerService = timerService;
        _localizationService = localizationService;

        _movieSerializer = new ReflectionMovieSerializer();
    }

    public MovieViewModel? CurrentMovie { get; private set; }
    public bool IsMovieLoaded => CurrentMovie != null;

    [RelayCommand]
    private async Task LoadMovie()
    {
        var file = await _filesService.TryPickOpenFileAsync(new[] { "m64" });

        if (file == null) return;

        var bytes = await file.ReadAllBytes();

        _dialogService.ShowError(_localizationService.GetStringOrDefault("Movie load failed"));

        var movie = _movieSerializer.Deserialize(bytes,
            new MovieDeserializationOptions { SimplifyNullTerminators = true });

        CurrentMovie = new MovieViewModel(movie, _timerService);
        OnPropertyChanged(nameof(CurrentMovie));
        OnPropertyChanged(nameof(IsMovieLoaded));
        SaveMovieCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(IsMovieLoaded))]
    private async Task SaveMovie()
    {
        var file = await _filesService.TryPickSaveFileAsync("movie", ("Movie", new[] { "m64" }));

        if (file == null) return;

        var bytes = _movieSerializer.Serialize(CurrentMovie.Movie);

        await using var stream = await file.OpenStreamForWriteAsync();

        stream.Write(bytes);
    }

   
}