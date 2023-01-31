using CommunityToolkit.Mvvm.ComponentModel;
using MupenUtilitiesRedux.Models;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
///     A <see langword="class" /> which represents a ViewModel for a <see cref="Controller" />
/// </summary>
public class ControllerViewModel : ObservableObject
{
    private readonly Controller _controller;

    public ControllerViewModel(Controller controller)
    {
        _controller = controller;

        if (controller.Samples == null) return;

        SampleViewModels = new List<SampleViewModel>();

        foreach (var t in controller.Samples)
            SampleViewModels.Add(new SampleViewModel(t));
    }

    /// <summary>
    ///     Whether <see langword="this" /> is present
    /// </summary>
    public bool IsPresent
    {
        get => _controller.IsPresent;
        set
        {
            _controller.IsPresent = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Whether <see langword="this" /> has a mempak attached
    /// </summary>
    public bool IsMempakAttached
    {
        get => _controller.IsMempakAttached;
        set
        {
            _controller.IsMempakAttached = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     Whether <see langword="this" /> has a rumblepak attached
    /// </summary>
    public bool IsRumblepakAttached
    {
        get => _controller.IsRumblepakAttached;
        set
        {
            _controller.IsRumblepakAttached = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    ///     This controller's input sequence, or <see langword="null" /> by default if <see cref="IsPresent" /> is
    ///     <see langword="false" />
    /// </summary>
    public List<SampleViewModel>? SampleViewModels { get; set; }
}