namespace PoeData;

/// <summary>
/// Class containing Fowler–Noll–Vo non-cryptographic hash functions.
/// </summary>
internal static class Fnv
{
    /// <summary>
    /// 64 bit variant of FNV-1a.
    /// </summary>
    /// <param name="bytes">bytes of data.</param>
    /// <returns>hashed bytes.</returns>
    public static ulong Fnv1a_64(byte[] bytes)
    {
        const ulong FNV1_64_INIT = 0xcbf29ce484222325;
        const ulong FNV_64_PRIME = 0x00000100000001B3;

        var hval = FNV1_64_INIT;
        for (var i = 0; i < bytes.Length; i++)
        {
            hval ^= bytes[i];
            hval *= FNV_64_PRIME;
        }

        return hval;
    }
}