namespace PoeData;

internal interface IDataLoader
{
    byte[] ReadIndex();
    byte[] GetFileBytes(string path);
}
