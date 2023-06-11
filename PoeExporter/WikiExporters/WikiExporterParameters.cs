using Serilog;

namespace PoeExporter.WikiExporters;

/// <summary>
/// Class containing commonly used classes.
/// </summary>
internal sealed class WikiExporterParameters
{
    /// <summary>Gets logger.</summary>
    internal ILogger Logger { get; init; }

    /// <summary>Gets specification wrapper.</summary>
    internal SpecificationWrapper SpecificationWrapper { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WikiExporterParameters"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specificationWrapper">specification wrapper.</param>
    internal WikiExporterParameters(ILogger logger, SpecificationWrapper specificationWrapper)
    {
        Logger = logger;
        SpecificationWrapper = specificationWrapper;
    }
}
