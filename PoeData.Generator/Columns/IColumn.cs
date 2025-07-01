namespace PoeData.Generator.Columns;

internal interface IColumn
{
    string FullUnderlyingTypeName { get; }
    string FullExposedTypeName { get; }
    string PropertyName { get; }
}
