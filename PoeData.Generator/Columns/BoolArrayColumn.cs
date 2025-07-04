namespace PoeData.Generator.Columns;

internal sealed class BoolArrayColumn : IColumn
{
    public string FullUnderlyingTypeName => "bool[]";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
}
