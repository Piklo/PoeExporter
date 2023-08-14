namespace PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
internal class PredicateInRange : IDescriptionLinePredicate
{
    public int MinValue { get; }

    public int MaxValue { get; }

    public PredicateInRange(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public bool Matches(int value)
    {
        return value >= MinValue && value <= MaxValue;
    }
}
