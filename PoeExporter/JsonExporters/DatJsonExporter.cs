using PoeData.Specifications;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace PoeExporter.JsonExporters;

/// <summary>
/// Exports all dat data and saves as json.
/// </summary>
internal sealed class DatJsonExporter
{
    private ILogger logger;
    private readonly Specification specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatJsonExporter"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    public DatJsonExporter(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
    }

    /// <summary>
    /// Runs <see cref="DatJsonExporter"/>.
    /// </summary>
    public void Run()
    {
        var startTime = Stopwatch.GetTimestamp();

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
    }
}
