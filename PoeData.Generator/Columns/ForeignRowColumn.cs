namespace PoeData.Generator.Columns;

internal sealed class ForeignRowColumn : IColumn
{
    public string FullUnderlyingTypeName => "float";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
    public required string ReferencedTable { get; init; }
    public required string? ReferencedColumn { get; init; }
    public string ReferencedTableFullType { get; }
    public ForeignRowColumn(string globalNamespace)
    {
        ReferencedTableFullType = $"{globalNamespace}.{ReferencedTable}";
    }
}
