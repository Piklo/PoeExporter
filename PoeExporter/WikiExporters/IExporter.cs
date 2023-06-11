namespace PoeExporter.WikiExporters;

/// <summary>
/// Interface containing exporter methods.
/// </summary>
internal interface IExporter
{
    /// <summary>Gets page name.</summary>
    public string PageName { get; }

    /// <summary>
    /// Exports data.
    /// </summary>
    /// <returns>exported string.</returns>
    public string Export();
}
