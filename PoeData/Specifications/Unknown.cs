using System.Numerics;

namespace PoeData.Specifications;

/// <summary>
/// Struct containing the value for variables with unknown name.
/// </summary>
/// <typeparam name="T">Type of the underlying variable.</typeparam>
public readonly struct Unknown<T> : IEquatable<Unknown<T>>
    where T : notnull, IEqualityOperators<T, T, bool>
{
    private readonly T value;

    private readonly int columnOffset;

    /// <summary>Gets value unsafely.</summary>
    public T ValueUnsafe { get => value; }

    /// <summary>
    /// Safely gets value from the unknown.
    /// </summary>
    /// <param name="columnOffset">column offset the value is expected to be at.</param>
    /// <returns>value of the unknown variable.</returns>
    /// <exception cref="WrongOffsetException">thrown when wrong column offset is provided when trying to get value.</exception>
    public T GetValue(int columnOffset)
    {
        if (columnOffset != this.columnOffset)
        {
            throw new WrongOffsetException($"provided {nameof(columnOffset)} != expected {nameof(this.columnOffset)}");
        }

        return value;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj != null && obj is Unknown<T> other && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    /// <inheritdoc/>
    public bool Equals(Unknown<T> other)
    {
        return value == other.value;
    }

    public static bool operator ==(Unknown<T> left, Unknown<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Unknown<T> left, Unknown<T> right)
    {
        return !(left == right);
    }
}
