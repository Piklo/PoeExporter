using System;
using PoeData.Generator.Columns;

namespace PoeData.Generator;

internal static class ColumnHelpers
{
    public static IColumn GetParsedColumn(Column column, string globalNamespace, int offset, string tableName)
    {
        var name = column.Name ?? $"Unknown{offset}";

        name = name == tableName ? $"{name}_" : name;

        return column switch
        {
            { Array: false, Type: "bool" } => new BoolColumn() { PropertyName = name },
            { Array: true, Type: "bool" } => new BoolArrayColumn() { PropertyName = name },
            { Array: false, Type: "i16" } => new ShortColumn() { PropertyName = name },
            { Array: true, Type: "i16" } => new ShortArrayColumn() { PropertyName = name },
            { Array: false, Type: "i32" } => new IntColumn() { PropertyName = name },
            { Array: true, Type: "i32" } => new IntArrayColumn() { PropertyName = name },
            { Array: false, Type: "u16" } => new UShortColumn() { PropertyName = name },
            { Array: true, Type: "ui16" } => new UShortArrayColumn() { PropertyName = name },
            { Array: false, Type: "u32" } => new UIntColumn() { PropertyName = name },
            { Array: true, Type: "u32" } => new UIntArrayColumn() { PropertyName = name },
            { Array: false, Type: "f32" } => new FloatColumn() { PropertyName = name },
            { Array: true, Type: "f32" } => new FloatArrayColumn() { PropertyName = name },
            { Array: false, Type: "string" } => new StringColumn() { PropertyName = name },
            { Array: true, Type: "string" } => new StringArrayColumn() { PropertyName = name },
            { Array: false, Type: "foreignrow", References: not null } => new ForeignRowColumn(globalNamespace)
            { PropertyName = name, ReferencedTable = column.References.Table, ReferencedColumn = column.References.Column },
            { Array: true, Type: "foreignrow", References: not null } => new ForeignRowArrayColumn(globalNamespace)
            { PropertyName = name, ReferencedTable = column.References.Table, ReferencedColumn = column.References.Column },
            { Array: false, Type: "foreignrow", References: null } => new UnknownForeignRowColumn() { PropertyName = name },
            { Array: true, Type: "foreignrow", References: null } => new UnknownForeignRowArrayColumn() { PropertyName = name },
            { Array: true, Type: "array" } => new ArrayColumn() { PropertyName = name },
            { Array: false, Type: "enumrow" } => new EnumRowColumn() { PropertyName = name },
            { Array: true, Type: "enumrow" } => new EnumRowArrayColumn() { PropertyName = name },
            { Array: false, Type: "row" } => new RowColumn() { PropertyName = name },
            { Array: true, Type: "row" } => new RowArrayColumn() { PropertyName = name },
            _ => throw new NotImplementedException($"Not implemented column type."),
        };
    }
}
