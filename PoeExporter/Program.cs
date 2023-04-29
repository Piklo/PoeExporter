using Microsoft.Extensions.Configuration;
using PoeExporter;
using PoeExporter.JsonExporters;
using PoeExporter.WikiExporters;
using Serilog;
using Serilog.Core;
using System.CommandLine;

var builder = new ConfigurationBuilder()
    .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var parsedConfig = config.Get<Config>();
if (parsedConfig is null)
{
    return;
}

if (!Enum.IsDefined(typeof(Serilog.Events.LogEventLevel), parsedConfig.MinimumLoggerLevel))
{
    Console.WriteLine($"{nameof(parsedConfig.MinimumLoggerLevel)} is set to unknown value");
    return;
}

var levelSwitch = new LoggingLevelSwitch
{
    MinimumLevel = (Serilog.Events.LogEventLevel)parsedConfig.MinimumLoggerLevel,
};
var logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .WriteTo.Console()
    .CreateLogger();

var specificationWrapper = new SpecificationWrapper(parsedConfig, logger);

var rootCommand = new RootCommand("exports poe data.");
JsonCommands.AddCommands(specificationWrapper, logger, rootCommand);
WikiCommands.AddCommands(rootCommand, specificationWrapper, parsedConfig, logger);

// var wikiCommand = new Command("wiki", "exports data to wiki");
// rootCommand.Add(wikiCommand);
// var luaCommand = new Command("lua", "exports lua data");
// wikiCommand.Add(luaCommand);
rootCommand.Invoke(args);
