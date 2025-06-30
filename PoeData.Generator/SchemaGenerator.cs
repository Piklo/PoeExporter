using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            .Select(GetSchema);

        var tablesProvider = schemaProvider.SelectMany(GetTables);

        // context.RegisterSourceOutput(schemaProvider, OutputSchema);
        context.RegisterSourceOutput(tablesProvider, OutputSchema);
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

    private static Schema GetSchema(string schemaText, CancellationToken cancellationToken)
    {
        var schema = JsonSerializer.Deserialize<Schema>(schemaText);

        if (schema is null)
        {
            throw new ArgumentException($"Provided schema failed to deserialize to {nameof(Schema)}.");
        }

#if DEBUG
        var schema2 = JsonSerializer.Deserialize<Schema>(schemaText);

        if (schema2 is null)
        {
            throw new("Failed to construct second schema.");
        }

        Debug.Assert(schema.Equals(schema2));
        Debug.Assert(schema.GetHashCode() == schema2.GetHashCode());
#endif

        return schema;
    }

    private static IEnumerable<Table> GetTables(Schema schema, CancellationToken cancellationToken)
    {
        return schema.Tables;
    }

    // private static void OutputSchema(SourceProductionContext context, Schema schema)
    // {
    //     using var stringWriter = new StringWriter();
    //     using var writer = new IndentedTextWriter(stringWriter);
    //
    //     context.AddSource("test.g.cs", "//empty");
    // }

    private static void OutputSchema(SourceProductionContext context, Table table)
    {
        using var tableGenerator = new TableGenerator(table);
        var source = tableGenerator.GetSourceCode();

        var suffix = table.ValidFor != 3 ? $"_{table.ValidFor}" : "";
        var name = $"{table.Name}{suffix}";

        context.AddSource($"{name}.g.cs", source);
    }
}
