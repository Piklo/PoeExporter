using PoeData.Specifications.SpecificationFiles;
using Serilog;
using System.Collections.ObjectModel;

namespace PoeData.Specifications;

/// <summary>
/// Class containing all Path of Exile data.
/// </summary>
public sealed class Specification
{
    /// <summary>Gets data loader.</summary>
    internal DataLoader DataLoader { get; }

    private AbyssObjects[]? abyssObjects;
    private MonsterVarieties[]? monsterVarieties;
    private WorldAreas[]? worldAreas;

    /// <summary>
    /// Initializes a new instance of the <see cref="Specification"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public Specification(IConfig config, ILogger logger)
    {
        DataLoader = new DataLoader(config, logger);
    }

    /// <summary>
    /// Gets abyss objects data.
    /// </summary>
    /// <returns>readonly collection of abyss objects.</returns>
    public ReadOnlyCollection<AbyssObjects> GetAbyssObjects()
    {
        abyssObjects ??= AbyssObjects.Load(this);

        return abyssObjects.AsReadOnly();
    }

    /// <summary>
    /// Gets monster varieties data.
    /// </summary>
    /// <returns>readonly collection of monster varieties.</returns>
    public ReadOnlyCollection<MonsterVarieties> GetMonsterVarieties()
    {
        monsterVarieties ??= MonsterVarieties.Load(this, DataLoader);

        return monsterVarieties.AsReadOnly();
    }

    /// <summary>
    /// Gets world areas data.
    /// </summary>
    /// <returns>readonly collection of world areas.</returns>
    public ReadOnlyCollection<WorldAreas> GetWorldAreas()
    {
        worldAreas ??= WorldAreas.Load(this, DataLoader);

        return worldAreas.AsReadOnly();
    }
}
