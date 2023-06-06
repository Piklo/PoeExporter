﻿using PoeDataGenerator.ParsedColumns.Helpers;
using PoeDataGenerator.RepositoryGenerators;
using PoeDataGenerator.SchemaJson;

namespace PoeDataGenerator.ParsedColumns;

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
    public string? ReferencedColumn { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 1;

    /// <inheritdoc/>
    public string ClassPropertyUnderlyingType => "bool";

    /// <inheritdoc/>
    public string ClassPropertyType => ClassPropertyUnderlyingType;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoolNonArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public BoolNonArrayColumn(Column column, IReadOnlyList<IParsedColumn> parsedColumns)
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
            $"/// <summary> Gets a value indicating whether {ClassPropertyName} is set.</summary>",
            ColumnGeneratorHelper.GetReferenceString(ReferencedTable, ReferencedColumn),
            $$"""public required {{ClassPropertyType}} {{ClassPropertyName}} { get; init; }""",
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

    /// <inheritdoc/>
    public IReadOnlyList<LineOfCode> GetSingle(string datClassName)
    {
        return RepositoryGetMethodsHelper.GetSingleMethod(datClassName, this, false);
    }

    /// <inheritdoc/>
    public IReadOnlyList<LineOfCode> GetMany(string datClassName, string fieldName)
    {
        return RepositoryGetMethodsHelper.GetManyMethodNonNullableValueType(datClassName, fieldName, this);
    }

    /// <inheritdoc/>
    public IReadOnlyList<LineOfCode> GetManyToMany(string datClassName, string fieldName)
    {
        return RepositoryGetMethodsHelper.GetManyToMany(datClassName, fieldName, this);
    }
}
