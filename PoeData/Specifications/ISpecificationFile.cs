namespace PoeData.Specifications;

/// <summary>
/// Interface containing base dat file methods.
/// </summary>
/// <typeparam name="T">Type of the dat file.</typeparam>
public interface ISpecificationFile<T>
{
    /// <summary>
    /// Loads dat file.
    /// </summary>
    /// <param name="specification">Instance of <see cref="Specification"/> containing specification files.</param>
    /// <returns>Array of <see cref="{T}"/>.</returns>
    internal static abstract T[] Load(Specification specification);
}
