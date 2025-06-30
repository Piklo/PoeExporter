using System;
using System.CodeDom.Compiler;
using System.IO;

namespace PoeData.Generator;

internal sealed class TableGenerator : IDisposable
{
    private readonly Table _table;
    private readonly IndentedTextWriter _writer;

    public TableGenerator(Table table)
    {
        _table = table;

        var stringWriter = new StringWriter();
        _writer = new(stringWriter);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }

    public void GenerateCode()
    {
        var namespace1 = _table.ValidFor != 3 ? $"Poe{_table.ValidFor}" : "PoeCommon";
        var suffix = _table.ValidFor != 3 ? $"_{_table.ValidFor}" : "";
        var name = $"{_table.Name}{suffix}";
        _writer.WriteLine($"namespace PoeData.{namespace1}.Table;");
        _writer.WriteLine($"public sealed class {name}");
        _writer.WriteIndentedBlock((writer) => { });
    }

    public string GetSourceCode()
    {
        var source = _writer.InnerWriter.ToString();

        return source;
    }
}
