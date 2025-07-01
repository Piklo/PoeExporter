using System;

namespace PoeData.Generator.Columns;

internal static class ColumnHelpers
{
    public static string GetColumnType(Column column)
    {
        return column switch
        {
            { Array: false, Type: "bool" } => "bool",
            { Array: true, Type: "bool" } => "bool[]",
            { Array: false, Type: "i16" } => "short",
            { Array: true, Type: "i16" } => "short[]",
            { Array: false, Type: "i32" } => "int",
            { Array: true, Type: "i32" } => "int[]",
            { Array: false, Type: "u16" } => "ushort",
            { Array: true, Type: "ui16" } => "ushort[]",
            { Array: false, Type: "u32" } => "uint",
            { Array: true, Type: "u32" } => "uint[]",
            { Array: false, Type: "f32" } => "float",
            { Array: true, Type: "f32" } => "float[]",
            { Array: false, Type: "string" } => "string",
            { Array: true, Type: "string" } => "string[]",
            _ => throw new NotImplementedException($"Not implemented column type.")
        };
    }

    public static IColumn GetParsedColumn(Column column)
    {
        throw new NotImplementedException();
    }
}
