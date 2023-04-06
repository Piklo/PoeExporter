﻿namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Interface containing properties required by read ggpk tag records.
/// </summary>
internal interface IGgpkTagRecord
{
    /// <summary>Gets length.</summary>
    public int Length { get; init; }

    /// <summary>Gets offset.</summary>
    public long Offset { get; init; }
}

/// <summary>
/// Interface containing Read method for ggpk tag records.
/// </summary>
/// <typeparam name="T">type of the returned <see cref="IGgpkTagRecord"/>.</typeparam>
internal interface IReadGgpkTagRecord<T>
    where T : IGgpkTagRecord
{
    /// <summary>
    /// Reads ggpk record data.
    /// </summary>
    /// <param name="ggpkReader">reader to read from.</param>
    /// <param name="length">records length.</param>
    /// <param name="offset">records offset.</param>
    /// <returns>parsed ggpk record data.</returns>
    public static abstract T Read(BinaryReader ggpkReader, int length, long offset);
}