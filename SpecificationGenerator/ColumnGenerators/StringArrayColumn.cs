using SpecificationGenerator.RepositoryGenerators;
using SpecificationGenerator.SchemaJson;

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
    public string? ReferencedColumn { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 16;

    /// <inheritdoc/>
    public string ClassPropertyUnderlyingType => "string";

    /// <inheritdoc/>
    public string ClassPropertyType => $"ReadOnlyCollection<{ClassPropertyUnderlyingType}>";

    /// <summary>
    /// Initializes a new instance of the <see cref="StringArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public StringArrayColumn(Column column, IReadOnlyList<IParsedColumn> parsedColumns)
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
            $"(var temp{LoadingPropertyName}, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);",
            $"var {LoadingPropertyName} = temp{LoadingPropertyName}.AsReadOnly();",
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
        return RepositoryGetMethodsHelper.GetManyMethodReferenceArrayType(datClassName, fieldName, this);
    }
}
