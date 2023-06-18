namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Class contraining translation descriptions.
/// </summary>
public sealed class Description
{
    private readonly string[] ids = Array.Empty<string>();
    private readonly Dictionary<Language, DescriptionLine[]> descriptions = new();
    private ProcessState state = ProcessState.None;
    private Language language = Language.English;

    /// <summary>Gets Ids.</summary>
    public IReadOnlyList<string> Ids { get => ids; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Description"/> class.
    /// </summary>
    /// <param name="data">description data.</param>
    internal Description(string data)
    {
        var lines = data.Split("\r\n");

        var tempLines = Array.Empty<DescriptionLine>();
        var linesIndex = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            var line = lines[i].Trim();

            if (line.StartsWith("description"))
            {
                state = ProcessState.Description;
            }
            else if (line.StartsWith("lang"))
            {
                state = ProcessState.Language;
            }

            switch (state)
            {
                case ProcessState.None:
                    throw new NotImplementedException();
                case ProcessState.Description:
                    state = ProcessState.Ids;
                    break;
                case ProcessState.Ids:
                    var split = line.Split();
                    var count = int.Parse(split[0]);
                    ids = split[1..];

                    if (count != ids.Length)
                    {
                        throw new NotImplementedException("unexpected count of ids");
                    }

                    state = ProcessState.LinesCount;
                    break;
                case ProcessState.Language:

                    language = GetLanguage(line);
                    state = ProcessState.LinesCount;
                    break;
                case ProcessState.LinesCount:
                    var linesCount = int.Parse(line);
                    tempLines = new DescriptionLine[linesCount];
                    linesIndex = 0;
                    descriptions.Add(language, tempLines);
                    state = ProcessState.Line;
                    break;
                case ProcessState.Line:
                    var descriptionLine = new DescriptionLine(line);
                    tempLines[linesIndex] = descriptionLine;
                    linesIndex++;
                    break;
                default:
                    throw new NotImplementedException("unhandled case");
            }
        }
    }

    private static Language GetLanguage(string language) => language switch
    {
        "lang \"English\"" => Language.English,
        "lang \"Spanish\"" => Language.Spanish,
        "lang \"German\"" => Language.German,
        "lang \"Portuguese\"" => Language.Portuguese,
        "lang \"Simplified Chinese\"" => Language.SimplifiedChinese,
        "lang \"French\"" => Language.French,
        "lang \"Russian\"" => Language.Russian,
        "lang \"Korean\"" => Language.Korean,
        "lang \"Traditional Chinese\"" => Language.TraditionalChinese,
        "lang \"Thai\"" => Language.Thai,
        "lang \"Japanese\"" => Language.Japanese,
        _ => throw new NotImplementedException(),
    };

    /// <summary>
    /// Enum containing possible languages for stat descriptions.
    /// </summary>
    private enum Language
    {
        English,
        Spanish,
        German,
        Portuguese,
        SimplifiedChinese,
        French,
        Russian,
        Korean,
        TraditionalChinese,
        Thai,
        Japanese,
    }

    private enum ProcessState
    {
        None,
        Description,
        Ids,
        LinesCount,
        Line,
        Language,
    }
}
