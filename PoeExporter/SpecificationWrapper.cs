using PoeData;
using PoeData.Specifications;
using Serilog;

namespace PoeExporter;

/// <summary>
/// Wrapper class which holds the instance of <see cref="Specification"/>.
/// </summary>
internal class SpecificationWrapper
{
    private readonly IConfig config;
    private readonly ILogger logger;
    private Specification? specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpecificationWrapper"/> class.
    /// </summary>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    public SpecificationWrapper(IConfig config, ILogger logger)
    {
        this.config = config;
        this.logger = logger;
    }

    /// <summary>
    /// Gets or creates new instance of <see cref="Specification"/>.
    /// </summary>
    /// <returns>instance of <see cref="Specification"/>.</returns>
    public Specification GetOrCreateSpecification()
    {
        specification ??= new Specification(config, logger);

        return specification;
    }
}
