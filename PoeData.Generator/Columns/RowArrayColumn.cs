namespace PoeData.Generator.Columns;

internal sealed class RowArrayColumn : IColumn
{
    public string FullUnderlyingTypeName => "int[]";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
    public int Size => 16;
}
