namespace PoeData;

internal sealed class SteamLoader : IDataLoader
{
    private const string IndexPath = "Bundles2/_.index.bin";
    private readonly string _clientPath;

    public SteamLoader(string clientPath)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(clientPath);
        _clientPath = clientPath;
    }

    public byte[] ReadIndex()
    {
        var fullPath = Path.Combine(_clientPath, IndexPath);
        var bytes = File.ReadAllBytes(fullPath);
        return bytes;
    }

    public byte[] GetFileBytes(string path)
    {
        var fullPath = Path.Combine(_clientPath, path);
        var bytes = File.ReadAllBytes(fullPath);
        return bytes;
    }
}
