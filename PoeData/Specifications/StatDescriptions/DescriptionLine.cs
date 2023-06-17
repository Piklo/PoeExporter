namespace PoeData.Specifications.StatDescriptions;

internal sealed record DescriptionLine
{
    private readonly Func<object, bool>[] predicates;

    /// <summary>Gets description line.</summary>
    internal string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DescriptionLine"/> class.
    /// </summary>
    /// <param name="descriptionLine">stat description line.</param>
    internal DescriptionLine(string descriptionLine)
    {
        Value = descriptionLine;
        predicates = Array.Empty<Func<object, bool>>();
    }
}
