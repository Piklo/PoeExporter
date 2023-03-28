using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class which parses the column which is a bool and is not an array.
/// </summary>
internal sealed class BoolNonArrayColumn : IParsedColumn
{
    /// <inheritdoc/>
    public string ClassPropertyName { get; }

    /// <inheritdoc/>
    public string? ReferencedTable { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoolNonArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public BoolNonArrayColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        ClassPropertyName = column.Name is not null ? column.Name : ColumnGeneratorHelper.GetUnknownColumnName(parsedColumns);
        LoadingPropertyName = ClassPropertyName.ToLower();
        ReferencedTable = column.References?.Table;
    }

    /// <inheritdoc/>
    public string[] GetPropertyStrings()
    {
        var strings = new string[]
        {
            $"/// <summary> Gets a value indicating whether {ClassPropertyName} is set.</summary>",
            $$"""public required bool {{ClassPropertyName}} { get; init; }""",
        };

        return strings;
    }
    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var strings = new string[]
        {
            $"// loading {ClassPropertyName}",
            $"(var {LoadingPropertyName}, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);",
        };

        return strings;
    }
}
