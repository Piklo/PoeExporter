namespace PoeDataGenerator.ParsedColumns.Helpers;

/// <summary>
/// Struct storing data about the type for <see cref="IParsedColumn"/>.
/// </summary>
internal readonly record struct TypeData
{
    /// <summary>Gets string representation of the type.</summary>
    public required string Type { get; init; }

    /// <summary>Gets a value indicating whether type is nullable.</summary>
    public required bool IsNullable { get; init; }

    /// <summary>Gets a value indicating whether type is value type.</summary>
    public required bool IsValueType { get; init; }

    /// <summary>Gets a value indicating whether type is a list.</summary>
    public required bool IsList { get; init; }

    /// <summary>Gets inner types.</summary>
    public TypeData[] InnerTypes { get; init; } = Array.Empty<TypeData>();

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeData"/> class.
    /// </summary>
    public TypeData()
    {
    }

    /// <summary>Gets <see cref="TypeData"/> for a bool.</summary>
    public static TypeData Bool = new()
    {
        Type = "bool",
        IsNullable = false,
        IsValueType = true,
        IsList = false,
    };

    /// <summary>Gets <see cref="TypeData"/> for an int.</summary>
    public static TypeData Int = new()
    {
        Type = "int",
        IsNullable = false,
        IsValueType = true,
        IsList = false,
    };

    /// <summary>Gets <see cref="TypeData"/> for a nullable int.</summary>
    public static TypeData NullableInt = new()
    {
        Type = "int?",
        IsNullable = true,
        IsValueType = true,
        IsList = false,
    };

    /// <summary>Gets <see cref="TypeData"/> for ReadOnlyCollection of ints.</summary>
    public static TypeData ReadonlyCollectionInt = new()
    {
        Type = "ReadOnlyCollection<int>",
        IsNullable = false,
        IsValueType = false,
        IsList = true,
        InnerTypes = new TypeData[] { new() { Type = "int", IsNullable = false, IsValueType = true, IsList = false } },
    };

    /// <summary>Gets <see cref="TypeData"/> for a float.</summary>
    public static TypeData Float = new()
    {
        Type = "float",
        IsNullable = false,
        IsValueType = true,
        IsList = false,
    };

    /// <summary>Gets <see cref="TypeData"/> for ReadOnlyCollection of floats.</summary>
    public static TypeData ReadonlyCollectionFloat = new()
    {
        Type = "ReadOnlyCollection<float>",
        IsNullable = false,
        IsValueType = false,
        IsList = true,
        InnerTypes = new TypeData[] { new() { Type = "float", IsNullable = false, IsValueType = true, IsList = false } },
    };

    /// <summary>Gets <see cref="TypeData"/> for a string.</summary>
    public static TypeData String = new()
    {
        Type = "string",
        IsNullable = false,
        IsValueType = false,
        IsList = false,
    };

    /// <summary>Gets <see cref="TypeData"/> for ReadOnlyCollection of strings.</summary>
    public static TypeData ReadonlyCollectionString = new()
    {
        Type = "ReadOnlyCollection<string>",
        IsNullable = false,
        IsValueType = false,
        IsList = true,
        InnerTypes = new TypeData[] { new() { Type = "string", IsNullable = false, IsValueType = false, IsList = false } },
    };
}
