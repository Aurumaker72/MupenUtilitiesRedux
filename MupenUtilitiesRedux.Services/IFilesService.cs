﻿using MupenUtilitiesRedux.Services.Abstractions;

// original: https://github.com/Sergio0694/Brainf_ckSharp/blob/master/src/Brainf_ckSharp.Services/IFilesService.cs
// extended by aurumaker72

namespace MupenUtilitiesRedux.Services;

/// <summary>
///     The default <see langword="interface" /> for a service that handles files
/// </summary>
public interface IFilesService
{
    /// <summary>
    ///     Gets the path of the installation directory
    /// </summary>
    string InstallationPath { get; }

    /// <summary>
    ///     Gets the path of the local temporary files folder
    /// </summary>
    string TemporaryFilesPath { get; }

    /// <summary>
    ///     Gets a target file from a specified path
    /// </summary>
    /// <param name="path">The path of the file to retrieve</param>
    /// <returns>The file retrieved from the specified path</returns>
    Task<IFile> GetFileFromPathAsync(string path);

    /// <summary>
    ///     Tries to get a target file from a specified path
    /// </summary>
    /// <param name="path">The path of the file to retrieve</param>
    /// <returns>The file retrieved from the specified path, if existing</returns>
    Task<IFile?> TryGetFileFromPathAsync(string path);

    /// <summary>
    ///     Tries to open or create a file from a specified path
    /// </summary>
    /// <param name="path">The path of the file to create or open</param>
    /// <returns>The file created or opened from the specified path</returns>
    Task<IFile> CreateOrOpenFileFromPathAsync(string path);

    /// <summary>
    ///     Tries to pick a file to open with a specified extension
    /// </summary>
    /// <param name="extensions">The extensions to use</param>
    /// <returns>A <see cref="IFile" /> to open, if available</returns>
    Task<IFile?> TryPickOpenFileAsync(string[] extensions);

    /// <summary>
    ///     Tries to pick a file to save to with the specified parameters
    /// </summary>
    /// <param name="filename">The suggested filename to use</param>
    /// <param name="fileType">The info on the file type to save to</param>
    /// <returns>A <see cref="IFile" /> to use to save data to, if available</returns>
    Task<IFile?> TryPickSaveFileAsync(string filename, (string Name, string[] Extensions) fileType);

    /// <summary>
    ///     Enumerates all the available files from the future access list.
    /// </summary>
    /// <returns>An <see cref="IAsyncEnumerable{T}" /> sequence of available files.</returns>
    IAsyncEnumerable<(IFile File, string Metadata)> GetFutureAccessFilesAsync();

    /// <summary>
    ///     Checks whether the file at the specified path is accessible
    /// </summary>
    /// <param name="path">The path of the file to retrieve</param>
    /// <returns>Whether the file is accessible</returns>
    Task<bool> IsAccessible(string path);
}