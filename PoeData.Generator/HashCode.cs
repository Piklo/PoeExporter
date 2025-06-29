using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PoeData.Generator;

internal sealed class HashCode
{
    private int _hash;

    public void Add<T>(T? value)
    {
        if (value is null)
        {
            return;
        }

        AddInternal(value);
    }

    [OverloadResolutionPriority(1)]
    public void Add<T>(IEnumerable<T>? values)
    {
        if (values is null)
        {
            return;
        }

        foreach (var value in values)
        {
            AddInternal(value);
        }
    }

    private void AddInternal<T>(T value)
    {
        _hash = _hash * -1521134295 + EqualityComparer<T>.Default.GetHashCode(value);
    }

    public int ToHashCode()
    {
        return _hash;
    }
}
