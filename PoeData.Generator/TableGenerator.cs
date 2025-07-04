using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using PoeData.Generator.Columns;

namespace PoeData.Generator;

internal sealed class TableGenerator : IDisposable
{
    private readonly Table _table;
    private readonly int _chosenPoeVersion;
    private readonly IReadOnlyList<IColumn> _columns;
    private readonly IndentedTextWriter _writer;
    private readonly string _namespace;
    private readonly string _globalNamespace;

    public TableGenerator(Table table, int chosenPoeVersion)
    {
        _table = table;
        _chosenPoeVersion = chosenPoeVersion;

        var stringWriter = new StringWriter();
        _writer = new(stringWriter);


        _chosenPoeVersion = chosenPoeVersion;
        _namespace = $"PoeData.Poe{_chosenPoeVersion}";
        _globalNamespace = $"global::{_namespace}";

        var columns = new List<IColumn>();
        _columns = columns;
        var offset = 0;
        foreach (var column in _table.Columns)
        {
            var parsedColumn = ColumnHelpers.GetParsedColumn(column, _globalNamespace, offset, table.Name);
            columns.Add(parsedColumn);
            offset += parsedColumn.Size;
        }
    }

    public void Dispose()
    {
        _writer.Dispose();
    }

    public string GenerateCode()
    {
        _writer.WriteLine($"namespace {_namespace};");
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
            writer.WriteLine($$"""public required {{column.FullExposedTypeName}} {{column.PropertyName}} { get; init; }""");
        }
    }
}
