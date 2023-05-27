using PoeDataGenerator;

namespace PoeDataGenerator.RepositoryGenerators;

/// <summary>
/// Interface containing methods for columns required by repository generator.
/// </summary>
internal interface IRepositoryColumn
{
    /// <summary>
    /// Gets lines of code for repository get single method.
    /// </summary>
    /// <param name="datClassName">name of the dat class.</param>
    /// <returns>parsed lines of code.</returns>
    public IReadOnlyList<LineOfCode> GetSingle(string datClassName);

    /// <summary>
    /// Gets lines of code for repository get many method.
    /// </summary>
    /// <param name="datClassName">name of the dat class.</param>
    /// <param name="fieldName">name of the field name in repository class.</param>
    /// <returns>parsed lines of code.</returns>
    public IReadOnlyList<LineOfCode> GetMany(string datClassName, string fieldName);

    /// <summary>
    /// Gets lines of code for repository get many to manymethod.
    /// </summary>
    /// <param name="datClassName">name of the dat class.</param>
    /// <param name="fieldName">name of the field name in repository class.</param>
    /// <returns>parsed lines of code.</returns>
    public IReadOnlyList<LineOfCode> GetManyToMany(string datClassName, string fieldName);
}
