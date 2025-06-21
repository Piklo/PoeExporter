namespace PoeData.Hash;

internal static class FNV
{
    public static long HashFNV1a(string data)
    {
        var bytes = data.Select(c => (byte)c);

        return HashFNV1a(bytes);
    }

    public static long HashFNV1a(IEnumerable<byte> data)
    {
        const long fnv64Offset = unchecked((long)14695981039346656037);
        const long fnv64Prime = 0x100000001b3;
        var hash = fnv64Offset;

        foreach (var b in data)
        {
            hash = hash ^ b;
            hash *= fnv64Prime;
        }

        return hash;
    }
}
