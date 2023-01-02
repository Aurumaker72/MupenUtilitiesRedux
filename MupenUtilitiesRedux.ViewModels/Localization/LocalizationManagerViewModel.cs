using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;
using Newtonsoft.Json;

namespace MupenUtilitiesRedux.ViewModels.Localization;

/// <summary>
///     A <see langword="class" /> which represents the application's root ViewModel for localization
/// </summary>
/// <remarks>
///     This should only be created once and injected into all ViewModels which require it
/// </remarks>
public partial class LocalizationManagerViewModel : ObservableObject
{
	private static LocalizationDataViewModel _defaultLocalizationDataViewModel = new();

	private readonly IFilesService _filesService;
	private readonly Dictionary<string, LocalizationDataViewModel> _localizationData = new();

	public LocalizationManagerViewModel(IFilesService filesService)
	{
		_filesService = filesService;
	}

	public LocalizationDataViewModel LocalizationData =>
		_localizationData[Culture] /*?? _defaultLocalizationDataViewModel*/;

	public string Culture { get; private set; }

	public event Action? OnCultureChanged;

	private async Task LoadLocalizationData(string name)
	{
		var path = $"Cultures/{name}.json";
		var file = await _filesService.GetFileFromPathAsync(path);
		var text = await file.ReadAllText(Encoding.UTF8);
		text = text.Substring(1); // FIXME: UMMMM first char is corrupted

		_localizationData[name] = JsonConvert.DeserializeObject<LocalizationDataViewModel>(text);
	}

	public async Task Load()
	{
		await LoadLocalizationData("en-US");
		await LoadLocalizationData("de-DE");

		SetCultureString("en-US");
	}

	[RelayCommand]
	private void SetCultureString(string cultureString)
	{
		Culture = cultureString;
		OnCultureChanged?.Invoke();
		OnPropertyChanged(nameof(LocalizationData));
		OnPropertyChanged(nameof(Culture));
	}
}