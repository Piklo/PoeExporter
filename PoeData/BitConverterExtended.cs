namespace PoeData;

/// <summary>
/// Class meant to be used to convert an array of bytes to base data types instead of <seealso cref="BitConverter"/>.
/// </summary>
internal static class BitConverterExtended
{
    /// <inheritdoc cref="BitConverter.ToInt32(byte[], int)"/>
    public static (int value, int offset) ToInt32(byte[] value, int startIndex = 0)
    {
        var converted = BitConverter.ToInt32(value, startIndex);
        startIndex += sizeof(int);

        return (converted, startIndex);
    }

    /// <inheritdoc cref="BitConverter.ToUInt32(byte[], int)"/>
    public static (uint value, int offset) ToUInt32(byte[] value, int startIndex = 0)
    {
        var converted = BitConverter.ToUInt32(value, startIndex);
        startIndex += sizeof(uint);

        return (converted, startIndex);
    }

    /// <inheritdoc cref="BitConverter.ToInt64(byte[], int)"/>
    public static (long value, int offset) ToInt64(byte[] value, int startIndex = 0)
    {
        var converted = BitConverter.ToInt64(value, startIndex);
        startIndex += sizeof(long);

        return (converted, startIndex);
    }

    /// <inheritdoc cref="BitConverter.ToUInt64(byte[], int)"/>
    public static (ulong value, int offset) ToUInt64(byte[] value, int startIndex = 0)
    {
        var converted = BitConverter.ToUInt64(value, startIndex);
        startIndex += sizeof(ulong);

        return (converted, startIndex);
    }

    /// <inheritdoc cref="BitConverter.ToSingle(byte[], int)(byte[], int)"/>
    public static (float value, int offset) ToSingle(byte[] value, int startIndex = 0)
    {
        var converted = BitConverter.ToSingle(value, startIndex);
        startIndex += sizeof(float);

        return (converted, startIndex);
    }

    /// <inheritdoc cref="BitConverter.ToBoolean(byte[], int)(byte[], int)"/>
    public static (bool value, int offset) ToBoolean(byte[] value, int startIndex = 0)
    {
        var converted = BitConverter.ToBoolean(value, startIndex);
        startIndex += sizeof(bool);

        return (converted, startIndex);
    }
}
