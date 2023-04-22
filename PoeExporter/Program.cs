using Microsoft.Extensions.Configuration;
using PoeExporter;
using PoeExporter.JsonExporters;
using PoeExporter.WikiExporters.Lua;
using PoeExporter.WikiExporters.Lua.Helpers;
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

var levelSwitch = new LoggingLevelSwitch
{
    MinimumLevel = Serilog.Events.LogEventLevel.Verbose,
};
var logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .WriteTo.Console()
    .CreateLogger();

var test = new FossilsTest()
{
    BaseItemId = "test",
    AllowedTags = new[] { "garbage", "garbage 2" },
    InnerFossil = new() { Test = 2 },
};

var array = new FossilsTest[] { test };

var str = LuaConverter.ToLuaString(array);

return;
var specificationWrapper = new SpecificationWrapper(parsedConfig, logger);

var rootCommand = new RootCommand("exports poe data.");
JsonCommands.AddCommands(specificationWrapper, logger, rootCommand);

// var wikiCommand = new Command("wiki", "exports data to wiki");
// rootCommand.Add(wikiCommand);
// var luaCommand = new Command("lua", "exports lua data");
// wikiCommand.Add(luaCommand);
rootCommand.Invoke(args);
