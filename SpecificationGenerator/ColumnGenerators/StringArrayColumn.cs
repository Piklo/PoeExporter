﻿using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class which parses the column which is a string and is an array.
/// </summary>
internal sealed class StringArrayColumn : IParsedColumn
{
    /// <inheritdoc/>
    public string ClassPropertyName { get; }

    /// <inheritdoc/>
    public string? ReferencedTable { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 16;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public StringArrayColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
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
            $"/// <summary> Gets {ClassPropertyName}.</summary>",
            $$"""public required ReadOnlyCollection<string> {{ClassPropertyName}} { get; init; }""",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var strings = new string[]
        {
            $"// loading {ClassPropertyName}",
            $"(var temp{LoadingPropertyName}, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);",
            $"var {LoadingPropertyName} = temp{LoadingPropertyName}.AsReadOnly();",
        };

        return strings;
    }
}
