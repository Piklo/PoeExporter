namespace PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
internal class PredicateGreaterThan : IDescriptionLinePredicate
{
    public int MinValue { get; }

    public PredicateGreaterThan(int minValue)
    {
        MinValue = minValue;
    }

    public bool Matches(int value)
    {
        return value >= MinValue;
    }
}
