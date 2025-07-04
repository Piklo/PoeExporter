namespace PoeData.Generator.Columns;

internal sealed class StringArrayColumn : IColumn
{
    public string FullUnderlyingTypeName => "string[]";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
}
