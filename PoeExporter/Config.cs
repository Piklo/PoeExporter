using PoeData;

namespace PoeExporter;

public class Config : IConfig
{
    public required string PoePath { get; init; }
}
