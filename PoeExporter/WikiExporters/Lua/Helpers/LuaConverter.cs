using PoeExporter.WikiExporters.Lua.Helpers;
using System.Text;

namespace PoeExporter.WikiExporters.Lua;

/// <summary>
/// Class used to convert c# classes to lua objects.
/// </summary>
internal static class LuaConverter
{
    /// <summary>
    /// Converts a list of c# objects to lua string.
    /// </summary>
    /// <typeparam name="T">type of the object.</typeparam>
    /// <param name="items">items.</param>
    /// <returns>converted objects in lua string.</returns>
    public static string ToLuaString<T>(IReadOnlyList<T> items)
        where T : ILuaExporter<T>
    {
        var builder = new StringBuilder();
        builder.AppendLine("local data = {");
        var baseIndentation = 1; // from the data indentation

        foreach (var item in items)
        {
            var strings = item.ToLuaStrings(baseIndentation);
            foreach (var line in strings)
            {
                builder.AppendLine($"{new string('\t', line.Indentation)}{line.Value}");
            }
        }

        builder.AppendLine("}");
        builder.AppendLine("return data");

        var str = builder.ToString();
        return str;
    }
}
