using Microsoft.CodeAnalysis;
using PoeDataGenerator.DatFiles;
using PoeDataGenerator.GeneratorHelpers;
using PoeDataGenerator.RepositoryGenerators;
using PoeDataGenerator.SchemaJson;
using PoeDataGenerator.Specification;
using System.Collections.Immutable;
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

        var collectedParsedTablesProvider = parsedTablesProvider.Collect();

        var tableReferenceDataProvider = collectedParsedTablesProvider.SelectMany((tables, cancellationToken) =>
        {
            var dict = new Dictionary<string, HashSet<string>>();

            foreach (var table in tables)
            {
                foreach (var column in table.ParsedColumns)
                {
                    if (column.ReferencedTable is null || column.ReferencedColumn is null)
                    {
                        continue;
                    }

                    if (!dict.TryGetValue(column.ReferencedTable, out var set) || set is null)
                    {
                        set = new HashSet<string>();
                        dict.Add(column.ReferencedTable, set);
                    }

                    set.Add(column.ReferencedColumn);
                }
            }

            var results = new List<ParsedSchemaWithReferenceData>(tables.Length);

            var test = new HashSet<string>();
            var emptyHashSet = test.ToImmutableHashSet();
            foreach (var table in tables)
            {
                var tableName = table.Name;

                if (dict.TryGetValue(tableName, out var referencedColumns) && referencedColumns is not null)
                {
                    var parsedTable = new ParsedSchemaWithReferenceData()
                    {
                        Table = table,
                        ReferencedColumns = referencedColumns.ToImmutableHashSet(),
                    };
                    results.Add(parsedTable);
                }
                else
                {
                    var tableReferenceData = new ParsedSchemaWithReferenceData()
                    {
                        Table = table,
                        ReferencedColumns = emptyHashSet,
                    };
                    results.Add(tableReferenceData);
                }
            }

            return results;
        });

        context.RegisterSourceOutput(tableReferenceDataProvider, (sourceProductionContext, tableReferenceData) =>
        {
            var repositoryGenerator = new RepositoryGenerator(tableReferenceData);
            sourceProductionContext.AddSource(repositoryGenerator.FileName, repositoryGenerator.Code);
        });

        var collectedTablesProvider = tablesProvider.Collect();

        context.RegisterSourceOutput(collectedTablesProvider, (sourceProductionContext, tables) =>
        {
            var specificationGenerator = new SpecificationFileGenerator(tables);
            sourceProductionContext.AddSource(specificationGenerator.FileName, specificationGenerator.Code);
        });
    }
}
