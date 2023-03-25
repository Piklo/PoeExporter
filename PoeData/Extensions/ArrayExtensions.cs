using System.Numerics;

namespace PoeData.Extensions;

/// <summary>
/// Array extensions.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Finds the index of a sub array in a given array.
    /// </summary>
    /// <typeparam name="T">Type of both arrays.</typeparam>
    /// <param name="array">array to look in.</param>
    /// <param name="subArray">array to look for.</param>
    /// <param name="startIndex">start index.</param>
    /// <returns>Index of a sub array. -1 if not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown if either of the arrays is null.</exception>
    // https://stackoverflow.com/a/26880541
    public static int IndexOfSubArray<T>(this T[] array, T[] subArray, int startIndex = 0)
        where T : IEqualityOperators<T, T, bool>
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (subArray is null)
        {
            throw new ArgumentNullException(nameof(subArray));
        }

        var len = subArray.Length;
        var limit = array.Length - len;
        for (var i = startIndex; i <= limit; i++)
        {
            var k = 0;
            for (; k < len; k++)
            {
                if (subArray[k] != array[i + k])
                {
                    break;
                }
            }

            if (k == len)
            {
                return i;
            }
        }

        return -1;
    }
}
