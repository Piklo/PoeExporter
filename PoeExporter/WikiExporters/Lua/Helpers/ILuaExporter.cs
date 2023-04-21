namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Interface with lua exporter methods.
/// </summary>
/// <typeparam name="T">type of the exporter.</typeparam>
internal interface ILuaExporter<T>
{
    /// <summary>
    /// Converts the object to lua strings.
    /// </summary>
    /// <param name="currentIndentation">current indentation</param>
    /// <returns>converted object in lua strings.</returns>
    public LuaString[] ToLuaStrings(int currentIndentation = 0);
}
