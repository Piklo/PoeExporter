namespace PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
internal class PredicateNotSpecificValue : IDescriptionLinePredicate
{
    public int Value { get; }

    public PredicateNotSpecificValue(int value)
    {
        Value = value;
    }

    public bool Matches(int value)
    {
        return value != Value;
    }
}
