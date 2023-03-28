namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Interface containing methods and properties for parsed column classes.
/// </summary>
internal interface IParsedColumn
{
    /// <summary>
    /// Gets column offset.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Gets an array of not indented strings related to class properties for a given column.
    /// </summary>
    /// <returns>an array of strings.</returns>
    public string[] GetPropertyStrings();

    /// <summary>
    /// Gets an array of not indented strings related to loading referenced tables for a given column.
    /// </summary>
    /// <returns>an array of strings.</returns>
    public string[] GetReferencedTablesLoading();

    /// <summary>
    /// Gets an array of not indented strings related to loading the property data in LoadXyz method for a given column.
    /// </summary>
    /// <returns>an array of strings.</returns>
    public string[] GetLoading();

    /// <summary>
    /// Gets an array of not indented strings related to creating the instance of a class for a given column.
    /// </summary>
    /// <returns>an array of strings.</returns>
    public string[] GetObjectInitialization();
}
