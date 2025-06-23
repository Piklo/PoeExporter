using System.Text;

namespace PoeData.Hash;

internal static class MurmurHash2
{
    public static ulong MurmurHash64A(string data, ulong seed)
    {
        var bytes = Encoding.UTF8.GetBytes(data);

        return MurmurHash64A(bytes, seed);
    }

    public static ulong MurmurHash64A(byte[] bytes, ulong seed)
    {
        const ulong m = 0xc6a4a7935bd1e995;
        const int r = 47;

        var h = seed ^ ((ulong)bytes.Length * m);
        int i;
        var remainder = bytes.Length & 7;
        for (i = 0; i < bytes.Length - remainder; i += sizeof(ulong))
        {
            var k = BitConverter.ToUInt64(bytes, i);

            k *= m;
            k ^= k >> r;
            k *= m;

            h ^= k;
            h *= m;
        }

        if (remainder == 7)
        {
            h ^= (ulong)bytes[i + 6] << 48;
        }

        if (remainder >= 6)
        {
            h ^= (ulong)bytes[i + 5] << 40;
        }

        if (remainder >= 5)
        {
            h ^= (ulong)bytes[i + 4] << 32;
        }

        if (remainder >= 4)
        {
            h ^= (ulong)bytes[i + 3] << 24;
        }

        if (remainder >= 3)
        {
            h ^= (ulong)bytes[i + 2] << 16;
        }

        if (remainder >= 2)
        {
            h ^= (ulong)bytes[i + 1] << 8;
        }

        if (remainder >= 1)
        {
            h ^= bytes[i];
            h *= m;
        }

        h ^= h >> r;
        h *= m;
        h ^= h >> r;

        return h;
    }
}
