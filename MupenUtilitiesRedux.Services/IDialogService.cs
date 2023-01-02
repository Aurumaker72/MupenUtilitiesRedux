namespace MupenUtilitiesRedux.Services;

/// <summary>
///     The default <see langword="interface" /> for a service that displays dialogs
/// </summary>
public interface IDialogService
{
	/// <summary>
	///     Shows an error dialog
	/// </summary>
	/// <param name="content">The dialog's content</param>
	public void ShowError(string content);
}