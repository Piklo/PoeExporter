namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Attribute used to rename property names in lua converter.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal sealed class LuaPropertyNameAttribute : Attribute
{
    /// <summary>Gets overriden property name.</summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LuaPropertyNameAttribute"/> class.
    /// </summary>
    /// <param name="name">overriden name.</param>
    public LuaPropertyNameAttribute(string name)
    {
        Name = name;
    }
}
