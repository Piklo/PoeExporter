﻿using System.Runtime.Serialization;

namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Thrown when a ggpk tag record is found not at the start of the file.
/// </summary>
public class FoundAnotherGgpkTagRecordException : Exception
{
    /// <inheritdoc cref="Exception()"/>
    public FoundAnotherGgpkTagRecordException()
    {
    }

    /// <inheritdoc cref="Exception(string?)"/>
    public FoundAnotherGgpkTagRecordException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(string?, Exception?)"/>
    public FoundAnotherGgpkTagRecordException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    protected FoundAnotherGgpkTagRecordException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}