namespace PoeData.Generator.Columns;

internal sealed class UShortColumn : IColumn
{
    public string FullUnderlyingTypeName => "ushort";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
}
