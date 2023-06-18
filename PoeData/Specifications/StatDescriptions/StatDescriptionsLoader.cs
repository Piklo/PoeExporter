using Serilog;
using System.Text;
using System.Text.RegularExpressions;

namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Class used to load stat descriptions and work with them.
/// </summary>
public partial class StatDescriptionsLoader
{
    private readonly DataLoader dataLoader;
    private readonly IConfig config;
    private readonly ILogger logger;
    private readonly Dictionary<string, Description> parsedDescriptions = new();

    [GeneratedRegex("^(?=description|no_description)", RegexOptions.Multiline)]
    private static partial Regex DescriptionRegex();

    /// <summary>
    /// Initializes a new instance of the <see cref="StatDescriptionsLoader"/> class.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    internal StatDescriptionsLoader(DataLoader dataLoader, IConfig config, ILogger logger)
    {
        this.dataLoader = dataLoader;
        this.config = config;
        this.logger = logger;
        LoadStatDescriptions();
    }

    private void LoadStatDescriptions()
    {
        const string filePath = "Metadata/StatDescriptions/stat_descriptions.txt";
        var file = Encoding.Unicode.GetString(dataLoader.GetFileBytes(filePath));
        var descriptions = DescriptionRegex().Split(file);

        foreach (var description in descriptions)
        {
            if (description.StartsWith("description"))
            {
                var parsed = new Description(description);

                foreach (var id in parsed.Ids)
                {
                    if (parsedDescriptions.TryGetValue(id, out var existing))
                    {
                        existing.Merge(parsed);
                    }
                    else
                    {
                        parsedDescriptions.Add(id, parsed);
                    }
                }
            }
            else if (description.StartsWith("no_description"))
            {

            }
            else
            {
                throw new NotImplementedException("unknown description start");
            }
        }
    }
}
