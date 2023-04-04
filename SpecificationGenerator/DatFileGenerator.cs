﻿using Serilog;
using SpecificationGenerator.ColumnGenerators;
using SpecificationGenerator.SchemaJson;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SpecificationGenerator;

/// <summary>
/// Class containing parsed table data.
/// </summary>
internal sealed class DatFileGenerator
{
    private readonly Table table;
    private readonly ILogger logger;

    /// <summary>Gets string with class name.</summary>
    internal string ClassName { get; }

    /// <summary>Gets generated code.</summary>
    public string Code { get; }

    /// <summary>Gets file name.</summary>
    public string FileName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatFileGenerator"/> class.
    /// </summary>
    /// <param name="table">Table to parse.</param>
    /// <param name="logger">logger.</param>
    public DatFileGenerator(Table table, ILogger logger)
    {
        this.table = table;
        this.logger = logger;

        ClassName = GenerateClassName(table.Name);

        FileName = GenerateFileName(ClassName);

        Code = Generate();
    }

    /// <summary>
    /// Generates the class name for the table.
    /// </summary>
    /// <param name="tableName">Table name to generate the class name for.</param>
    /// <returns>class name string.</returns>
    internal static string GenerateClassName(string tableName)
    {
        return $"{tableName}Dat";
    }

    /// <summary>
    /// Generates the file name based on the class name.
    /// </summary>
    /// <param name="className">class name for which the method generates the file name for.</param>
    /// <returns>file name string.</returns>
    private static string GenerateFileName(string className)
    {
        return $"{className}.cs";
    }

    private string Generate()
    {
        var startTimestamp = Stopwatch.GetTimestamp();

        var builder = new StringBuilder();

        var datFileName = $"{table.Name}.dat";

        // builder.AppendLine("// <auto-generated />");
        builder.AppendLine($$"""
                // this file is auto generated
                // the generated class is partial, please extend it in another file
                #nullable enable

                using PoeData.Extensions;
                using System.Collections.ObjectModel;
                using System.Text;

                namespace PoeData.Specifications.Dat;

                /// <summary>
                /// Class containing {{datFileName}} data.
                /// </summary>
                public sealed partial class {{ClassName}} : ISpecificationFile<{{ClassName}}>
                {
                """);

        var parsedColumns = ParseColumns(table);

        AppendProperties(builder, parsedColumns);

        AppendLoadMethod(builder, parsedColumns);

        builder.AppendLine("}"); // class end bracket

        var str = builder.ToString();

        logger.Verbose("generated {fileName} string in {elapsed}", FileName, Stopwatch.GetElapsedTime(startTimestamp));

        return str;
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
            return new ForeignRowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "foreignrow" && column.Array)
        {
            return new ForeignRowArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "string" && !column.Array)
        {
            return new StringNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "string" && column.Array)
        {
            return new StringArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "array" && column.Array)
        {
            return new ArrayArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "row" && !column.Array)
        {
            return new RowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "row" && column.Array)
        {
            return new RowArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "enumrow" && !column.Array)
        {
            return new EnumRowNonArrayColumn(column, parsedColumns);
        }
        else if (column.Type == "enumrow" && column.Array)
        {
            return new EnumRowArrayColumn(column, parsedColumns);
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
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                builder.AppendLine($"{Tabs.Tab1}{line}");
            }

            builder.AppendLine();
        }
    }

    private void AppendLoadMethod(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""
                    /// <inheritdoc/>
                    public static {{ClassName}}[] Load(Specification specification)
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

                        var objects = new {{ClassName}}[tableRows];
                        for (var rowId = 0; rowId < tableRows; rowId++)
                        {
                            // offset = 4 + (rowId * tableRecordLength); // debug only
                            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

                """);

        AppendColumnsLoading(builder, parsedColumns);

        builder.AppendLine($$"""
                        if (offset != expectedOffset)
                        {
                            throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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

    private static void AppendColumnsLoading(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
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

    private void AppendObjectInitialization(StringBuilder builder, ReadOnlyCollection<IParsedColumn> parsedColumns)
    {
        builder.AppendLine($$"""
                        var obj = new {{ClassName}}()
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