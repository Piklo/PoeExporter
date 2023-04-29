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
internal readonly partial record struct LuaString(string Value, int Indentation)
{
    /// <summary>
    /// Generates a lua strings for a string.
    /// </summary>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="value">value of a lua variable.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    internal static LuaString[] Generate(string name, string? value, int indentation)
    {
        // do we ever want to export empty strings as ""?
        if (string.IsNullOrEmpty(value))
        {
            return Array.Empty<LuaString>();
        }

        var formattedValue = value.Replace("\r\n", "<br>");
        formattedValue = formattedValue.Replace("\"", "\\\"");

        return new LuaString[] { new LuaString($"{name} = \"{formattedValue}\",", indentation) };
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
    /// Generates lua strings for another <see cref="ILuaExporter{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of a lua exporter.</typeparam>
    /// <param name="name">name of a lua variable.</param>
    /// <param name="exporter">exporter to generater values from.</param>
    /// <param name="indentation">indentation of lua strings.</param>
    /// <returns>parsed lua strings.</returns>
    /// <exception cref="DefaultNonGeneratorMethodUsedException">Thrown when the generic method is used.
    /// Which means the generator failed to generate overload specific for the given T.</exception>
#pragma warning disable IDE0060 // Remove unused parameter
    internal static LuaString[] Generate<T>(string name, ILuaExporter<T> exporter, int indentation)
#pragma warning restore IDE0060 // Remove unused parameter
        where T : ILuaExporter<T>
    {
        throw new DefaultNonGeneratorMethodUsedException();
    }

    // private string GetName(Type type)
    // {
    //     var attribute = Attribute.GetCustomAttribute(type, typeof(LuaPropertyNameAttribute)) as LuaPropertyNameAttribute;
    //     var name = attribute is not null ? attribute.Name : type.Name;
    //     return name;
    // }
}
