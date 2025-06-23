namespace PoeData.Hash;

internal static class FNV
{
    public static ulong HashFNV1a(string data)
    {
        var bytes = data.Select(c => (byte)c);

        return HashFNV1a(bytes);
    }

    public static ulong HashFNV1a(IEnumerable<byte> data)
    {
        const ulong fnv64Offset = 14695981039346656037;
        const ulong fnv64Prime = 0x100000001b3;
        var hash = fnv64Offset;

        foreach (var b in data)
        {
            hash = hash ^ b;
            hash *= fnv64Prime;
        }

        return hash;
    }
}
