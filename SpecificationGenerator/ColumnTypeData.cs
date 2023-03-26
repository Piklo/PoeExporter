namespace SpecificationGenerator;

internal readonly struct ColumnTypeData
{
    public readonly required string Value { get; init; }

    public readonly required string BaseColumnType { get; init; }

    public readonly required bool IsUnknown { get; init; }

    public readonly required bool IsArray { get; init; }

    public readonly required bool IsForeignRow { get; init; }
}