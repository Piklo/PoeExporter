namespace PoeData.Generator.Columns;

internal sealed class BoolColumn : IColumn
{
    public string FullUnderlyingTypeName => "bool";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
}
