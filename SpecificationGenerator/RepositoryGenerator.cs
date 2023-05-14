using Serilog;
using SpecificationGenerator.ColumnGenerators;
using System.Diagnostics;
using System.Text;

namespace SpecificationGenerator;

/// <summary>
/// Class used to generate repository files.
/// </summary>
internal sealed class RepositoryGenerator
{
    private const string Namespace = "PoeData.Specifications.Repositories";
    private readonly ILogger logger;
    private readonly ParsedSchemaTable parsedTable;
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
    /// <param name="parsedTable">Table to parse.</param>
    public RepositoryGenerator(ILogger logger, ParsedSchemaTable parsedTable)
    {
        this.logger = logger;
        this.parsedTable = parsedTable;

        datClassName = DatFileGenerator.GenerateClassName(parsedTable.Table.Name);

        ClassName = GenerateRepositoryClassName(this.parsedTable);
        FileName = GenerateFileName(this.parsedTable);
        Code = Generate();
    }

    /// <summary>
    /// Generates repository class name.
    /// </summary>
    /// <param name="parsedTable">parsed table for which the repository is generated.</param>
    /// <returns>class name string.</returns>
    internal static string GenerateRepositoryClassName(ParsedSchemaTable parsedTable)
    {
        var name = $"{parsedTable.Table.Name}Repository";

        return name;
    }

    /// <summary>
    /// Generates file name for the generated repository class.
    /// </summary>
    /// <param name="parsedTable">parsed table for which the repository is generated.</param>
    /// <returns>file name string.</returns>
    internal static string GenerateFileName(ParsedSchemaTable parsedTable)
    {
        var className = GenerateRepositoryClassName(parsedTable);
        var name = $"{className}.cs";

        return name;
    }

    private string Generate()
    {
        var startTimestamp = Stopwatch.GetTimestamp();
        var builder = new StringBuilder();

        builder.AppendLine($$"""
            using PoeData.Extensions;
            using PoeData.Specifications.DatFiles;
            using System.Collections.ObjectModel;

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
        AppendLoadMethod(builder, parsedTable.ParsedColumns);

        builder.AppendLine("""
            }
            """);

        var str = builder.ToString();

        logger.Verbose("generated {fileName} string in {elapsed}", FileName, Stopwatch.GetElapsedTime(startTimestamp));

        return str;
    }

    private void AppendFieldsForColumns(StringBuilder builder)
    {
        foreach (var column in parsedTable.ParsedColumns)
        {
            builder.AppendLine($"""
                    private Dictionary<{column.ClassPropertyUnderlyingType}, List<{datClassName}>>? {GenerateByFieldName(column)};
                """);
        }
    }

    private static string GenerateByFieldName(IParsedColumn column)
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
        foreach (var column in parsedTable.ParsedColumns)
        {
            AppendGetSingleMethod(builder, column);
            AppendGetManyMethod(builder, column);
        }
    }

    private void AppendGetSingleMethod(StringBuilder builder, IParsedColumn column)
    {
        var getManyMethodName = GenerateGetManyMethodName(column);
        builder.AppendLine($$"""

                /// <summary>
                /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
                /// </summary>
                /// <param name="key">key.</param>
                /// <param name="item">returned item if found.</param>
                /// <returns>true if item with a given key was found, false otherwise.</returns>
                public bool TryGetBy{{column.ClassPropertyName}}({{column.ClassPropertyUnderlyingType}}? key, out {{datClassName}}? item)
                {
                    if (key is null)
                    {
                        item = null;
                        return false;
                    }

                    if (!{{getManyMethodName}}(key, out var items))
                    {
                        item = null;
                        return false;
                    }

                    if (items.Count == 0)
                    {
                        logger.Warning("failed to find item with key = {key}", key.Value);
                        item = null;
                        return false;
                    }

                    if (items.Count > 1)
                    {
                        logger.Warning("found too many items with key = {key}", key.Value);
                        item = null;
                        return false;
                    }

                    item = items[0];
                    return true;
                }
            """);
    }

    private void AppendGetManyMethod(StringBuilder builder, IParsedColumn column)
    {
        var methodName = GenerateGetManyMethodName(column);
        var fieldName = GenerateByFieldName(column);

        builder.AppendLine($$"""

                /// <summary>
                /// Tries to get <see cref="{{datClassName}}"/> with <see cref="{{datClassName}}.{{column.ClassPropertyName}}"/> equal to a given key.
                /// </summary>
                /// <param name="key">key.</param>
                /// <param name="items">returned items if found.</param>
                /// <returns>true if item with a given key was found, false otherwise.</returns>
                public bool {{methodName}}({{column.ClassPropertyUnderlyingType}}? key, out IReadOnlyList<{{datClassName}}> items)
                {
                    if (key is null)
                    {
                        items = Array.Empty<{{datClassName}}>();
                        return false;
                    }

                    if ({{fieldName}} is null)
                    {
                        {{fieldName}} = new();
            """);

        AppendToDictionaryParsing(builder, column);

        builder.AppendLine($$"""
                    }

                    if (!{{fieldName}}.TryGetValue(key.Value, out var temp))
                    {
                        items = Array.Empty<{{datClassName}}>();
                        return false;
                    }

                    items = temp;
                    return true;
                }
            """);
    }

    private static string GenerateGetManyMethodName(IParsedColumn column)
    {
        return $"TryGetManyBy{column.ClassPropertyName}";
    }

    private static void AppendToDictionaryParsing(StringBuilder builder, IParsedColumn column)
    {
        var fieldName = GenerateByFieldName(column);
        var isNullable = column.ClassPropertyType.Contains('?');
        if (isNullable)
        {
            builder.AppendLine($$"""
                        foreach (var item in Items)
                        {
                            var itemKey = item.{{column.ClassPropertyName}};
                            if (itemKey is null)
                            {
                                continue;
                            }
            
                            if (!{{fieldName}}.TryGetValue(itemKey.Value, out var list))
                            {
                                list = new();
                                {{fieldName}}.TryAdd(itemKey.Value, list);
                            }
            
                            list.Add(item);
                        }
            """);
        }
        else
        {
            builder.AppendLine($$"""
                        foreach (var item in Items)
                        {
                            var itemKey = item.{{column.ClassPropertyName}};
            
                            if (!{{fieldName}}.TryGetValue(itemKey, out var list))
                            {
                                list = new();
                                {{fieldName}}.TryAdd(itemKey, list);
                            }
            
                            list.Add(item);
                        }
            """);
        }
    }

    private void AppendLoadMethod(StringBuilder builder, IReadOnlyList<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""

                    private {{datClassName}}[] Load()
                    {
                        const string filePath = "Data/{{parsedTable.Name}}.dat64";
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
                        var obj = new {{datClassName}}()
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
