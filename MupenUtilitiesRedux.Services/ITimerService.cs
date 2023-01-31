using MupenUtilitiesRedux.Services.Abstractions;

namespace MupenUtilitiesRedux.Services;

/// <summary>
///     The default <see langword="interface" /> for a service that provides creation functionality for
///     <see cref="ITimer" />
/// </summary>
public interface ITimerService
{
	/// <summary>
	///     Creates an <see cref="ITimer" />
	/// </summary>
	/// <param name="interval">The time between <see cref="ITimer" /> ticks</param>
	/// <param name="callback">The method to be invoked on a tick</param>
	/// <returns>The created <see cref="ITimer" /></returns>
	ITimer Create(TimeSpan interval, Action callback);
}