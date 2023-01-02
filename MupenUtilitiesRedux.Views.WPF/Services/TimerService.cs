using System;
using System.Windows.Threading;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;
using MupenUtilitiesRedux.Views.WPF.Services.Abstractions;

namespace MupenUtilitiesRedux.Views.WPF.Services;

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
			new DispatcherTimer(interval, DispatcherPriority.Normal, (o, e) => { callback(); }, _dispatcher);

		return new Timer(dispatcherTimer);
	}
}