using System.Collections.Generic;
using System.Linq;

namespace PoeData.Generator;

internal static class EqualityExtensions
{
    public static bool SequenceEqual<T>(IEnumerable<T>? first, IEnumerable<T>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.SequenceEqual(second);
    }
}
