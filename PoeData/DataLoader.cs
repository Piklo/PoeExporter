namespace PoeData;
public sealed class DataLoader
{
    public IConfig Config { get; set; }

    public DataLoader(IConfig config)
    {
        Config = config;
    }
}
