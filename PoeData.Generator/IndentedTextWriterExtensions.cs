using System;
using System.CodeDom.Compiler;

namespace PoeData.Generator;

public static class IndentedTextWriterExtensions
{
    public static void WriteEmptyLine(this IndentedTextWriter writer)
    {
        writer.WriteLineNoTabs(string.Empty);
    }

    public static void WriteIndentedBlock(this IndentedTextWriter writer, Action<IndentedTextWriter> action, bool wrapWithBraces = true)
    {
        if (wrapWithBraces)
        {
            writer.WriteLine('{');
        }

        writer.Indent++;
        action.Invoke(writer);
        writer.Indent--;

        if (wrapWithBraces)
        {
            writer.WriteLine('}');
        }
    }
}
