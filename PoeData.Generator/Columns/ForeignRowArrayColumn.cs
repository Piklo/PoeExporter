namespace PoeData.Generator.Columns;

internal sealed class ForeignRowArrayColumn : IColumn
{
    public string FullUnderlyingTypeName => "int[]";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
    public required string ReferencedTable { get; init; }
    public required string? ReferencedColumn { get; init; }
    public string ReferencedTableFullType { get; }
    public int Size => 16;
    public ForeignRowArrayColumn(string globalNamespace)
    {
        ReferencedTableFullType = $"{globalNamespace}.{ReferencedTable}";
    }
}
