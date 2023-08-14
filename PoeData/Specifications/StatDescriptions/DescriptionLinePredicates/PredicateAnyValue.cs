namespace PoeData.Specifications.StatDescriptions.DescriptionLinePredicates;
internal class PredicateAnyValue : IDescriptionLinePredicate
{
    public bool Matches(int value)
    {
        return true;
    }
}
