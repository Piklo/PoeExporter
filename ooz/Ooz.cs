using System.Runtime.InteropServices;

namespace ooz;
public static partial class Ooz
{
    [LibraryImport("libooz.dll", EntryPoint = "Ooz_Decompress")]
    public static partial int Decompress(byte[] src, int src_len, byte[] dst, int dst_len);
}
