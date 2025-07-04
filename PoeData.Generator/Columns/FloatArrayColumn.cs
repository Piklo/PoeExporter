namespace PoeData.Generator.Columns;

internal sealed class FloatArrayColumn : IColumn
{
    public string FullUnderlyingTypeName => "float[]";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
}
