namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class containing helper methods for delve exporters.
/// </summary>
internal static class DelveExporterHelper
{
    /// <summary>
    /// Parses <see cref="PoeData.Specifications.DatFiles.DelveUpgradesDat.DelveUpgradeTypeKey"/> to a string.
    /// </summary>
    /// <param name="key">key to parse.</param>
    /// <returns>string representation of the upgrade.</returns>
    /// <exception cref="NotImplementedException">thrown when unknown key is passed.</exception>
    internal static string GetDelveUpgradeStatString(int key) => key switch
    {
        0 => "sulphite_capacity",
        1 => "flare_capacity",
        2 => "dynamite_capacity",
        3 => "light_radius",
        4 => "flare_radius",
        5 => "dynamite_radius",
        6 => "unknown",
        7 => "dynamite_damage",
        8 => "darkness_resistance",
        9 => "flare_duration",
        _ => throw new NotImplementedException(),
    };
}
