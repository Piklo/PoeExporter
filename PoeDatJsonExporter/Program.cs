using Microsoft.Extensions.Configuration;
using PoeData.Specifications;
using Serilog;
using Serilog.Core;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var parsedConfig = config.Get<Config>();
if (parsedConfig is null)
{
    throw new ArgumentNullException(nameof(parsedConfig));
}

var levelSwitch = new LoggingLevelSwitch
{
    MinimumLevel = Serilog.Events.LogEventLevel.Verbose,
};
var logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(levelSwitch)
    .WriteTo.Console()
    .CreateLogger();

var startTime = Stopwatch.GetTimestamp();

var specification = new Specification(parsedConfig, logger);

var type = typeof(Specification);
var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

var resultsDir = new DirectoryInfo("results");

if (resultsDir.Exists)
{
    resultsDir.Delete(true);
}

resultsDir.Create();

var exceptionsCount = 0;
foreach (var method in methods)
{
    if (!method.Name.StartsWith("Load"))
    {
        continue;
    }

    try
    {
        var result = method.Invoke(specification, null);
        if (result is not IReadOnlyList<object> collection)
        {
            logger.Error("result isnt an indexable type");
            continue;
        }

        var resType = collection[0].GetType();
        var className = resType.Name;

        var serialized = JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions()
        {
            WriteIndented = true,
        });

        File.WriteAllText(Path.Combine(resultsDir.FullName, $"{className}.json"), serialized, Encoding.UTF8);
    }
    catch (TargetInvocationException e)
    {
        logger.Error("{error}", e.InnerException);
        exceptionsCount++;
    }
}

logger.Information("{count} exceptions", exceptionsCount);
logger.Information("total duration {elapsed} elapsed", Stopwatch.GetElapsedTime(startTime));