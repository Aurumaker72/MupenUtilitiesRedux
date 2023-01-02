using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Win32;
using MupenUtilitiesRedux.Services;
using MupenUtilitiesRedux.Services.Abstractions;
using File = MupenUtilitiesRedux.Views.WPF.Services.Abstractions.File;

namespace MupenUtilitiesRedux.Views.WPF.Services;

/// <summary>
///     A <see langword="class" /> that implements the <see cref="IFilesService" /> <see langword="interface" /> using
///     WPF APIs
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
		var fileDialog = new OpenFileDialog
		{
			AddExtension = true,
			CheckFileExists = true,
			CheckPathExists = true
			//Filter = 
		};
		var result = fileDialog.ShowDialog();

		if (result != null && result.Value)
			return new File(fileDialog.FileName);
		return null;
	}

	/// <inheritdoc />
	public async Task<IFile?> TryPickSaveFileAsync(string filename, (string Name, string[] Extensions) fileType)
	{
		var fileDialog = new SaveFileDialog
		{
			AddExtension = true
			//Filter = 
		};
		var result = fileDialog.ShowDialog();

		if (result != null && result.Value)
			return new File(fileDialog.FileName);
		return null;
	}

	/// <inheritdoc />
	public async IAsyncEnumerable<(IFile, string)> GetFutureAccessFilesAsync()
	{
		yield return await Task.FromResult<(IFile, string)>((null, null));
		throw new NotImplementedException();
	}
}