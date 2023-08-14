namespace PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
internal class PredicateLessThan : IDescriptionLinePredicate
{
    public int MaxValue { get; }

    public PredicateLessThan(int maxValue)
    {
        MaxValue = maxValue;
    }

    public bool Matches(int value)
    {
        return value <= MaxValue;
    }
}
