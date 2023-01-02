namespace MupenUtilitiesRedux.Services.Abstractions;

/// <summary>
///     The default <see langword="interface" /> for a service that provides repeated method invocation functionality
/// </summary>
public interface ITimer
{
	/// <summary>
	///     Pauses the timer
	/// </summary>
	public void Pause();

	/// <summary>
	///     Resumes the timer
	/// </summary>
	public void Resume();

	/// <summary>
	/// Whether the timer is resumed
	/// </summary>
	public bool IsResumed { get; }
}