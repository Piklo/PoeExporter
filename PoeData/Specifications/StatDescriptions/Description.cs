using System.Text;

namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Class contraining translation descriptions.
/// </summary>
public sealed class Description
{
    private readonly string[] ids;
    private readonly Dictionary<Language, DescriptionLine[]> descriptions = new();

    /// <summary>Gets Ids.</summary>
    public IReadOnlyList<string> Ids { get => ids; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Description"/> class.
    /// </summary>
    /// <param name="bytes">bytes with description data.</param>
    internal Description(ReadOnlySpan<byte> bytes)
    {
        // the entire parsing could probably be done with the span
        // but im kinda lazy tbh
        var str = Encoding.Unicode.GetString(bytes);
        var lines = str.Split("\r\n");

        var idsLine = lines[1].Trim();
        var idsTemp = idsLine.Split();
        ids = idsTemp[1..];

        var lang = Language.English;
        var descriptionsCount = 0;
        var descriptionIndex = 0;
        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith("lang"))
            {
                lang = GetLanguage(line);
            }
            else if (int.TryParse(line, out descriptionsCount))
            {
                descriptions[lang] = new DescriptionLine[descriptionsCount];
                descriptionIndex = 0;
            }
            else
            {
                var array = descriptions[lang];
                var description = new DescriptionLine(line);
                array[descriptionIndex] = description;
                descriptionIndex++;
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
}
