namespace MupenUtilitiesRedux.ViewModels.Localization;

/// <summary>
///     A <see langword="class" /> which holds localized data
/// </summary>
/// <remarks>
///     This <see langword="class" /> is <b>serializable</b> and <see cref="OnPropertyChanged" /> will be fired on the
///     container, not the <see langword="class" /> itself.
/// </remarks>
public class LocalizationDataViewModel
{
	public string File { get; set; } = nameof(File);
	public string Help { get; set; } = nameof(Help);
	public string Header { get; set; } = nameof(Header);
	public string Frames { get; set; } = nameof(Frames);
	public string Replacement { get; set; } = nameof(Replacement);
	public string FileReadFailure { get; set; } = nameof(FileReadFailure);
	public string Load { get; set; } = nameof(Load);
	public string Save { get; set; } = nameof(Save);
	public string Languages { get; set; } = nameof(Languages);
	public string Author { get; set; } = nameof(Author);
	public string Description { get; set; } = nameof(Description);
	public string Tas { get; set; } = nameof(Tas);
	public string Creator { get; set; } = nameof(Creator);
	public string Rerecords { get; set; } = nameof(Rerecords);
}