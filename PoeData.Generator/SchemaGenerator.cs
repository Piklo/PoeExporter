using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace PoeData.Generator;

[Generator]
public class SchemaGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var schemaProvider = context.AdditionalTextsProvider.Where(Predicate)
            .Select(GetOutput)
            .Select((schemaText, _) => schemaText);

        context.RegisterSourceOutput(schemaProvider, OutputSchema);
    }

    private static bool Predicate(AdditionalText text)
    {
        return Path.GetFileName(text.Path) == "schema.json";
    }

    private static string GetOutput(AdditionalText text, CancellationToken _)
    {
        var source = text.GetText();
        if (source is null)
        {
            throw new ArgumentException("Received text is null");
        }

        return source.ToString();
    }

    private static void OutputSchema(SourceProductionContext context, string schemaText)
    {
        var schema = JsonSerializer.Deserialize<Schema>(schemaText);

        using var stringWriter = new StringWriter();
        using var writer = new IndentedTextWriter(stringWriter);

        context.AddSource("test.cs", "//empty");
    }
}

internal sealed class Schema
{
    [JsonPropertyName("version")]
    public required int Version { get; init; }

    [JsonPropertyName("createdAt")]
    public required int CreatedAt { get; init; }

    [JsonPropertyName("tables")]
    public required Tables[] Tables { get; init; }

    [JsonPropertyName("enumerations")]
    public required Enumerations[] Enumerations { get; init; }
}

internal sealed class Tables
{
    [JsonPropertyName("validFor")]
    public required int ValidFor { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("columns")]
    public required Columns[] Columns { get; init; }

    [JsonPropertyName("tags")]
    public required string[] Tags { get; init; }
}

internal sealed class Columns
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("array")]
    public required bool Array { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("unique")]
    public required bool Unique { get; init; }

    [JsonPropertyName("localized")]
    public required bool Localized { get; init; }

    [JsonPropertyName("references")]
    public required References? References { get; init; }

    [JsonPropertyName("until")]
    public required object Until { get; init; }

    [JsonPropertyName("file")]
    public required string File { get; init; }

    [JsonPropertyName("files")]
    public required string[] Files { get; init; }

    [JsonPropertyName("interval")]
    public required bool Interval { get; init; }
}

internal sealed class References
{
    [JsonPropertyName("table")]
    public required string Table { get; init; }

    [JsonPropertyName("column")]
    public string? Column { get; init; }
}

internal sealed class Enumerations
{
    [JsonPropertyName("validFor")]
    public required int ValidFor { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("indexing")]
    public required int Indexing { get; init; }

    [JsonPropertyName("enumerators")]
    public required string[] Enumerators { get; init; }
}
