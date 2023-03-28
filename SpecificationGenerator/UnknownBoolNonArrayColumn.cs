using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class which parses the column which is an unknown bool and is not an array.
/// </summary>
internal sealed class UnknownBoolNonArrayColumn : IParsedColumn
{
    private readonly string classPropertyName;
    private readonly string? referencedTable;
    private readonly string loadingPropertyName;

    /// <inheritdoc/>
    public int Offset { get; } = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownBoolNonArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns..</param>
    public UnknownBoolNonArrayColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        classPropertyName = ColumnGeneratorHelper.GetUnknownColumnName(parsedColumns);
        loadingPropertyName = classPropertyName.ToLower();
        referencedTable = column.References?.Table;
    }

    /// <inheritdoc/>
    public string[] GetPropertyStrings()
    {
        var strings = new string[]
        {
            $"/// <summary> Gets a value indicating whether {classPropertyName} is set.</summary>",
            $$"""public required bool {{classPropertyName}} { get; init; }""",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetReferencedTablesLoading()
    {
        if (referencedTable is null)
        {
            return Array.Empty<string>();
        }

        var str = new string[]
        {
            $"specification.Get{referencedTable}();",
        };

        return str;
    }

    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var strings = new string[]
        {
            $"// loading {classPropertyName}",
            $"(var {loadingPropertyName}, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetObjectInitialization()
    {
        var strings = new string[]
        {
            $"{classPropertyName} = {loadingPropertyName}",
        };

        return strings;
    }
}
