using System.Windows.Threading;
using MupenUtilitiesRedux.Services.Abstractions;

namespace MupenUtilitiesRedux.Views.WPF.Services.Abstractions;

internal class Timer : ITimer
{
	private readonly DispatcherTimer _dispatcherTimer;

	public Timer(DispatcherTimer dispatcherTimer)
	{
		_dispatcherTimer = dispatcherTimer;
	}


	public void Pause()
	{
		_dispatcherTimer.Stop();
	}

	public void Resume()
	{
		_dispatcherTimer.Start();
	}

	public bool IsResumed => _dispatcherTimer.IsEnabled;
}