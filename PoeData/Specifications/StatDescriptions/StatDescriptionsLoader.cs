using Serilog;
using System.Diagnostics.CodeAnalysis;
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
    private readonly Dictionary<IReadOnlyList<string>, Description> descriptions = new(new EqualityComparer());
    private readonly HashSet<string> noDescriptions = new();

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
        var descriptionStrings = DescriptionRegex().Split(file);

        foreach (var descriptionString in descriptionStrings)
        {
            if (descriptionString.StartsWith("description"))
            {
                var newDescription = new Description(descriptionString);
                if (descriptions.TryGetValue(newDescription.Ids, out var existingDescription))
                {
                    existingDescription.Merge(newDescription);
                }
                else
                {
                    descriptions.Add(newDescription.Ids, newDescription);
                }
            }
            else if (descriptionString.StartsWith("no_description"))
            {
                var split = descriptionString.Trim().Split();
                if (split.Length != 2)
                {
                    throw new NotImplementedException();
                }

                var item = split[1];
                noDescriptions.Add(item);
            }
            else
            {
                throw new NotImplementedException("unknown description start");
            }
        }
    }

    private sealed class EqualityComparer : IEqualityComparer<IReadOnlyList<string>>
    {
        public bool Equals(IReadOnlyList<string>? x, IReadOnlyList<string>? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            else if (x is null || y is null)
            {
                return false;
            }

            return x.SequenceEqual(y);
        }

        public int GetHashCode([DisallowNull] IReadOnlyList<string> obj)
        {
            var code = default(HashCode);

            foreach (var item in obj)
            {
                code.Add(item);
            }

            return code.ToHashCode();
        }
    }

    /// <summary>
    /// Gets description.
    /// </summary>
    /// <param name="descriptionIds">description ids.</param>
    /// <returns><see cref="Description"/> if found, <see langword="null"/> otherwise.</returns>
    public Description? GetDescription(IReadOnlyList<string> descriptionIds)
    {
        if (descriptions.TryGetValue(descriptionIds, out var description))
        {
            return description;
        }
        else
        {
            return null;
        }
    }
}
