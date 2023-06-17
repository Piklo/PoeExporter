using Serilog;
using System.Text;

namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Class used to load stat descriptions and work with them.
/// </summary>
public class StatDescriptionsLoader
{
    private const int CharacterLength = 2;
    private readonly DataLoader dataLoader;
    private readonly IConfig config;
    private readonly ILogger logger;
    private readonly static byte[] DescriptionBytes = Encoding.Unicode.GetBytes("description");
    private readonly static byte[] NoDescriptionBytes = Encoding.Unicode.GetBytes("no_description");

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
        var decompressedFile = new ReadOnlySpan<byte>(dataLoader.GetFileBytes(filePath));
        decompressedFile = decompressedFile[2..]; // we start at 2 because the first two bytes are Byte order mark

        var blockStart = 0;
        var blockEnd = 0;
        var lineStart = 0;
        var lineEnd = 0;
        var isNextBlockDescription = true;
        for (var i = 0; i < decompressedFile.Length; i++)
        {
            if (StatDescriptionsHelper.IsNewLine(decompressedFile, i))
            {
                i += CharacterLength;
                lineEnd = i;
                var line = decompressedFile[lineStart..lineEnd];
                var lineString = Encoding.Unicode.GetString(line); // debug

                var isDescription = IsDescription(line);
                var isNoDescription = IsNoDescription(line);

                if (isDescription || isNoDescription)
                {
                    blockEnd = lineStart;
                    var descriptionSpan = decompressedFile[blockStart..blockEnd];
                    var descriptionString = Encoding.Unicode.GetString(descriptionSpan);

                    if (descriptionSpan.Length != 0)
                    {
                        if (isNextBlockDescription)
                        {
                            var description = new Description(descriptionSpan);
                        }
                        else
                        {
                            // create no description here
                        }
                    }

                    blockStart = blockEnd;
                }

                if (isDescription)
                {
                    isNextBlockDescription = true;
                }
                else if (isNoDescription)
                {
                    isNextBlockDescription = false;
                }

                lineStart = lineEnd;
            }
        }
    }

    private static bool IsDescription(ReadOnlySpan<byte> span)
    {
        var descriptionSpan = new ReadOnlySpan<byte>(DescriptionBytes);

        if (span.Length < descriptionSpan.Length)
        {
            return false;
        }

        var subspan = span[..descriptionSpan.Length];

        return subspan.SequenceEqual(descriptionSpan);
    }

    private static bool IsNoDescription(ReadOnlySpan<byte> span)
    {
        var noDescriptionSpan = new ReadOnlySpan<byte>(NoDescriptionBytes);

        if (span.Length < noDescriptionSpan.Length)
        {
            return false;
        }

        var subspan = span[..noDescriptionSpan.Length];

        return subspan.SequenceEqual(noDescriptionSpan);
    }
}
