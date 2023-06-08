namespace System.Runtime.CompilerServices;

/// <summary>
/// CS0656 Missing compiler required member.
/// </summary>
public class CompilerFeatureRequiredAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompilerFeatureRequiredAttribute"/> class.
    /// </summary>
    /// <param name="name">name.</param>
#pragma warning disable IDE0060 // Remove unused parameter
    public CompilerFeatureRequiredAttribute(string name)
#pragma warning restore IDE0060 // Remove unused parameter
    {
    }
}