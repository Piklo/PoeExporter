using Serilog;

namespace PoeData.Specifications;

/// <summary>
/// Class containing all Path of Exile data.
/// </summary>
public sealed partial class Specification
{
    /// <summary>Gets dat file magic number.</summary>
    /// thats where the table ends?
    internal static byte[] DatFileMagicNumber { get; } = new byte[] { (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb' };

    private readonly DataLoader dataLoader;
    private readonly IConfig config;
    private readonly ILogger logger;

    /// <summary>Gets data loader.</summary>
    internal DataLoader DataLoader => dataLoader;

    /// <summary>
    /// Initializes a new instance of the <see cref="Specification"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public Specification(IConfig config, ILogger logger)
    {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        dataLoader = new DataLoader(config, logger);

        this.config = config;
        this.logger = logger;
    }
}
