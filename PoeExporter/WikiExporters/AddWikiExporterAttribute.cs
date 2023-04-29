namespace PoeExporter.WikiExporters;

/// <summary>
/// Attribute used add wiki exporters.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal sealed class AddWikiExporterAttribute : Attribute
{
    /// <summary>Gets exporter.</summary>
    public Type[] Exporters { get; }

    /// <summary>Gets exporter.</summary>
    public string[] Aliases { get; }

    /// <summary>Gets exporter.</summary>
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddWikiExporterAttribute"/> class.
    /// </summary>
    /// <param name="exporters">exporters.</param>
    /// <param name="aliases">option aliases.</param>
    /// <param name="description">option description.</param>
    public AddWikiExporterAttribute(Type[] exporters, string[] aliases, string description)
    {
        Exporters = exporters;
        Aliases = aliases;
        Description = description;
    }
}
