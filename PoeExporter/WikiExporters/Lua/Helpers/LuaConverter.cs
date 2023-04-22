namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Class used to convert c# classes to lua objects.
/// </summary>
internal static partial class LuaConverter
{
    /// <summary>
    /// Converts a list of c# objects to lua string.
    /// </summary>
    /// <typeparam name="T">type of the object.</typeparam>
    /// <param name="items">items.</param>
    /// <returns>converted objects in lua string.</returns>
    /// <exception cref="DefaultNonGeneratorMethodUsedException">Thrown when the generic method is used.
    /// Which means the generator failed to generate overload specific for the given T.</exception>
#pragma warning disable IDE0060 // Remove unused parameter
    public static string ToLuaString<T>(IReadOnlyList<T> items)
#pragma warning restore IDE0060 // Remove unused parameter
        where T : ILuaExporter<T>
    {
        throw new DefaultNonGeneratorMethodUsedException();
    }

    /// <summary>
    /// Converts the object to lua strings.
    /// </summary>
    /// <typeparam name="T">type of the item.</typeparam>
    /// <param name="exporter">exporter to get lua strings from.</param>
    /// <param name="currentIndentation">current indentation.</param>
    /// <returns>converted object in lua strings.</returns>
    /// <exception cref="DefaultNonGeneratorMethodUsedException">Thrown when the generic method is used.
    /// Which means the generator failed to generate overload specific for the given T.</exception>
#pragma warning disable IDE0060 // Remove unused parameter
    public static LuaString[] ToLuaStrings<T>(ILuaExporter<T> exporter, int currentIndentation = 0)
#pragma warning restore IDE0060 // Remove unused parameter
        where T : ILuaExporter<T>
    {
        throw new DefaultNonGeneratorMethodUsedException();
    }
}
