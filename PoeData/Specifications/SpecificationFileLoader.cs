using PoeData.Extensions;
using System.Text;

namespace PoeData.Specifications;

/// <summary>
/// Helper class to load specification files.
/// </summary>
internal static class SpecificationFileLoader
{
    private const int TableOffset = 4;

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

    public static (float value, int offset) LoadFloat(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToSingle(decompressedFile, offset);

        return (value, offset);
    }

    public static (Unknown<int> value, int offset) LoadUnknownInt(byte[] decompressedFile, int offset, int tableRecordLength)
    {
        var columnOffset = GetColumnOffset(offset, tableRecordLength);
        (var value, offset) = LoadInt(decompressedFile, offset);
        var unknown = new Unknown<int>(value, columnOffset);

        return (unknown, offset);
    }

    public static (long value, int offset) LoadLong(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        return (value, offset);
    }

    public static (int? value, int offset) LoadRowPrimaryKey(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidValue(value))
        {
            return (null, offset);
        }

        return ((int)value, offset);
    }

    public static (int[] values, int offset) LoadRowPrimaryKeys(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var keysCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var keysLength, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidKeysCount(keysCount))
        {
            return (Array.Empty<int>(), offset);
        }

        var primaryKeys = new int[keysCount];
        var newOffset = dataOffset + (int)keysLength;
        for (var i = 0; i < keysCount; i++)
        {
            (var primaryKey, newOffset) = BitConverterExtended.ToInt64(decompressedFile, newOffset);

            primaryKeys[i] = (int)primaryKey;
        }

        return (primaryKeys, offset);
    }

    public static (int? value, int offset) LoadForeignRowPrimaryKey(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var _, offset) = BitConverterExtended.ToInt64(decompressedFile, offset); // throwaway value in case of 1 primary key?

        if (IsInvalidValue(value))
        {
            return (null, offset);
        }

        return ((int)value, offset);
    }

    public static (int[] values, int offset) LoadForeignRowPrimaryKeys(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var keysCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var keysLength, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidKeysCount(keysCount))
        {
            return (Array.Empty<int>(), offset);
        }

        var primaryKeys = new int[keysCount];
        //for (var i = 0; i < keysCount; i++)
        //{
        //    var newOffset = dataOffset + (int)keysLength;

        //    (var primaryKey, _) = BitConverterExtended.ToInt64(decompressedFile, newOffset);

        //    primaryKeys[i] = (int)primaryKey;
        //}

        var newOffset = dataOffset + (int)keysLength;
        for (var i = 0; i < keysCount; i++)
        {

            (var primaryKey, newOffset) = BitConverterExtended.ToInt64(decompressedFile, newOffset);
            (var _, newOffset) = BitConverterExtended.ToInt64(decompressedFile, newOffset); // what is this padding?

            primaryKeys[i] = (int)primaryKey;
        }

        return (primaryKeys, offset);
    }

    private static bool IsInvalidKeysCount(long keysCount)
    {
        return keysCount == 0 || IsInvalidValue(keysCount);

        // return keysCount == 0 || keysCount == -72340172838076674;
    }

    private static bool IsInvalidValue(long value)
    {
        // values from pypoe
        return value == -0x1010102 || value == 0xFEFEFEFE || value == -0x101010101010102 || value == -72340172838076674 || value == 0xFFFFFFFF;

        // return value == -0x1010102 || value == 0xFEFEFEFE || value == -0x101010101010102 || value == 0xFEFEFEFEFEFEFEFE || value == 0xFFFFFFFF;
    }

    public static (bool value, int offset) LoadBoolean(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToBoolean(decompressedFile, offset);

        return (value, offset);
    }

    public static (Unknown<bool> value, int offset) LoadUnknownBoolean(byte[] decompressedFile, int offset, int tableRecordLength)
    {
        var columnOffset = GetColumnOffset(offset, tableRecordLength);
        (var value, offset) = LoadBoolean(decompressedFile, offset);
        var unknown = new Unknown<bool>(value, columnOffset);

        return (unknown, offset);
    }

    /// <summary>
    /// Gets column offset.
    /// </summary>
    /// <param name="fileOffset">current file offset.</param>
    /// <param name="tableRecordLength">length of the table record.</param>
    /// <returns>column offset.</returns>
    public static int GetColumnOffset(int fileOffset, int tableRecordLength)
    {
        return (fileOffset - TableOffset) % tableRecordLength;
    }

    internal static (int[] value, int offset) LoadIntArray(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var keysCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var keysLength, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidKeysCount(keysCount))
        {
            return (Array.Empty<int>(), offset);
        }

        var primaryKeys = new int[keysCount];
        var newOffset = dataOffset + (int)keysLength;
        for (var i = 0; i < keysCount; i++)
        {
            (var primaryKey, newOffset) = BitConverterExtended.ToInt32(decompressedFile, newOffset);

            primaryKeys[i] = primaryKey;
        }

        return (primaryKeys, offset);
    }

    internal static (float[] value, int offset) LoadFloatArray(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var keysCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var keysLength, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidKeysCount(keysCount))
        {
            return (Array.Empty<float>(), offset);
        }

        var primaryKeys = new float[keysCount];
        var newOffset = dataOffset + (int)keysLength;
        for (var i = 0; i < keysCount; i++)
        {
            (var primaryKey, newOffset) = BitConverterExtended.ToSingle(decompressedFile, newOffset);

            primaryKeys[i] = primaryKey;
        }

        return (primaryKeys, offset);
    }
}