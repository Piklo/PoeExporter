using PoeData.Extensions;
using System.Text;

namespace PoeData.Specifications;

/// <summary>
/// Helper class to load specification files.
/// </summary>
internal static class SpecificationFileLoader
{
    /// <summary>
    /// Loads a string from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read string from.</param>
    /// <param name="offset">offset to read the string offset from.</param>
    /// <param name="dataOffset">offset of referenced data.</param>
    /// <returns>read string and moved offset.</returns>
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

    /// <summary>
    /// Loads a string array from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read strings from.</param>
    /// <param name="offset">offset to read the strings offset from.</param>
    /// <param name="dataOffset">offset of referenced data.</param>
    /// <returns>read strings and moved offset.</returns>
    public static (string[] value, int offset) LoadStringArray(byte[] decompressedFile, int offset, int dataOffset)
    {
        (var stringsCount, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var stringsOffset, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidKeysCount(stringsCount))
        {
            return (Array.Empty<string>(), offset);
        }

        var strings = new string[stringsCount];
        var newOffset = dataOffset + (int)stringsOffset;
        for (var i = 0; i < stringsCount; i++)
        {
            // (var stringSomething, newOffset) = BitConverterExtended.ToInt64(decompressedFile, newOffset);
            // var strOffset = stringSomething + dataOffset;
            // strings[i] = "";
            (var str, newOffset) = LoadString(decompressedFile, newOffset, dataOffset);

            strings[i] = str;
        }

        return (strings, offset);
    }

    /// <summary>
    /// Loads an int from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read int from.</param>
    /// <param name="offset">offset to read the int from.</param>
    /// <returns>read int and moved offset.</returns>
    public static (int value, int offset) LoadInt(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt32(decompressedFile, offset);

        return (value, offset);
    }

    /// <summary>
    /// Loads a float from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read float from.</param>
    /// <param name="offset">offset to read the float from.</param>
    /// <returns>read float and moved offset.</returns>
    public static (float value, int offset) LoadFloat(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToSingle(decompressedFile, offset);

        return (value, offset);
    }

    /// <summary>
    /// Loads a long from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read long from.</param>
    /// <param name="offset">offset to read the long from.</param>
    /// <returns>read long and moved offset.</returns>
    public static (long value, int offset) LoadLong(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        return (value, offset);
    }

    /// <summary>
    /// Loads a row primary key from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read primary key from.</param>
    /// <param name="offset">offset to read the primary key from.</param>
    /// <returns>read primary key and moved offset.</returns>
    public static (int? value, int offset) LoadRowPrimaryKey(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);

        if (IsInvalidValue(value))
        {
            return (null, offset);
        }

        return ((int)value, offset);
    }

    /// <summary>
    /// Loads row primary keys array from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read primary keys from.</param>
    /// <param name="offset">offset to read the primary keys offset from.</param>
    /// <param name="dataOffset">offset of referenced data.</param>
    /// <returns>read primary keys and moved offset.</returns>
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

    /// <summary>
    /// Loads foreign row primary key from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read primary key from.</param>
    /// <param name="offset">offset to read the primary key from.</param>
    /// <returns>read primary key and moved offset.</returns>
    public static (int? value, int offset) LoadForeignRowPrimaryKey(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToInt64(decompressedFile, offset);
        (var _, offset) = BitConverterExtended.ToInt64(decompressedFile, offset); // throwaway value in case of 1 primary key?

        if (IsInvalidValue(value))
        {
            return (null, offset);
        }

        return ((int)value, offset);
    }

    /// <summary>
    /// Loads foreign row primary keys array from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read primary keys from.</param>
    /// <param name="offset">offset to read the primary keys offset from.</param>
    /// <param name="dataOffset">offset of referenced data.</param>
    /// <returns>read primary keys and moved offset.</returns>
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

    /// <summary>
    /// Loads a boolean from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read boolean from.</param>
    /// <param name="offset">offset to read the boolean from.</param>
    /// <returns>read boolean and moved offset.</returns>
    public static (bool value, int offset) LoadBoolean(byte[] decompressedFile, int offset)
    {
        (var value, offset) = BitConverterExtended.ToBoolean(decompressedFile, offset);

        return (value, offset);
    }

    /// <summary>
    /// Loads int array from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read ints from.</param>
    /// <param name="offset">offset to read the ints offset from.</param>
    /// <param name="dataOffset">offset of referenced data.</param>
    /// <returns>read ints and moved offset.</returns>
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

    /// <summary>
    /// Loads float array from the file and moves the offset.
    /// </summary>
    /// <param name="decompressedFile">file to read float from.</param>
    /// <param name="offset">offset to read the float offset from.</param>
    /// <param name="dataOffset">offset of referenced data.</param>
    /// <returns>read float and moved offset.</returns>
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