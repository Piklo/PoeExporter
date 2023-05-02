using PoeData.Specifications.DatFiles;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Contains data about delve upgrade types <see cref="DelveUpgradesDat"/>.
/// </summary>
internal enum DelveUpgradeTypes
{
    SULPHITE_CAPACITY = 0,
    FLARE_CAPACITY = 1,
    DYNAMITE_CAPACITY = 2,
    LIGHT_RADIUS = 3,
    FLARE_RADIUS = 4,
    DYNAMITE_RADIUS = 5,
    UNKNOWN = 6, // 6 is unused atm
    DYNAMITE_DAMAGE = 7,
    DARKNESS_RESISTANCE = 8,
    FLARE_DURATION = 9,
}