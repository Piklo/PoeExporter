namespace PoeExporter;

/// <summary>
/// Interface with config data.
/// </summary>
internal interface IConfig : PoeData.IConfig
{
    /// <summary>Gets output directory.</summary>
    string Output { get; init; }
}
