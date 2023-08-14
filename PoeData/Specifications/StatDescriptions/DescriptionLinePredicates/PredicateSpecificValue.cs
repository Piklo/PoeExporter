namespace PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
internal class PredicateSpecificValue : IDescriptionLinePredicate
{
    public int Value { get; }

    public PredicateSpecificValue(int value)
    {
        Value = value;
    }

    public bool Matches(int value)
    {
        return value == Value;
    }
}
