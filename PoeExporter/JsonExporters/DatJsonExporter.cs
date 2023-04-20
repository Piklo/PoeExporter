using PoeData.Specifications;
using Serilog;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace PoeExporter.JsonExporters;

/// <summary>
/// Exports all dat data and saves as json.
/// </summary>
internal sealed partial class DatJsonExporter
{
    private ILogger logger;
    private readonly Specification specification;
    private readonly DirectoryInfo resultsDir;
    private int exceptionCounter;
    private bool throwOnException;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatJsonExporter"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    public DatJsonExporter(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        resultsDir = new DirectoryInfo("results"); // pass this in Config?
        if (!resultsDir.Exists)
        {
            resultsDir.Create();
        }

        throwOnException = false; // pass this in Config?
    }

    /// <summary>
    /// Runs <see cref="DatJsonExporter"/>.
    /// </summary>
    public void Run()
    {
        var startTime = Stopwatch.GetTimestamp();
        logger.Information("running");
        RunAll();

        logger.Information("{count} exceptions", exceptionCounter);
        logger.Information("total duration {elapsed} elapsed", Stopwatch.GetElapsedTime(startTime));
    }

    private void Save<T>(IReadOnlyList<T> result, string fileName)
    {
        var serialized = JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions()
        {
            WriteIndented = true,
        });

        File.WriteAllText(Path.Combine(resultsDir.FullName, $"{fileName}.json"), serialized, Encoding.UTF8);
    }

    private partial void RunAll();
}
