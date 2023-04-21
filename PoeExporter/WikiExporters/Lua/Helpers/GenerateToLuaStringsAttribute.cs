namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Attribute used to auto generate <see cref="ILuaExporter{T}.ToLuaStrings(int)"/> methods.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
internal sealed class GenerateToLuaStringsAttribute : Attribute
{
}
