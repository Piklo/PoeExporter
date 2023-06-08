using Microsoft.CodeAnalysis;
using PoeDataGenerator.GeneratorHelpers;
using PoeDataGenerator.RepositoryGenerators;
using PoeDataGenerator.SchemaJson;
using System.Text.Json;

namespace PoeDataGenerator;

/// <summary>
/// Class used to generate source code for Specification, DatFiles and Repositories.
/// </summary>
[Generator]
internal sealed class SpecificationGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var schemaJsonProvider = context.AdditionalTextsProvider.Where(text =>
        {
            return text.Path.EndsWith(".json") && text.Path.Contains("schema");
        });

        var schemaProvider = schemaJsonProvider.Select((schemaFile, cancellationToken) =>
        {
            var jsonString = schemaFile.GetText()!.ToString();
            var schema = JsonSerializer.Deserialize<Schema>(jsonString);
            return schema;
        });

        var tablesProvider = schemaProvider.SelectMany((schema, cancellationToken) =>
        {
            return schema.Tables;
        });

        var parsedTablesProvider = tablesProvider.Select((table, cancellationToken) =>
        {
            var parsed = new ParsedSchemaTable(table);
            return parsed;
        });

        context.RegisterSourceOutput(parsedTablesProvider, (sourceProductionContext, table) =>
        {
            var datFileGenerator = new DatFileGenerator(table);
            sourceProductionContext.AddSource(datFileGenerator.FileName, datFileGenerator.Code);
        });

        context.RegisterSourceOutput(parsedTablesProvider, (sourceProductionContext, table) =>
        {
            var repositoryGenerator = new RepositoryGenerator(table);
            sourceProductionContext.AddSource(repositoryGenerator.FileName, repositoryGenerator.Code);
        });

        var collectedTables = tablesProvider.Collect();

        context.RegisterSourceOutput(collectedTables, (sourceProductionContext, tables) =>
        {
            var specificationGenerator = new SpecificationFileGenerator(tables);
            sourceProductionContext.AddSource(specificationGenerator.FileName, specificationGenerator.Code);
        });
    }
}
