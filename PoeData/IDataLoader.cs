﻿namespace PoeData;

/// <summary>
/// Interface with methods for data loader.
/// </summary>
internal interface IDataLoader
{
    /// <summary>
    /// Gets bytes for a file.
    /// </summary>
    /// <param name="filePath">file path to get bytes from.</param>
    /// <returns>bytes for a file.</returns>
    public byte[] GetFileBytes(string filePath);

    /// <summary>
    /// Reads all bytes from the index file.
    /// </summary>
    /// <returns>bytes from the index file.</returns>
    public byte[] ReadIndex();
}
