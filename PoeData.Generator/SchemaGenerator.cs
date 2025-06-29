using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text.Json;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace PoeData.Generator;

[Generator]
public class SchemaGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var schemaProvider = context.AdditionalTextsProvider.Where(Predicate)
            .Select(GetSchemaString)
            .Select((schemaText, _) => GetSchema(schemaText));

        context.RegisterSourceOutput(schemaProvider, OutputSchema);
    }

    private static bool Predicate(AdditionalText text)
    {
        return Path.GetFileName(text.Path) == "schema.json";
    }

    private static string GetSchemaString(AdditionalText text, CancellationToken _)
    {
        var source = text.GetText();
        if (source is null)
        {
            throw new ArgumentException("Received text is null");
        }

        return source.ToString();
    }

    private static Schema GetSchema(string schemaText)
    {
        var schema = JsonSerializer.Deserialize<Schema>(schemaText);
        var schema2 = JsonSerializer.Deserialize<Schema>(schemaText);

        if (schema is null)
        {
            throw new ArgumentException($"Provided schema failed to deserialize to {nameof(Schema)}.");
        }

        var equals = schema.Equals(schema2);
        var code1 = schema.GetHashCode();
        var code2 = schema2.GetHashCode();

        return schema;
    }

    private static void OutputSchema(SourceProductionContext context, Schema schema)
    {
        using var stringWriter = new StringWriter();
        using var writer = new IndentedTextWriter(stringWriter);

        context.AddSource("test.cs", "//empty");
    }
}
