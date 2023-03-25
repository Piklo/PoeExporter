using PoeData.Extensions;
using System.Text;

namespace PoeData.Specifications;

/// <summary>
/// Helper class to load specification files.
/// </summary>
internal static class SpecificationFileLoader
{
    public static (string value, int offset) LoadString(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        var beginningOfTheSequence = new byte[] { (byte)'\x00', (byte)'\x00', (byte)'\x00', (byte)'\x00' };
        var start = value + dataOffset;

        var offsetNew = decompressedFile.IndexOfSubArray(beginningOfTheSequence, (int)start);

        var str = string.Empty;
        if (start == offsetNew)
        {
            return (str, offset);
        }

        while ((offsetNew - start) % 2 == 1)
        {
            offsetNew = decompressedFile.IndexOfSubArray(beginningOfTheSequence, offsetNew + 1);
        }

        str = Encoding.Unicode.GetString(decompressedFile, (int)start, offsetNew - (int)start);

        return (str, offset);
    }

    public static (int value, int offset) LoadInt(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt32(decompressedFile, offset);

        return (value, offset);
    }

    public static (long value, int offset) LoadLong(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        return (value, offset);
    }

    public static (int? value, int offset) LoadPrimaryKey(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var keysCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var keysLength, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (keysCount == 0 || keysCount == -72340172838076674)
        {
            return (null, offset);
        }

        var newOffset = dataOffset + (int)keysLength;

        (var primaryKey, _) = BitConverterExtended.ToInt64(decompressedFile, newOffset);

        return ((int)primaryKey, offset);
    }

    public static (int[] values, int offset) LoadPrimaryKeys(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var keysCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var keysLength, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (keysCount == 0)
        {
            return (Array.Empty<int>(), offset);
        }

        var primaryKeys = new int[keysCount];
        for (var i = 0; i < keysCount; i++)
        {
            var newOffset = dataOffset + (int)keysLength;

            (var primaryKey, _) = BitConverterExtended.ToInt64(decompressedFile, newOffset);

            primaryKeys[i] = (int)primaryKey;
        }

        return (primaryKeys, offset);
    }

    public static (bool value, int offset) LoadBoolean(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToBoolean(decompressedFile, offset);

        return (value, offset);
    }
}