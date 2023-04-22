using System.Numerics;

namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Record of a lua string.
/// </summary>
/// <param name="Value">value of the lua string.</param>
/// <param name="Indentation">indentation of the lua string.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "StyleCop.CSharp.NamingRules",
    "SA1313:Parameter names should begin with lower-case letter",
    Justification = "its a record not a method with parameters :ICANT:")]
internal readonly record struct LuaString(string Value, int Indentation)
{
    /// <summary>
    /// Generates a lua strings for a string.
    /// </summary>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="value">value of a lua variable.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    internal static LuaString[] Generate(string name, string value, int indentation)
    {
        return new LuaString[] { new LuaString($"{name} = \"{value}\",", indentation) };
    }

    /// <summary>
    /// Generates a lua strings for a list of strings.
    /// </summary>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="values">values of a lua variable.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    internal static LuaString[] Generate(string name, IReadOnlyList<string> values, int indentation)
    {
        var strings = new List<LuaString>();

        var tagsBracket = new LuaString($"{name} = {{", indentation);
        strings.Add(tagsBracket);
        indentation++;

        foreach (var value in values)
        {
            var tagLuaStr = new LuaString($"\"{value}\",", indentation);
            strings.Add(tagLuaStr);
        }

        indentation--;
        var endTagsBracket = new LuaString("},", indentation);
        strings.Add(endTagsBracket);

        return strings.ToArray();
    }

    /// <summary>
    /// Generates a lua strings for a number.
    /// </summary>
    /// <typeparam name="T">number type.</typeparam>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="value">value of a lua variable.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    internal static LuaString[] Generate<T>(string name, T value, int indentation)
        where T : INumber<T>
    {
        return new LuaString[] { new LuaString($"{name} = {value},", indentation) };
    }

    /// <summary>
    /// Generates a lua strings for a list of numbers.
    /// </summary>
    /// <typeparam name="T">number type.</typeparam>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="values">values of a lua variable.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    internal static LuaString[] Generate<T>(string name, IReadOnlyList<T> values, int indentation)
        where T : INumber<T>
    {
        var strings = new List<LuaString>();

        var tagsBracket = new LuaString($"{name} = {{", indentation);
        strings.Add(tagsBracket);
        indentation++;

        foreach (var value in values)
        {
            var tagLuaStr = new LuaString($"{value},", indentation);
            strings.Add(tagLuaStr);
        }

        indentation--;
        var endTagsBracket = new LuaString("},", indentation);
        strings.Add(endTagsBracket);

        return strings.ToArray();
    }

    /// <summary>
    /// Generates a lua strings for a another <see cref="ILuaExporter{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of a lua exporter.</typeparam>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="exporter">exporter to generater values from.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    internal static LuaString[] Generate<T>(string name, ILuaExporter<T> exporter, int indentation)
    {
        var strings = exporter.ToLuaStrings(indentation);
        var first = strings[0];
        var overridenFirst = new LuaString($"{name} = {first.Value}", indentation);
        strings[0] = overridenFirst;

        return strings;
    }

    // private string GetName(Type type)
    // {
    //     var attribute = Attribute.GetCustomAttribute(type, typeof(LuaPropertyNameAttribute)) as LuaPropertyNameAttribute;
    //     var name = attribute is not null ? attribute.Name : type.Name;
    //     return name;
    // }
}
