using Microsoft.Extensions.Configuration;
using PoeData;
using Serilog;
using Serilog.Core;

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

        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = Serilog.Events.LogEventLevel.Verbose
        };
        var logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch)
            .WriteTo.Console()
            .CreateLogger();


        var loader = new DataLoader(parsedConfig, logger);
    }
}

public class Config : IConfig
{
    public required string PoePath { get; init; }
}