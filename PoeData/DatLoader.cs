using System.Text;

namespace PoeData;

internal sealed class DatLoader
{
    private static byte[] DatFileMagicNumber = [(byte)'\xbb', (byte)'\xbb', (byte)'\xbb', (byte)'\xbb', (byte)'\xbb', (byte)'\xbb', (byte)'\xbb', (byte)'\xbb'];
    private static byte[] StringEnd = [(byte)'\x00', (byte)'\x00', (byte)'\x00', (byte)'\x00'];

    private readonly byte[] _data;
    private int _offset;
    private readonly int _additionalDataOffset;
    private readonly int _tableRows;
    private readonly int _tableLength;
    private readonly int _recordLength;

    public DatLoader(byte[] data)
    {
        _data = data;
        _offset = 0;
        _additionalDataOffset = data.AsSpan().IndexOf(DatFileMagicNumber);

        if (_additionalDataOffset == -1)
        {
            throw new ArgumentException("Failed to find additional data offset.", nameof(data));
        }

        _tableRows = ReadI32();
        _tableLength = _additionalDataOffset - _offset;
        _recordLength = _tableLength / _tableRows;
    }

    public bool ReadBool()
    {
        var value = BitConverter.ToBoolean(_data, _offset);
        _offset += sizeof(bool);
        return value;
    }

    public short ReadI16()
    {
        var value = BitConverter.ToInt16(_data, _offset);
        _offset += sizeof(short);
        return value;
    }

    public ushort ReadU16()
    {
        var value = BitConverter.ToUInt16(_data, _offset);
        _offset += sizeof(ushort);
        return value;
    }

    public int ReadI32()
    {
        var value = BitConverter.ToInt32(_data, _offset);
        _offset += sizeof(int);
        return value;
    }

    public (long count, long length) ReadI32ArrayRaw()
    {
        var count = ReadI64();
        var length = ReadI64();

        return (count, length);
    }

    public int[] ReadI32Array()
    {
        var (count, legnth) = ReadI32ArrayRaw();
        var values = ReadI32Array(count, _recordLength);
        return values;
    }

    public int[] ReadI32Array(long count, long length)
    {
        throw new NotImplementedException();
    }

    public uint ReadU32()
    {
        var value = BitConverter.ToUInt32(_data, _offset);
        _offset += sizeof(uint);
        return value;
    }

    public long ReadI64()
    {
        var value = BitConverter.ToInt64(_data, _offset);
        _offset += sizeof(long);
        return value;
    }

    public long ReadStringOffset()
    {
        var value = ReadI64();
        return value;
    }

    public string ReadString()
    {
        var stringOffset = ReadStringOffset();
        var str = ReadString(stringOffset);
        return str;
    }

    public string ReadString(long stringOffset)
    {
        var start = (int)(_additionalDataOffset + stringOffset);

        var end = _data.AsSpan(start).IndexOf(StringEnd);

        if (end == start)
        {
            return "";
        }

        while ((end - start) % 2 == 1)
        {
            end = _data.AsSpan(end + 1).IndexOf(StringEnd);
        }

        var str = Encoding.UTF8.GetString(_data.AsSpan(start, end - start));

        return str;
    }

    public long ReadRowRaw()
    {
        var row = ReadI64();
        return row;
    }

    public int? ReadRow()
    {
        var row = ReadRowRaw();
        var row2 = ReadRow(row);
        return row2;
    }

    public int? ReadRow(long row)
    {
        if (IsInvalidValue(row))
        {
            return null;
        }

        return (int)row;
    }

    public (long count, long length) ReadRowsRaw()
    {
        var count = ReadI64();
        var length = ReadI64();
        return (count, length);
    }

    public int[] ReadRows()
    {
        var (count, length) = ReadRowsRaw();
        var rows = ReadRows(count, length);
        return rows;
    }

    public int[] ReadRows(long count, long length)
    {
        if (IsInvalidKeysCount(count))
        {
            return [];
        }

        var currentOffset = _offset;

        var primaryKeys = new int[count];
        _offset = _additionalDataOffset + (int)length;
        for (var i = 0; i < count; i++)
        {
            var key = ReadRowRaw();
            primaryKeys[i] = (int)key;
        }

        _offset = currentOffset;

        return primaryKeys;
    }

    public (long, long) ReadForeignRowRaw()
    {
        var row = ReadI64();
        var value2 = ReadI64();
        return (row, value2);
    }

    public int? ReadForeignRow()
    {
        var (foreignRow, value2) = ReadForeignRowRaw();
        var foreignRow2 = ReadForeignRow(foreignRow, value2);
        return foreignRow2;
    }

    public int? ReadForeignRow(long foreignRow, long value2)
    {
        if (IsInvalidValue(foreignRow))
        {
            return null;
        }

        return (int)foreignRow;
    }


    public (long count, long length) ReadForeignRowsRaw()
    {
        var count = ReadI64();
        var length = ReadI64();
        return (count, length);
    }

    public int[] ReadForeignRows()
    {
        var (count, length) = ReadForeignRowsRaw();
        var rows = ReadForeignRows(count, length);
        return rows;
    }

    public int[] ReadForeignRows(long count, long length)
    {
        if (IsInvalidKeysCount(count))
        {
            return [];
        }

        var currentOffset = _offset;

        var primaryKeys = new int[count];
        _offset = _additionalDataOffset + (int)length;
        for (var i = 0; i < count; i++)
        {
            var (key, _) = ReadForeignRowRaw();
            primaryKeys[i] = (int)key;
        }

        _offset = currentOffset;

        return primaryKeys;
    }

    private static bool IsInvalidValue(long value)
    {
        return value is -0x1010102 or 0xFEFEFEFE or -0x101010101010102 or unchecked((long)0xFEFEFEFEFEFEFEFE) or 0xFFFFFFFF;
    }

    private static bool IsInvalidKeysCount(long count)
    {
        return count == 0 || IsInvalidValue(count);
    }
}
