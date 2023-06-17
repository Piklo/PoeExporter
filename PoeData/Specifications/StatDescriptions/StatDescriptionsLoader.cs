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

        var start = 0;
        for (var i = 0; i < decompressedFile.Length; i++)
        {
            if (StatDescriptionsHelper.IsNewLine(decompressedFile, i) && !StatDescriptionsHelper.IsTab(decompressedFile, i + CharacterLength))
            {
                var end = i + CharacterLength;
                var subspan = decompressedFile[start..end];

                var line = Encoding.Unicode.GetString(subspan); // debug

                start = end;

                if (IsEmptyLine(subspan))
                {
                    continue;
                }
                else if (IsDescription(subspan))
                {

                }
                else if (IsNoDescription(subspan))
                {

                }
                else
                {
                    throw new InvalidOperationException("found span which isn't description or nodescription");
                }
            }
        }
    }

    private static bool IsEmptyLine(ReadOnlySpan<byte> span)
    {
        var emptyLine = new ReadOnlySpan<byte>(StatDescriptionsHelper.NewLineBytes);

        return span.SequenceEqual(emptyLine);
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
