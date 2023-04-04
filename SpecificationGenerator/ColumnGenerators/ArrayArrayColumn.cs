using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class which parses the column which is an array and is an array.
/// </summary>
/// <remarks>
/// Only gods know what type array means, lets just assume it's array of ints for now.
/// </remarks>
internal sealed class ArrayArrayColumn : IParsedColumn
{
    /// <inheritdoc/>
    public string ClassPropertyName { get; }

    /// <inheritdoc/>
    public string? ReferencedTable { get; }

    /// <inheritdoc/>
    public string? ReferencedColumn { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 16;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public ArrayArrayColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        ClassPropertyName = column.Name is not null ? column.Name : ColumnGeneratorHelper.GetUnknownColumnName(parsedColumns);
        LoadingPropertyName = $"{ClassPropertyName.ToLower()}Loading";
        ReferencedTable = column.References?.Table;
        ReferencedColumn = column.References?.Column;
    }

    /// <inheritdoc/>
    public string[] GetPropertyStrings()
    {
        var strings = new string[]
        {
            $"/// <summary> Gets {ClassPropertyName}.</summary>",
            ColumnGeneratorHelper.GetReferenceString(ReferencedTable, ReferencedColumn),
            $$"""public required ReadOnlyCollection<int> {{ClassPropertyName}} { get; init; }""",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var strings = new string[]
        {
            $"// loading {ClassPropertyName}",
            $"(var temp{LoadingPropertyName}, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);",
            $"var {LoadingPropertyName} = temp{LoadingPropertyName}.AsReadOnly();",
        };

        return strings;
    }
}
