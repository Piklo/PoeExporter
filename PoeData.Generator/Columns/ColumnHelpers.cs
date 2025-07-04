using System;

namespace PoeData.Generator.Columns;

internal static class ColumnHelpers
{
    public static IColumn GetParsedColumn(Column column, string globalNamespace)
    {
        return column switch
        {
            { Array: false, Type: "bool" } => new BoolColumn() { PropertyName = column.Name },
            { Array: true, Type: "bool" } => new BoolArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "i16" } => new ShortColumn() { PropertyName = column.Name },
            { Array: true, Type: "i16" } => new ShortArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "i32" } => new IntColumn() { PropertyName = column.Name },
            { Array: true, Type: "i32" } => new IntArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "u16" } => new UShortColumn() { PropertyName = column.Name },
            { Array: true, Type: "ui16" } => new UShortArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "u32" } => new UIntColumn() { PropertyName = column.Name },
            { Array: true, Type: "u32" } => new UIntArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "f32" } => new FloatColumn() { PropertyName = column.Name },
            { Array: true, Type: "f32" } => new FloatArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "string" } => new StringColumn() { PropertyName = column.Name },
            { Array: true, Type: "string" } => new StringArrayColumn() { PropertyName = column.Name },
            { Array: false, Type: "foreignrow", References: not null } => new ForeignRowColumn(globalNamespace)
                { PropertyName = column.Name, ReferencedTable = column.References.Table, ReferencedColumn = column.References.Column },
            _ => throw new NotImplementedException($"Not implemented column type.")
        };
    }
}
