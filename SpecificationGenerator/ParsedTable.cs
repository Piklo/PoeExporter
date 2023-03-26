﻿using Serilog;
using SpecificationGenerator.SchemaJson;
using System.Text;

namespace SpecificationGenerator;

/// <summary>
/// Class containing parsed table data.
/// </summary>
internal class ParsedTable
{
    private readonly Table table;
    private readonly ILogger logger;
    private readonly StringBuilder builder;
    private readonly ColumnTypeData[] parsedColumnTypeData;
    private readonly string[] columnNames;
    private readonly string className;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParsedTable"/> class.
    /// </summary>
    /// <param name="table">Table to parse.</param>
    /// <param name="logger">logger.</param>
    public ParsedTable(Table table, ILogger logger)
    {
        this.table = table;
        this.logger = logger;

        // parsing is done here
        builder = new StringBuilder();

        className = TableNameToClassName(table.Name);
        var datFileName = $"{table.Name}.dat";

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

        (parsedColumnTypeData, columnNames) = AppendProperties();

        AppendLoadMethod();

        builder.AppendLine("}");

        var str = builder.ToString();
        logger.Debug("AbyssObjects");
    }

    private (ColumnTypeData[] parsedColumns, string[] columnNames) AppendProperties()
    {
        var unknownCounter = 0;
        var columnNames = new string[table.Columns.Length];
        var parsedColumns = new ColumnTypeData[table.Columns.Length];

        for (var i = 0; i < table.Columns.Length; i++)
        {
            var column = table.Columns[i];
            (var columnName, unknownCounter, var isUnknown) = GetColumnName(column, unknownCounter);
            var columnTypeData = GetColumnTypeData(column, isUnknown);
            columnNames[i] = columnName;
            parsedColumns[i] = columnTypeData;

            if (column.Type == "bool")
            {
                builder.AppendLine($"    /// <summary> Gets a value indicating whether {columnName} is set.</summary>");
            }
            else
            {
                builder.AppendLine($"    /// <summary> Gets {columnName}.</summary>");
            }

            builder.AppendLine($$"""
                        public required {{columnTypeData.Value}} {{columnName}} { get; init; }
                    """);

            builder.AppendLine();
        }

        return (parsedColumns, columnNames);
    }

    private static string TableNameToClassName(string tableName)
    {
        return tableName;

        // doesnt work with stuff like MonsterVarieties
        // var className = tableName[..^1];
        // return className;
    }

    private static ColumnTypeData GetColumnTypeData(Column column, bool isUnknown)
    {
        var result = string.Empty;
        var isArray = column.Array;
        var isForeignRow = false;
        var columnTypeString = column.Type;
        var baseColumnType = string.Empty;
        var columnType = ColumnTypes.None;

        if (isArray && isUnknown)
        {
            throw new NotImplementedException($"{nameof(isArray)} && {nameof(isUnknown)} == true"); // array of unknowns or unknown of arrays?
        }

        if (columnTypeString == "foreignrow")
        {
            var className = TableNameToClassName(column.References.Table);
            isForeignRow = true;
            columnType = ColumnTypes.ForeignRow;

            // non arrays need to be nullable
            // arrays will just have length 0 if necessary
            if (isArray)
            {
                baseColumnType = className;
            }
            else
            {
                baseColumnType = $"{className}?";
            }
        }
        else if (columnTypeString == "bool")
        {
            baseColumnType = "bool";
            columnType = ColumnTypes.Bool;
        }
        else if (columnTypeString == "i32")
        {
            baseColumnType = "int";
            columnType = ColumnTypes.Int;
        }
        else if (columnTypeString == "string")
        {
            baseColumnType = "string";
            columnType = ColumnTypes.String;
        }
        else if (columnTypeString == "row")
        {
            var className = TableNameToClassName(column.Name);
            baseColumnType = $"{className}?"; // references another row in the same class
            columnType = ColumnTypes.Row;
        }
        else if (columnTypeString == "f32")
        {
            baseColumnType = "float";
            columnType = ColumnTypes.Float;
        }
        else if (columnTypeString == "array")
        {
            baseColumnType = "null";
            columnType = ColumnTypes.None;
        }
        else if (columnTypeString == "enumrow")
        {
            var className = TableNameToClassName(column.References.Table);

            // can enumrow be null?
            // baseColumnType = $"{className}?";
            baseColumnType = className;

            isForeignRow = true;
            columnType = ColumnTypes.Enumrow;
        }
        else
        {
            throw new NotImplementedException($"unknown {nameof(columnTypeString)} == {columnTypeString}");
        }

        var actualColumnType = string.Empty;
        if (isArray)
        {
            actualColumnType = $"ReadOnlyCollection<{baseColumnType}>";
        }
        else if (isUnknown)
        {
            actualColumnType = $"Unknown<{baseColumnType}>";
        }
        else
        {
            actualColumnType = baseColumnType;
        }

        var columnTypeData = new ColumnTypeData()
        {
            Value = actualColumnType,
            BaseColumnType = baseColumnType,
            IsArray = isArray,
            IsForeignRow = isForeignRow,
            IsUnknown = isUnknown,
            ColumnType = columnType,
        };

        return columnTypeData;
    }

    private static (string columnName, int unknownCounter, bool isUnknown) GetColumnName(Column column, int unknownCounter)
    {
        if (column.Name is not null)
        {
            return (column.Name, unknownCounter, false);
        }

        var name = $"Unknown{unknownCounter}";
        unknownCounter++;

        return (name, unknownCounter, true);
    }

    private void AppendLoadMethod()
    {
        var str = builder.ToString();
    }
}
