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
            if (IsNewLine(span, i) && !IsTab(span, i + characterLength))
            {
                var newLineIndex = i + 1; // +1 because unicode characters are two bytes long
                var descriptionSpan = span[descriptionStart..(newLineIndex + 1)]; // +1 because length is exclusive

                var line = System.Text.Encoding.Unicode.GetString(descriptionSpan); // debug

                descriptionStart = newLineIndex + 1;

                if (descriptionSpan.SequenceEqual(emptyLine))
                {
                    continue;
                }

                // create stat description here
            }
        }
    }

    private static bool IsNewLine(ReadOnlySpan<byte> span, int index)
    {
        if (index + 1 >= span.Length)
        {
            return false;
        }

        return span[index] == '\n' && span[index + 1] == 0;
    }

    private static bool IsTab(ReadOnlySpan<byte> span, int index)
    {
        if (index + 1 >= span.Length)
        {
            return false;
        }

        return span[index] == '\t' && span[index + 1] == 0;
    }
}
