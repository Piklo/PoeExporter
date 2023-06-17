﻿namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Helper class related to parsing unicode bytes in StatDescriptions.
/// </summary>
internal static class StatDescriptionsHelper
{
    /// <summary>
    /// Checks if next two bytes at the index translate to newline character.
    /// </summary>
    /// <param name="span">span with unicode bytes.</param>
    /// <param name="index">index to check.</param>
    /// <returns><see langword="true"/> if they translate to the newline character, <see langword="false"/> otherwise.</returns>
    internal static bool IsNewLine(ReadOnlySpan<byte> span, int index)
    {
        if (index + 1 >= span.Length)
        {
            return false;
        }

        return span[index] == '\n' && span[index + 1] == 0;
    }

    /// <summary>
    /// Checks if next two bytes at the index translate to tab character.
    /// </summary>
    /// <param name="span">span with unicode bytes.</param>
    /// <param name="index">index to check.</param>
    /// <returns><see langword="true"/> if they translate to the tab character, <see langword="false"/> otherwise.</returns>
    internal static bool IsTab(ReadOnlySpan<byte> span, int index)
    {
        if (index + 1 >= span.Length)
        {
            return false;
        }

        return span[index] == '\t' && span[index + 1] == 0;
    }
}
