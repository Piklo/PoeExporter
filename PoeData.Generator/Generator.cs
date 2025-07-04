using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace PoeData.Generator;

[Generator]
public class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var schemaProvider = context.AdditionalTextsProvider.Where(Predicate)
            .Select(GetSchemaString)
            .Select(GetSchema);

        var tablesProvider = schemaProvider.SelectMany(GetTables);

        // context.RegisterSourceOutput(schemaProvider, OutputSchema);
        context.RegisterSourceOutput(tablesProvider, GenerateTable);
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

    private static void GenerateTable(SourceProductionContext context, Table table)
    {
        if (table.ValidFor is 1 or 3)
        {
            GenerateTable(context, table, 1);
        }

        if (table.ValidFor is 2 or 3)
        {
            GenerateTable(context, table, 2);
        }
    }

    private static void GenerateTable(SourceProductionContext context, Table table, int chosenPoeVersion)
    {
        using var tableGenerator = new TableGenerator(table, chosenPoeVersion);
        var source = tableGenerator.GenerateCode();

        var name = $"{table.Name}{chosenPoeVersion}";

        context.AddSource($"{name}.g.cs", source);
    }
}
