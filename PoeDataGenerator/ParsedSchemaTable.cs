using PoeDataGenerator.ParsedColumns;
using PoeDataGenerator.ParsedColumns.Helpers;
using PoeDataGenerator.SchemaJson;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace PoeDataGenerator;

/// <summary>
/// Parses <see cref="SchemaJson.Table"/>.
/// </summary>
internal sealed class ParsedSchemaTable
{
    /// <summary>Gets Table.</summary>
    public Table Table { get; }

    /// <summary>Gets file name.</summary>
    public IReadOnlyList<IParsedColumn> ParsedColumns { get; }

    /// <summary>Gets table name.</summary>
    public string Name => Table.Name;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParsedSchemaTable"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="table">Table to parse.</param>
    public ParsedSchemaTable(Table table)
    {
        Table = table;

        ParsedColumns = ParseColumns(table);
    }

    private IReadOnlyList<IParsedColumn> ParseColumns(Table table)
    {
        var result = new List<IParsedColumn>();
        var readonlyResult = result.AsReadOnly();

        foreach (var column in table.Columns)
        {
            try
            {
                var parsedColumn = ParseColumn(column, readonlyResult);
                result.Add(parsedColumn);
            }
            catch (NotImplementedColumnException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        return readonlyResult;
    }

    private static IParsedColumn ParseColumn(Column column, IReadOnlyList<IParsedColumn> parsedColumns)
    {
        if (column.Type == "bool" && !column.Array)
        {
            return new BoolNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "i32" && !column.Array)
        {
            return new IntNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "i32" && column.Array)
        {
            return new IntArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "f32" && !column.Array)
        {
            return new FloatNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "f32" && column.Array)
        {
            return new FloatArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "foreignrow" && !column.Array)
        {
            return new ForeignRowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "foreignrow" && column.Array)
        {
            return new ForeignRowArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "string" && !column.Array)
        {
            return new StringNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "string" && column.Array)
        {
            return new StringArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "array" && column.Array)
        {
            return new ArrayArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "row" && !column.Array)
        {
            return new RowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "row" && column.Array)
        {
            return new RowArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "enumrow" && !column.Array)
        {
            return new EnumRowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "enumrow" && column.Array)
        {
            return new EnumRowArrayColumn(column, parsedColumns);
        }
        else
        {
            var serialized = JsonSerializer.Serialize(column, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            });

            var message = $"dont know how to load column: \n{serialized}";

            throw new NotImplementedColumnException(message);
        }
    }
}
