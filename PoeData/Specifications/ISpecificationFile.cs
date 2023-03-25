namespace PoeData.Specifications;

/// <summary>
/// Interface containing base specification file methods.
/// </summary>
/// <typeparam name="T">Type of the specification file.</typeparam>
public interface ISpecificationFile<T>
{
    /// <summary>
    /// Loads specification file.
    /// </summary>
    /// <param name="specification">Instance of <see cref="Specification"/> containing specification files.</param>
    /// <returns>Array of <see cref="{T}"/>.</returns>
    internal static abstract T[] Load(Specification specification);
}
