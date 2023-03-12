using Avalonia.Threading;
using MupenUtilitiesRedux.Services.Abstractions;

namespace MupenUtilitiesRedux.Views.Avalonia.Services.Abstractions;

internal class Timer : ITimer
{
    private readonly DispatcherTimer _dispatcherTimer;

    public Timer(DispatcherTimer dispatcherTimer)
    {
        _dispatcherTimer = dispatcherTimer;
    }


    public bool IsResumed
    {
        get => _dispatcherTimer.IsEnabled;
        set
        {
            if (value)
                _dispatcherTimer.Start();
            else
                _dispatcherTimer.Stop();
        }
    }


    public void Pause()
    {
        _dispatcherTimer.Stop();
    }

    public void Resume()
    {
        _dispatcherTimer.Start();
    }
}