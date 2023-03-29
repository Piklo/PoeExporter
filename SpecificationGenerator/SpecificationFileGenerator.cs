﻿using Serilog;
using SpecificationGenerator.ColumnGenerators;
using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SpecificationGenerator;

/// <summary>
/// Class containing parsed table data.
/// </summary>
internal sealed class SpecificationFileGenerator
{
    private readonly Table table;
    private readonly ILogger logger;
    private readonly StringBuilder builder;
    private readonly string className;
    internal static readonly string Tab = new(' ', 4);
    internal static readonly string Tab2 = new(' ', 8);
    internal static readonly string Tab3 = new(' ', 12);
    internal static readonly string Tab4 = new(' ', 16);

    /// <summary>
    /// Initializes a new instance of the <see cref="SpecificationFileGenerator"/> class.
    /// </summary>
    /// <param name="table">Table to parse.</param>
    /// <param name="logger">logger.</param>
    public SpecificationFileGenerator(Table table, ILogger logger)
    {
        this.table = table;
        this.logger = logger;

        // parsing is done here
        builder = new StringBuilder();

        className = table.Name;
        var datFileName = $"{table.Name}.dat";

        builder.AppendLine("// <auto-generated />");
        builder.AppendLine("// this file is auto generated");
        builder.AppendLine("// the generated class is partial, please extend it in another file");
        builder.AppendLine("#nullable enable\n");

        builder.AppendLine("using PoeData.Extensions;");
        builder.AppendLine("using System.Collections.ObjectModel;");
        builder.AppendLine("using System.Text;");
        builder.AppendLine();
        builder.AppendLine($$"""
                namespace PoeData.Specifications.SpecificationFiles;

                /// <summary>
                /// Class containing {{datFileName}} data.
                /// </summary>
                public sealed partial class {{className}} : ISpecificationFile<{{className}}>
                {
                """);

        var parsedColumns = ParseColumns(table);

        AppendProperties(builder, parsedColumns);

        AppendLoadMethod(builder, parsedColumns);

        builder.AppendLine("}");

        var str = builder.ToString();
        logger.Debug(datFileName);
    }

    /// <summary>
    /// Gets the string of a generated class.
    /// </summary>
    /// <returns>generated class string.</returns>
    public string GetFileString()
    {
        return builder.ToString();
    }

    private ReadOnlyCollection<IParsedColumn> ParseColumns(Table table)
    {
        var result = new List<IParsedColumn>();
        var readonlyResult = result.AsReadOnly();

        foreach (var column in table.Columns)
        {
            try
            {
                var parsedColumn = ParseColumn(column, readonlyResult);
                result.Add(parsedColumn);
            }
            catch (NotImplementedColumnException e)
            {
                logger.Error(e.Message);
            }
        }

        return readonlyResult;
    }

    private static IParsedColumn ParseColumn(Column column, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        if (column.Type == "bool" && !column.Array)
        {
            return new BoolNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "i32" && !column.Array)
        {
            return new IntNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "i32" && column.Array)
        {
            return new IntArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "f32" && !column.Array)
        {
            return new FloatNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "f32" && column.Array)
        {
            return new FloatArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "foreignrow" && !column.Array)
        {
            return new ForeignrowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "string" && !column.Array)
        {
            return new StringNonArrayColumn(column, parsedColumns);
        }
        else
        {
            var serialized = JsonSerializer.Serialize(column, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            });

            var message = $"dont know how to load column: \n{serialized}";

            throw new NotImplementedColumnException(message);
        }
    }

    private static void AppendProperties(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        foreach (var column in parsedColumns)
        {
            var lines = column.GetPropertyStrings();

            foreach (var line in lines)
            {
                builder.AppendLine($"{Tab}{line}");
            }

            builder.AppendLine();
        }
    }

    private void AppendLoadMethod(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""
                    /// <inheritdoc/>
                    public static {{className}}[] Load(Specification specification)
                    {
                        if (specification is null)
                        {
                            throw new ArgumentNullException(nameof(specification));
                        }

                """);

        builder.AppendLine($$"""
                        var fileToFind = Encoding.ASCII.GetBytes("Data/{{table.Name}}.dat64");
                        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
                        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

                        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
                        const int TableOffset = 4;
                        var offset = 0;
                        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
                        var tableLength = dataOffset - TableOffset;
                        var tableRecordLength = tableLength / (int)tableRows;

                        var objects = new {{className}}[tableRows];
                        for (var rowId = 0; rowId < tableRows; rowId++)
                        {                    
                            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);
                """);

        AppendReferencedTablesLoading(builder, parsedColumns);

        AppendColumnsLoading(builder, parsedColumns);

        builder.AppendLine($$"""
                        if (offset != expectedOffset)
                        {
                            throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
                        }

            """);

        AppendObjectInitialization(builder, parsedColumns);

        // loop ends here
        builder.AppendLine($$"""{{Tab2}}}""");

        builder.AppendLine("""

                    return objects;
                }
            """);
    }

    private static void AppendReferencedTablesLoading(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($"{Tab3}// loading referenced tables if any");
        var strings = ColumnGeneratorHelper.GetReferencedTablesLoading(parsedColumns);
        foreach (var str in strings)
        {
            builder.AppendLine($"{Tab3}{str}");
        }
    }

    private static void AppendColumnsLoading(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        foreach (var column in parsedColumns)
        {
            var columnLoadingStrings = column.GetLoading();

            foreach (var str in columnLoadingStrings)
            {
                builder.AppendLine($"{Tab3}{str}");
            }

            builder.AppendLine();
        }
    }

    private void AppendObjectInitialization(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""
                        var obj = new {{className}}()
                        {
            """);

        var strings = ColumnGeneratorHelper.GetObjectInitialization(parsedColumns);
        foreach (var str in strings)
        {
            builder.AppendLine($"{Tab4}{str}");
        }

        builder.AppendLine("""
                        };

                        objects[rowId] = obj;
            """);
    }
}
