using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;
using File = MupenUtilitiesRedux.Views.Avalonia.Services.Abstractions.File;

namespace MupenUtilitiesRedux.Views.Avalonia.Services;

/// <summary>
///     A <see langword="class" /> that implements the <see cref="IFilesService" /> <see langword="interface" /> using
///     Avalonia APIs
/// </summary>
public sealed class FilesService : IFilesService
{
	/// <inheritdoc />
	public string InstallationPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

	/// <inheritdoc />
	public string TemporaryFilesPath => Path.GetTempPath();

	/// <inheritdoc />
	public async Task<IFile> GetFileFromPathAsync(string path)
	{
		return new File(path);
	}

	/// <inheritdoc />
	public async Task<IFile?> TryGetFileFromPathAsync(string path)
	{
		try
		{
			return await GetFileFromPathAsync(path);
		}
		catch (FileNotFoundException)
		{
			return null;
		}
	}

	/// <inheritdoc />
	public async Task<IFile> CreateOrOpenFileFromPathAsync(string path)
	{
		var folderPath = Path.GetDirectoryName(path);
		var filename = Path.GetFileName(path);

		if (folderPath != null && !Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

		if (!System.IO.File.Exists(path)) System.IO.File.Create(path);

		return new File(path);
	}

	/// <inheritdoc />
	public async Task<IFile?> TryPickOpenFileAsync(string[] extensions)
	{
		var adjustedExtensions = new string[extensions.Length];
		for (var i = 0; i < adjustedExtensions.Length; i++) adjustedExtensions[i] = $"*.{extensions[i]}";

		var storageFiles = await MainWindow.Instance.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
		{
			AllowMultiple = false,
			FileTypeFilter = new List<FilePickerFileType>
			{
				new("Supported formats")
				{
					Patterns = adjustedExtensions
				}
			}
		});

		if (storageFiles.Count > 0)
		{
			storageFiles[0].TryGetUri(out var uri);

			if (uri != null) return new File(uri.AbsolutePath.Replace("%20", " "));
		}


		return null;
	}

	/// <inheritdoc />
	public async Task<IFile?> TryPickSaveFileAsync(string filename, (string Name, string[] Extensions) fileType)
	{
		var adjustedExtensions = new string[fileType.Extensions.Length];
		for (var i = 0; i < adjustedExtensions.Length; i++) adjustedExtensions[i] = $"*.{fileType.Extensions[i]}";

		var storageFile = await MainWindow.Instance.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
		{
			FileTypeChoices = new List<FilePickerFileType>
			{
				new(fileType.Name)
				{
					Patterns = adjustedExtensions
				}
			}
		});

		if (storageFile != null)
		{
			storageFile.TryGetUri(out var uri);
			if (uri != null) return new File(uri.AbsolutePath.Replace("%20", " "));
		}

		return null;
	}

	/// <inheritdoc />
	public async IAsyncEnumerable<(IFile, string)> GetFutureAccessFilesAsync()
	{
		yield return await Task.FromResult<(IFile, string)>((null, null));
		throw new NotImplementedException();
	}
}