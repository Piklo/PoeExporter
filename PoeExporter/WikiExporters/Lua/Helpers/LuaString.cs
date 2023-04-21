namespace PoeExporter.WikiExporters.Lua.Helpers;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "StyleCop.CSharp.NamingRules",
    "SA1313:Parameter names should begin with lower-case letter",
    Justification = "its a record not a method with parameters :ICANT:")]
internal readonly record struct LuaString(string Value, int Indentation);
