﻿using PoeDataGenerator.ParsedColumns.Helpers;
using PoeDataGenerator.SchemaJson;

namespace PoeDataGenerator.ParsedColumns;

/// <summary>
/// Class which parses the column which is an enum row and is an array.
/// </summary>
internal sealed class EnumRowArrayColumn : IParsedColumn
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

    /// <inheritdoc/>
    public TypeData Type => TypeData.ReadonlyCollectionInt;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumRowArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public EnumRowArrayColumn(Column column, IReadOnlyList<IParsedColumn> parsedColumns)
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
            $$"""public required {{Type.Type}} {{ClassPropertyName}} { get; init; }""",
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
