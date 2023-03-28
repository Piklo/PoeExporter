using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class containing helper methods related to column generation.
/// </summary>
internal static class ColumnGeneratorHelper
{
    /// <summary>
    /// Generates a name for unknown column.
    /// </summary>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    /// <returns>string column name.</returns>
    internal static string GetUnknownColumnName(ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        var totalOffset = 0;

        foreach (var column in parsedColumns)
        {
            totalOffset += column.Offset;
        }

        var name = $"Unknown{totalOffset}";

        return name;
    }
}
