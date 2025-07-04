namespace PoeData.Generator.Columns;

internal sealed class ArrayColumn : IColumn
{
    public string FullUnderlyingTypeName => "byte[]";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
    public int Size => 16;
}
