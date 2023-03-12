using System.Collections.ObjectModel;
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
    private readonly ILocalizationService _localizationService;

    private readonly IMovieSerializer _movieSerializer;
    private readonly ITimerService _timerService;

    public MainViewModel(IFilesService filesService,
        IDialogService dialogService, ITimerService timerService, ILocalizationService localizationService)
    {
        _filesService = filesService;
        _dialogService = dialogService;
        _timerService = timerService;
        _localizationService = localizationService;

        _movieSerializer = new ReflectionMovieSerializer();
    }

    public ObservableCollection<MovieViewModel> OpenMovieViewModels { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMovieSelected))]
    [NotifyCanExecuteChangedFor(nameof(SaveAsCommand))]
    private MovieViewModel? _selectedMovieViewModel;

    public bool IsMovieSelected => _selectedMovieViewModel != null;


    [RelayCommand]
    private async Task Open()
    {
        var file = await _filesService.TryPickOpenFileAsync(new[] { "m64" });

        if (file == null) return;

        var bytes = await file.ReadAllBytes();

        var movie = _movieSerializer.Deserialize(bytes,
            new MovieDeserializationOptions { SimplifyNullTerminators = true });

        var movieViewModel = new MovieViewModel(movie, _timerService, Path.GetFileNameWithoutExtension(file.Path));
        OpenMovieViewModels.Add(movieViewModel);
        SelectedMovieViewModel = movieViewModel;
    }

    [RelayCommand(CanExecute = nameof(IsMovieSelected))]
    private async Task SaveAs()
    {
        var file = await _filesService.TryPickSaveFileAsync("movie", ("Movie", new[] { "m64" }));

        if (file == null) return;

        var bytes = _movieSerializer.Serialize(_selectedMovieViewModel.Movie);

        await using var stream = await file.OpenStreamForWriteAsync();

        stream.Write(bytes);
    }
}