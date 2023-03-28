using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Class which parses the column which is a foreign row reference and is not an array.
/// </summary>
internal class ForeignrowNonArrayColumn : IParsedColumn
{
    /// <inheritdoc/>
    public string ClassPropertyName { get; }

    /// <inheritdoc/>
    public string ReferencedTable { get; }

    /// <inheritdoc/>
    public string LoadingPropertyName { get; }

    /// <inheritdoc/>
    public int Offset { get; } = 8;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForeignrowNonArrayColumn"/> class.
    /// </summary>
    /// <param name="column">column to parse.</param>
    /// <param name="parsedColumns">a readonly collection of already parsed columns.</param>
    public ForeignrowNonArrayColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        ClassPropertyName = column.Name is not null ? column.Name : ColumnGeneratorHelper.GetUnknownColumnName(parsedColumns);
        LoadingPropertyName = ClassPropertyName.ToLower();

        if (column.References is null)
        {
            throw new NotImplementedException("foreign row reference with null referenced table");
        }

        ReferencedTable = column.References.Table;

    }

    /// <inheritdoc/>
    public string[] GetPropertyStrings()
    {
        var strings = new string[]
        {
            $"/// <summary> Gets {ClassPropertyName}.</summary>",
            $$"""public required {{ReferencedTable}} {{ClassPropertyName}} { get; init; }""",
        };

        return strings;
    }

    /// <inheritdoc/>
    public string[] GetLoading()
    {
        var primaryKeyName = $"{LoadingPropertyName}PrimaryKey";
        var strings = new string[]
        {
            $"// loading {ClassPropertyName}",
            $"(var {primaryKeyName}, offset) = SpecificationFileLoader.LoadPrimaryKey(decompressedFile, offset, dataOffset);",
            $"var {ReferencedTable}? {LoadingPropertyName} = null;",
            $"if ({primaryKeyName} is not null)",
            "{",
            $"{SpecificationFileGenerator.Tab}{LoadingPropertyName} = specification.Get{ReferencedTable}()[(int){primaryKeyName}];",
            "}",
        };

        return strings;
    }
}
