using System;
using Avalonia.Threading;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;
using MupenUtilitiesRedux.Views.Avalonia.Services.Abstractions;

namespace MupenUtilitiesRedux.Views.Avalonia.Services;

internal class TimerService : ITimerService
{
    private readonly Dispatcher _dispatcher;

    public TimerService(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public ITimer Create(TimeSpan interval, Action callback)
    {
        var dispatcherTimer =
            new DispatcherTimer(interval, DispatcherPriority.Normal, (o, e) => { callback(); });

        return new Timer(dispatcherTimer);
    }
}