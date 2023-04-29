using Serilog;

namespace PoeExporter.WikiExporters;

/// <summary>
/// Interface containing exporter methods.
/// </summary>
/// <typeparam name="T">type of the exporter.</typeparam>
internal interface IExporter<T>
{
    /// <summary>Gets page name.</summary>
    public string PageName { get; }

    /// <summary>
    /// Creates instance of <see cref="IExporter{T}"/>.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    /// <returns>instance of <see cref="IExporter{T}"/>.</returns>
    static abstract T Create(SpecificationWrapper specificationWrapper, ILogger logger);

    /// <summary>
    /// Exports data.
    /// </summary>
    /// <returns>exporter string.</returns>
    string Export();
}
