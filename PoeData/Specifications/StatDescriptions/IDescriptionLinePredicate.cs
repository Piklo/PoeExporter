namespace PoeData.Specifications.StatDescriptions;

internal interface IDescriptionLinePredicate
{
    public bool Matches(int value);
}
