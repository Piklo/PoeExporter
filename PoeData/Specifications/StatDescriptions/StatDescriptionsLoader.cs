using Serilog;
using System.Text;

namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Class used to load stat descriptions and work with them.
/// </summary>
public class StatDescriptionsLoader
{
    private readonly DataLoader dataLoader;
    private readonly IConfig config;
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatDescriptionsLoader"/> class.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <param name="config">config.</param>
    /// <param name="logger">logger</param>
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
        const int characterLength = 2;
        var decompressedFile = dataLoader.GetFileBytes(filePath);
        var span = new ReadOnlySpan<byte>(decompressedFile);

        var descriptionStart = 0;
        var emptyLine = new ReadOnlySpan<byte>(Encoding.Unicode.GetBytes("\r\n"));
        for (var i = 0; i < span.Length; i++)
        {
            if (StatDescriptionsHelper.IsNewLine(span, i) && !StatDescriptionsHelper.IsTab(span, i + characterLength))
            {
                var descriptionEnd = i + characterLength;
                var descriptionSpan = span[descriptionStart..descriptionEnd];

                var line = System.Text.Encoding.Unicode.GetString(descriptionSpan); // debug

                descriptionStart = descriptionEnd;

                if (descriptionSpan.SequenceEqual(emptyLine))
                {
                    continue;
                }

                // create stat description here
            }
        }
    }
}
