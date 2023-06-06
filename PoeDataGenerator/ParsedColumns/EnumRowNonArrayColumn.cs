﻿using PoeDataGenerator.Extensions;
using PoeDataGenerator.ParsedColumns.Helpers;
using PoeDataGenerator.RepositoryGenerators;
using PoeDataGenerator.SchemaJson;

namespace PoeDataGenerator.ParsedColumns;

/// <summary>
/// Class which parses the column which is an enum row and is not an array.
/// </summary>
internal sealed class EnumRowNonArrayColumn : IParsedColumn
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
    public int Offset { get; } = 4;

    /// <inheritdoc/>
    public string ClassPropertyUnderlyingType => "int";

    /// <inheritdoc/>
    public string ClassPropertyType => ClassPropertyUnderlyingType;

    /// <inheritdoc/>
    public Type ColumnType => typeof(int);

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumRowNonArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public EnumRowNonArrayColumn(Column column, IReadOnlyList<IParsedColumn> parsedColumns)
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
            $$"""public required {{ColumnType.GetCSharpRepresentation()}} {{ClassPropertyName}} { get; init; }""",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var strings = new string[]
        {
            $"// loading {ClassPropertyName}",
            $"(var {LoadingPropertyName}, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);",
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