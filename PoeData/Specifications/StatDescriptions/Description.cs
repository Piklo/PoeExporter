using System.Numerics;

namespace PoeData.Specifications.StatDescriptions;

/// <summary>
/// Class containing translation descriptions.
/// </summary>
public sealed class Description
{
    private readonly string[] ids = Array.Empty<string>();
    private readonly Dictionary<Language, HashSet<DescriptionLine>> descriptions = new();

    /// <summary>Gets Ids.</summary>
    public IReadOnlyList<string> Ids { get => ids; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Description"/> class.
    /// </summary>
    /// <param name="data">description data.</param>
    internal Description(string data)
    {
        var lines = data.Split("\r\n");
        var idsString = lines[1];
        ids = ParseIds(idsString);

        var langLines = lines[2..];

        var language = Language.English;
        descriptions.Add(language, new HashSet<DescriptionLine>());
        foreach (var langLine in langLines)
        {
            if (string.IsNullOrWhiteSpace(langLine))
            {
                continue;
            }

            var trimmed = langLine.Trim();

            if (int.TryParse(trimmed, out var _))
            {
                continue;
            }
            else if (trimmed.StartsWith("lang"))
            {
                language = GetLanguage(trimmed);
                descriptions.Add(language, new HashSet<DescriptionLine>());
                continue;
            }
            else
            {
                var parsed = new DescriptionLine(trimmed);
                descriptions[language].Add(parsed);
            }
        }
    }

    private static string[] ParseIds(string idsString)
    {
        var split = idsString.Split();
        var count = int.Parse(split[1]);
        var ids = split[2..];

        if (count != ids.Length)
        {
            throw new NotImplementedException();
        }

        return ids;
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
    /// Merges two descriptions.
    /// </summary>
    /// <param name="other">other description which is going to get merged.</param>
    internal void Merge(Description other)
    {
        foreach (var (key, otherSet) in other.descriptions)
        {
            if (descriptions.TryGetValue(key, out var set))
            {
                set.UnionWith(otherSet);
            }
            else
            {
                descriptions.Add(key, otherSet);
            }
        }
    }

    /// <summary>
    /// Formats the description line with values.
    /// </summary>
    /// <typeparam name="T">Type of the list.</typeparam>
    /// <param name="values">values to fill the description with.</param>
    /// <param name="language">language to use.</param>
    /// <param name="getReminderText">adds reminder text if set to <see langword="true"></see>.</param>
    /// <returns>formatted description.</returns>
    /// <exception cref="ArgumentNullException">thrown when <paramref name="values"/> is null.</exception>
    /// <exception cref="NotImplementedException">thrown when no matching description was found.</exception>
    public string Format<T>(IReadOnlyList<T> values, Language language = Language.English, bool getReminderText = false)
        where T : INumber<T>
    {
        if (values is null)
        {
            throw new ArgumentNullException(nameof(values));
        }

        var boxedValues = new object[values.Count];
        for (var i = 0; i < values.Count; i++)
        {
            var value = values[i];
            boxedValues[i] = value;
        }

        foreach (var item in descriptions[language])
        {
            if (item.IsMatching(boxedValues))
            {
                return item.Format(boxedValues);
            }
        }

        throw new NotImplementedException();
    }
}
