using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
///     A <see langword="class" /> which represents a ViewModel for scrubbing through a <see cref="MovieViewModel"/>'s samples
/// </summary>
public partial class SampleScrubberViewModel : ObservableObject
{
    private readonly MovieViewModel _movieViewModel;
    
    // TODO: implement controller switching
    
    private int _selectedSampleIndex;

    public int SelectedSampleIndex
    {
        get => _selectedSampleIndex;
        set
        {
            _selectedSampleIndex = Math.Clamp(value, 0, _movieViewModel.ControllerViewModels[0].SampleViewModels.Count - 1);
            OnPropertyChanged();
            OnPropertyChanged(nameof(SelectedSampleViewModel));
        }
    }

    public SampleViewModel SelectedSampleViewModel => _movieViewModel.ControllerViewModels[0].SampleViewModels[SelectedSampleIndex];
    

    public SampleScrubberViewModel(MovieViewModel movieViewModel)
    {
        _movieViewModel = movieViewModel;
        SelectedSampleIndex = 0;
    }

    [RelayCommand]
    private void IncrementSampleIndex(int i)
    {
        SelectedSampleIndex += i;
    }
}