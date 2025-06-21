using System.Text;

namespace PoeData.Hash;

internal static class MurmurHash2
{
    public static long MurmurHash64A(string data, long seed)
    {
        var bytes = Encoding.UTF8.GetBytes(data);

        return MurmurHash64A(bytes, seed);
    }

    public static long MurmurHash64A(byte[] bytes, long seed)
    {
        const long m = unchecked((long)0xc6a4a7935bd1e995);
        const int r = 47;

        var h = seed ^ (bytes.Length * m);
        var i = 0;
        var remainder = bytes.Length & 7;
        for (i = 0; i < bytes.Length - remainder; i += sizeof(ulong))
        {
            var k = BitConverter.ToInt64(bytes, i);

            k *= m;
            k ^= k >> r;
            k *= m;

            h ^= k;
            h *= m;
        }

        if (remainder == 7)
        {
            h ^= (long)bytes[i + 6] << 48;
        }

        if (remainder >= 6)
        {
            h ^= (long)bytes[i + 5] << 40;
        }

        if (remainder >= 5)
        {
            h ^= (long)bytes[i + 4] << 32;
        }

        if (remainder >= 4)
        {
            h ^= (long)bytes[i + 3] << 24;
        }

        if (remainder >= 3)
        {
            h ^= (long)bytes[i + 2] << 16;
        }

        if (remainder >= 2)
        {
            h ^= (long)bytes[i + 1] << 8;
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
