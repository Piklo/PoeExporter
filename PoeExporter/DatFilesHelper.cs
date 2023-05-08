using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace PoeExporter;

/// <summary>
/// Class containing helper methods related to dat files.
/// </summary>
internal static class DatFilesHelper
{
    /// <summary>
    /// Checks if <paramref name="item"/> is null and logs a warning message if null.
    /// </summary>
    /// <typeparam name="TItem">Type of the item to be checked.</typeparam>
    /// <param name="logger">logger instance to log.</param>
    /// <param name="varName">name of the variable to report as null if item is null.</param>
    /// <param name="item">item to check.</param>
    /// <returns>true if <paramref name="item"/> is null, false otherwise.</returns>
    internal static bool IsNullWithLog<TItem>(ILogger logger, string varName, [NotNullWhen(false)] TItem? item)
    {
        var isNull = item is null;

        if (isNull)
        {
            logger.Warning("{varName} is null", varName);
        }

        return isNull;
    }

    /// <summary>
    /// Checks if <paramref name="item"/> is null and logs a warning message if null.
    /// </summary>
    /// <typeparam name="TItem">Type of the item to be checked.</typeparam>
    /// <typeparam name="TId">Type of the id for which the <paramref name="item"/> is null.</typeparam>
    /// <param name="logger">logger instance to log.</param>
    /// <param name="varName">name of the variable to report as null if item is null.</param>
    /// <param name="item">item to check.</param>
    /// <param name="idName">name of the id variable.</param>
    /// <param name="id">id for which the <paramref name="item"/> is null.</param>
    /// <returns>true if <paramref name="item"/> is null, false otherwise.</returns>
    internal static bool IsNullWithLog<TItem, TId>(ILogger logger, string varName, [NotNullWhen(false)] TItem? item, string idName, TId id)
    {
        var isNull = item is null;

        if (isNull)
        {
            logger.Warning("{varName} is null for {idName} = {id}", varName, idName, id);
        }

        return isNull;
    }
}
