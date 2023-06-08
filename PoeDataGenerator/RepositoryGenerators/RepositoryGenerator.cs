﻿using PoeDataGenerator.GeneratorHelpers;
using PoeDataGenerator.ParsedColumns.Helpers;
using System.Text;

namespace PoeDataGenerator.RepositoryGenerators;

/// <summary>
/// Class used to generate repository files.
/// </summary>
internal sealed class RepositoryGenerator
{
    private const string Namespace = "PoeData.Specifications.Repositories";
    private readonly ParsedSchemaWithReferenceData tableReferenceData;
    private readonly string datClassName;

    /// <summary>Gets string with class name.</summary>
    internal string ClassName { get; }

    /// <summary>Gets generated code.</summary>
    public string Code { get; }

    /// <summary>Gets file name.</summary>
    public string FileName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryGenerator"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="tableReferenceData">referenced tables data.</param>
    public RepositoryGenerator(ParsedSchemaWithReferenceData tableReferenceData)
    {
        this.tableReferenceData = tableReferenceData;

        datClassName = DatFileGenerator.GenerateClassName(this.tableReferenceData.Table.Name);

        ClassName = GenerateRepositoryClassName(this.tableReferenceData.Table.Name);
        FileName = GenerateFileName(this.tableReferenceData.Table.Name);
        Code = Generate();
    }

    /// <summary>
    /// Generates repository class name.
    /// </summary>
    /// <param name="tableName">name of the table for which the repository is generated.</param>
    /// <returns>class name string.</returns>
    internal static string GenerateRepositoryClassName(string tableName)
    {
        var name = $"{tableName}Repository";

        return name;
    }

    /// <summary>
    /// Generates file name for the generated repository class.
    /// </summary>
    /// <param name="tableName">name of the table for which the repository is generated.</param>
    /// <returns>file name string.</returns>
    internal static string GenerateFileName(string tableName)
    {
        var className = GenerateRepositoryClassName(tableName);
        var name = $"{className}.g.cs";

        return name;
    }

    private string Generate()
    {
        var builder = new StringBuilder();

        builder.AppendLine($$"""
            // <auto-generated/>
            using PoeData.Extensions;
            using PoeData.Specifications.DatFiles;
            using System.Collections.ObjectModel;
            using Serilog;
            #nullable enable

            namespace {{Namespace}};

            /// <summary>
            /// Class containing <see cref="{{datClassName}}"/> related data and helper methods.
            /// </summary>
            public sealed class {{ClassName}}
            {
                private readonly ILogger logger;
                private readonly Specification specification;

                /// <summary>Gets items.</summary>
                public ReadOnlyCollection<{{datClassName}}> Items { get; }
            """);

        AppendFieldsForColumns(builder);
        AppendConstructor(builder);
        AppendGetMethods(builder);
        AppendLoadMethod(builder, tableReferenceData.Table.ParsedColumns);

        builder.AppendLine("""
            }
            """);

        var str = builder.ToString();

        return str;
    }

    private void AppendFieldsForColumns(StringBuilder builder)
    {
        foreach (var column in tableReferenceData.Table.ParsedColumns)
        {
            if (!tableReferenceData.ReferencedColumns.Contains(column.ClassPropertyName))
            {
                continue;
            }

            var type = column.Type.InnerTypes.Length != 0 ? column.Type.InnerTypes[0].Type : column.Type.Type;

            builder.AppendLine($"""

                    private Dictionary<{type}, {datClassName}>? {GenerateSingleByFieldName(column)};
                """);
        }
    }

    private static string GenerateSingleByFieldName(IParsedColumn column)
    {
        return $"by{column.ClassPropertyName}";
    }

    private void AppendConstructor(StringBuilder builder)
    {
        builder.AppendLine($$"""

                /// <summary>
                /// Initializes a new instance of the <see cref="{{ClassName}}"/> class.
                /// </summary>
                /// <param name="logger">logger.</param>
                /// <param name="specification">specification.</param>
                internal {{ClassName}}(ILogger logger, Specification specification)
                {
                    this.logger = logger;
                    this.specification = specification;
                    Items = Load().AsReadOnly();
                }
            """);
    }

    private void AppendGetMethods(StringBuilder builder)
    {
        foreach (var column in tableReferenceData.Table.ParsedColumns)
        {
            if (!tableReferenceData.ReferencedColumns.Contains(column.ClassPropertyName))
            {
                continue;
            }

            AppendGetSingleMethod(builder, column);
        }
    }

    private void AppendGetSingleMethod(StringBuilder builder, IParsedColumn column)
    {
        builder.AppendLine();
        var fieldName = GenerateSingleByFieldName(column);
        var code = RepositoryGetMethodsHelper.GetSingleMethod(datClassName, fieldName, column);
        foreach (var line in code)
        {
            if (string.IsNullOrWhiteSpace(line.Value))
            {
                builder.AppendLine();
                continue;
            }

            builder.AppendLine($"{new string(' ', (line.Indentation + 1) * 4)}{line.Value}");
        }
    }

    private void AppendLoadMethod(StringBuilder builder, IReadOnlyList<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""

                    private {{datClassName}}[] Load()
                    {
                        const string filePath = "Data/{{tableReferenceData.Table.Name}}.dat64";
                        var dataLoader = specification.DataLoader;
                        var decompressedFile = dataLoader.GetFileBytes(filePath);

                        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
                        const int TableOffset = 4;
                        var offset = 0;
                        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
                        var tableLength = dataOffset - TableOffset;
                        var tableRecordLength = tableLength / (int)tableRows;

                        var objects = new {{datClassName}}[tableRows];
                        for (var rowId = 0; rowId < tableRows; rowId++)
                        {
                            // offset = 4 + (rowId * tableRecordLength); // debug only
                            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

                """);

        AppendColumnsLoading(builder, parsedColumns);

        builder.AppendLine($$"""
                        if (offset != expectedOffset)
                        {
                            throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
                        }

            """);

        AppendObjectInitialization(builder, parsedColumns);

        // loop ends here
        builder.AppendLine($$"""{{Tabs.Tab2}}}""");

        builder.AppendLine("""

                    return objects;
                }
            """);
    }

    private static void AppendColumnsLoading(StringBuilder builder, IReadOnlyList<IParsedColumn> parsedColumns)
    {
        foreach (var column in parsedColumns)
        {
            var columnLoadingStrings = column.GetLoading();

            foreach (var str in columnLoadingStrings)
            {
                builder.AppendLine($"{Tabs.Tab3}{str}");
            }

            builder.AppendLine();
        }
    }

    private void AppendObjectInitialization(StringBuilder builder, IReadOnlyList<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""
                        var obj = new {{datClassName}}(specification)
                        {
            """);

        var strings = ColumnGeneratorHelper.GetObjectInitialization(parsedColumns);
        foreach (var str in strings)
        {
            builder.AppendLine($"{Tabs.Tab4}{str}");
        }

        builder.AppendLine("""
                        };

                        objects[rowId] = obj;
            """);
    }
}
