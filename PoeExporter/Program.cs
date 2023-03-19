using Microsoft.Extensions.Configuration;
using PoeData;

namespace PoeExporter;

internal sealed class Program
{
    static void Main()
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false);

        IConfiguration config = builder.Build();

        var parsedConfig = config.Get<Config>();
        if (parsedConfig is null) throw new ArgumentNullException(nameof(parsedConfig));

        var loader = new DataLoader(parsedConfig);
    }
}

public class Config : IConfig
{
    public required string PoePath { get; init; }
}