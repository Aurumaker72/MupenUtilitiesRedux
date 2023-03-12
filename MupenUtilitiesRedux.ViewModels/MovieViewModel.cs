using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MupenUtilitiesRedux.Models;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
///     A <see langword="class" /> which represents a ViewModel for a <see cref="Models.Movie" />
/// </summary>
public partial class MovieViewModel : ObservableObject
{
    private readonly ControllerViewModel[] _controllerViewModels = new ControllerViewModel[Movie.MaxControllers];
    private readonly ITimer _timer;
    private readonly ITimerService _timerService;
    internal readonly Movie Movie;
    
    private int _controllerIndex;

    private int _sampleIndex;

    internal MovieViewModel(Movie movie, ITimerService timerService, string friendlyName)
    {
        Movie = movie;
        _timerService = timerService;
        FriendlyName = friendlyName;

        for (var i = 0; i < movie.ReadOnlyControllers.Count; i++)
            _controllerViewModels[i] = new ControllerViewModel(movie.ReadOnlyControllers[i]);

        _timer = _timerService.Create(TimeSpan.FromMilliseconds(1000d / 30d), () => { SampleIndex++; });
    }

    public string FriendlyName { get; }
    
    public int ControllerIndex
    {
        get => _controllerIndex;
        set
        {
            SetProperty(ref _controllerIndex, Math.Clamp(value, 0, ControllerViewModels.Count));
            OnPropertyChanged(nameof(SelectedControllerViewModel));
            OnPropertyChanged(nameof(SampleIndex));
            OnPropertyChanged(nameof(SelectedSampleViewModel));
        }
    }

    public ControllerViewModel? SelectedControllerViewModel => ControllerViewModels[ControllerIndex];

    public int SampleIndex
    {
        get => _sampleIndex;
        set
        {
            if (SelectedControllerViewModel?.SampleViewModels == null) return;
            SetProperty(ref _sampleIndex, Math.Clamp(value, 0, SelectedControllerViewModel.SampleViewModels.Count));
            OnPropertyChanged(nameof(SelectedSampleViewModel));
        }
    }

    public SampleViewModel? SelectedSampleViewModel
    {
        get
        {
            if (SelectedControllerViewModel?.SampleViewModels == null) return null;

            return SampleIndex >= SelectedControllerViewModel.SampleViewModels.Count
                ? null
                : SelectedControllerViewModel.SampleViewModels[SampleIndex];
        }
    }


    /// <summary>
    ///     The magic cookie value
    ///     <para>Expected value: <c>0x4D36341A</c></para>
    /// </summary>
    public uint Magic
    {
        get => Movie.Magic;
        set
        {
            Movie.Magic = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The version value
    ///     <para>Expected value: <c>3</c>, a lower value indicates an outdated movie</para>
    /// </summary>
    public uint Version
    {
        get => Movie.Version;
        set
        {
            Movie.Version = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The unique identifier in Unix epoch format
    /// </summary>
    public uint Uid
    {
        get => Movie.Uid;
        set
        {
            Movie.Uid = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The amount of visual interrupts
    /// </summary>
    public uint VisualInterrupts
    {
        get => Movie.VisualInterrupts;
        set
        {
            Movie.VisualInterrupts = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The amount of rerecords
    /// </summary>
    public uint Rerecords
    {
        get => Movie.Rerecords;
        set
        {
            Movie.VisualInterrupts = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The amount of frames per second the rom region specifies
    ///     <para>
    ///         Expected values:
    ///         <list type="bullet">
    ///             <item>30, for PAL region roms</item>
    ///             <item>60, for NTSC/J region roms</item>
    ///         </list>
    ///         Any other value is invalid
    ///     </para>
    /// </summary>
    public byte FramesPerSecond
    {
        get => Movie.FramesPerSecond;
        set
        {
            Movie.VisualInterrupts = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The amount of logical frames
    /// </summary>
    public uint Frames => Movie.Frames;

    /// <summary>
    ///     The start type
    ///     <para>
    ///         Expected values:
    ///         <list type="table">
    ///             <listheader>
    ///                 <term>Value</term>
    ///                 <description>Create Type</description>
    ///             </listheader>
    ///             <item>
    ///                 <term>1</term>
    ///                 <description>Snapshot</description>
    ///             </item>
    ///             <item>
    ///                 <term>2</term>
    ///                 <description>Create</description>
    ///             </item>
    ///             <item>
    ///                 <term>4</term>
    ///                 <description>EEPROM</description>
    ///             </item>
    ///         </list>
    ///         Any other value is invalid
    ///     </para>
    /// </summary>
    public ushort StartType
    {
        get => Movie.StartType;
        set
        {
            Movie.VisualInterrupts = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The rom's name
    /// </summary>
    public string? RomName
    {
        get => Movie.RomName;
        set
        {
            Movie.RomName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The rom's crc32
    /// </summary>
    public uint RomCrc32
    {
        get => Movie.RomCrc32;
        set
        {
            Movie.RomCrc32 = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The rom's country code
    /// </summary>
    public ushort CountryCode
    {
        get => Movie.CountryCode;
        set
        {
            Movie.CountryCode = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The video plugin's name
    /// </summary>
    public string? VideoPluginName
    {
        get => Movie.VideoPluginName;
        set
        {
            Movie.VideoPluginName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The audio plugin's name
    /// </summary>
    public string? AudioPluginName
    {
        get => Movie.AudioPluginName;
        set
        {
            Movie.AudioPluginName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The input plugin's name
    /// </summary>
    public string? InputPluginName
    {
        get => Movie.InputPluginName;
        set
        {
            Movie.InputPluginName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The rsp plugin's name
    /// </summary>
    public string? RspPluginName
    {
        get => Movie.RspPluginName;
        set
        {
            Movie.RspPluginName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The author
    /// </summary>
    public string? Author
    {
        get => Movie.Author;
        set
        {
            Movie.Author = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The description
    /// </summary>
    public string? Description
    {
        get => Movie.Description;
        set
        {
            Movie.Description = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     The logical controllers, as provided by the input plugin
    /// </summary>
    public IReadOnlyList<ControllerViewModel> ControllerViewModels => _controllerViewModels;


    [RelayCommand]
    private void ToggleTimer()
    {
        _timer.IsResumed ^= true;
    }
}