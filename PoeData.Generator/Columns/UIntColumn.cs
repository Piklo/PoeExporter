namespace PoeData.Generator.Columns;

internal sealed class UIntColumn : IColumn
{
    public string FullUnderlyingTypeName => "uint";
    public string FullExposedTypeName => FullUnderlyingTypeName;
    public required string PropertyName { get; init; }
}
