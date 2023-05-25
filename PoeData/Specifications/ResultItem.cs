namespace PoeData.Specifications;

public readonly record struct ResultItem<TKey, TValue>(TKey Key, TValue Value);
