namespace PoeData.Generator.Columns;

internal sealed class ShortColumn : IColumn
{
    public string FullUnderlyingTypeName => "short";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
    public int Size => 2;
}
