using PoeData.Specifications;
using Serilog;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace PoeExporter.JsonExporters;

/// <summary>
/// Exports all dat data and saves as json.
/// </summary>
internal sealed partial class DatJsonExporter
{
    private readonly Specification specification;
    private readonly ILogger logger;
    private readonly DirectoryInfo resultsDir;
    private readonly bool throwOnException;
    private int exceptionCounter;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatJsonExporter"/> class.
    /// </summary>
    /// <param name="specification">specification.</param>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    public DatJsonExporter(Specification specification, IConfig config, ILogger logger)
    {
        this.specification = specification;
        this.logger = logger;

        resultsDir = new DirectoryInfo(config.Output);
        if (!resultsDir.Exists)
        {
            resultsDir.Create();
        }

        throwOnException = false;
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
        var serialized = JsonSerializer.Serialize(result, new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });

        File.WriteAllText(Path.Combine(resultsDir.FullName, $"{fileName}.json"), serialized, Encoding.UTF8);
    }

    private partial void RunAll();
}
