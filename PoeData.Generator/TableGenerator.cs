using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using PoeData.Generator.Columns;

namespace PoeData.Generator;

internal sealed class TableGenerator : IDisposable
{
    private readonly Table _table;
    private readonly IReadOnlyList<IColumn> _columns;
    private readonly IndentedTextWriter _writer;

    public TableGenerator(Table table)
    {
        _table = table;

        var stringWriter = new StringWriter();
        _writer = new(stringWriter);

        var columns = new List<IColumn>();
        foreach (var column in _table.Columns)
        {
            var parsedColumn = ColumnHelpers.GetParsedColumn(column);
            columns.Add(parsedColumn);
        }
        _columns = columns;
    }

    public void Dispose()
    {
        _writer.Dispose();
    }

    public string GenerateCode()
    {
        var namespace1 = _table.ValidFor != 3 ? $"Poe{_table.ValidFor}" : "PoeCommon";
        _writer.WriteLine($"namespace PoeData.{namespace1}.Table;");
        _writer.WriteEmptyLine();
        _writer.WriteLine($"public sealed class {_table.Name}");
        _writer.WriteIndentedBlock(WriteProperties);

        var source = _writer.InnerWriter.ToString();
        return source;
    }

    private void WriteProperties(IndentedTextWriter writer)
    {
        foreach (var column in _columns)
        {
            writer.WriteLine($$"""public required {{column.FullExposedTypeName}} {{column.PropertyName}} = { get; init; }""");
        }
    }
}
