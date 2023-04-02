using Serilog;
using Serilog.Core;

namespace SpecificationGenerator;

#pragma warning disable SA1600 // Elements should be documented
internal sealed class Program
#pragma warning restore SA1600 // Elements should be documented
{
    private static async Task Main()
    {
        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = Serilog.Events.LogEventLevel.Verbose,
        };
        var logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch)
            .WriteTo.Console()
            .CreateLogger();

        var generator = new Generator(logger);
        await generator.RunAsync();
    }
}
