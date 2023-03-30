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

    /// <summary>
    /// Gets an array of not indented strings related to loading referenced tables.
    /// </summary>
    /// <param name="parsedColumns">parsed columns.</param>
    /// <returns>loading referenced tables strings.</returns>
    internal static string[] GetReferencedTablesLoading(ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        var set = new HashSet<string>();

        foreach (var column in parsedColumns)
        {
            if (column.ReferencedTable is null)
            {
                continue;
            }

            var str = $"specification.Get{column.ReferencedTable}Dat();";

            set.Add(str);
        }

        return set.ToArray();
    }

    /// <summary>
    /// Gets an array of not indented strings related to creating the instance of a class.
    /// </summary>
    /// <param name="parsedColumns">parsed columns.</param>
    /// <returns>class initialization strings.</returns>
    internal static string[] GetObjectInitialization(ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        var strings = new string[parsedColumns.Count];

        for (var i = 0; i < parsedColumns.Count; i++)
        {
            var column = parsedColumns[i];

            var str = $"{column.ClassPropertyName} = {column.LoadingPropertyName},";

            strings[i] = str;
        }

        return strings;
    }
}
