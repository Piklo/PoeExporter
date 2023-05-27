using Microsoft.CodeAnalysis;
using PoeDataGenerator.RepositoryGenerators;
using PoeDataGenerator.SchemaJson;
using System.Text.Json;

#nullable disable

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
            var jsonString = schemaFile.GetText().ToString();
            var schema = JsonSerializer.Deserialize<Schema>(jsonString);
            return schema;
        });

        var tablesProvider = schemaProvider.SelectMany((schema, cancellationToken) =>
        {
            return schema.Tables;
        });

        context.RegisterSourceOutput(schemaProvider, (sourceProductionContext, schema) =>
        {
            var repositories = new List<RepositoryGenerator>(schema.Tables.Length);
            foreach (var table in schema.Tables)
            {
                var parsedTable = new ParsedSchemaTable(table);

                var datFileGenerator = new DatFileGenerator(parsedTable);

                var repositoryGenerator = new RepositoryGenerator(parsedTable);
                repositories.Add(repositoryGenerator);

                sourceProductionContext.AddSource(datFileGenerator.FileName, datFileGenerator.Code);
                sourceProductionContext.AddSource(repositoryGenerator.FileName, repositoryGenerator.Code);
            }

            var specificationGenerator = new SpecificationFileGenerator(repositories);
            sourceProductionContext.AddSource(specificationGenerator.FileName, specificationGenerator.Code);

            Console.WriteLine();
        });
    }
}
