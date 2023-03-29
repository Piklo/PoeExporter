using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class which parses the column which is a row reference and is not an array.
/// </summary>
internal class RowNonArrayColumn : IParsedColumn
{
    /// <inheritdoc/>
    public string ClassPropertyName { get; }

    /// <inheritdoc/>
    public string? ReferencedTable { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 8;

    /// <summary>
    /// Initializes a new instance of the <see cref="RowNonArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public RowNonArrayColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        ClassPropertyName = column.Name is not null ? column.Name : ColumnGeneratorHelper.GetUnknownColumnName(parsedColumns);
        LoadingPropertyName = ClassPropertyName.ToLower();
        ReferencedTable = null; // kinda a hack because it does reference the same table, needed to avoid infinite load of the same table, FIXME
    }

    /// <inheritdoc/>
    public string[] GetPropertyStrings()
    {
        var strings = new string[]
        {
            $"/// <summary> Gets {ClassPropertyName}.</summary>",
            $$"""public required int? {{ClassPropertyName}} { get; init; }""",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var strings = new string[]
        {
            $"// loading {ClassPropertyName}",
            $"(var {LoadingPropertyName}, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);",
        };

        return strings;
    }
}
