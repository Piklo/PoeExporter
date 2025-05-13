using System.Runtime.InteropServices;

namespace PoeData.Ooz;

public partial class Ooz
{
    [LibraryImport("libooz", EntryPoint = "Ooz_Decompress")]
    public static partial int Decompress(byte[] source, int sourceLength, byte[] destination, int destinationLength);
}
