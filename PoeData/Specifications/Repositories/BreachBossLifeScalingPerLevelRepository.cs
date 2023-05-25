using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BreachBossLifeScalingPerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class BreachBossLifeScalingPerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BreachBossLifeScalingPerLevelDat> Items { get; }

    private Dictionary<int, List<BreachBossLifeScalingPerLevelDat>>? byMonsterLevel;
    private Dictionary<int, List<BreachBossLifeScalingPerLevelDat>>? byLifeMultiplier;

    /// <summary>
    /// Initializes a new instance of the <see cref="BreachBossLifeScalingPerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BreachBossLifeScalingPerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BreachBossLifeScalingPerLevelDat"/> with <see cref="BreachBossLifeScalingPerLevelDat.MonsterLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterLevel(int? key, out BreachBossLifeScalingPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterLevel(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachBossLifeScalingPerLevelDat"/> with <see cref="BreachBossLifeScalingPerLevelDat.MonsterLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterLevel(int? key, out IReadOnlyList<BreachBossLifeScalingPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachBossLifeScalingPerLevelDat>();
            return false;
        }

        if (byMonsterLevel is null)
        {
            byMonsterLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterLevel;

                if (!byMonsterLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachBossLifeScalingPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachBossLifeScalingPerLevelDat"/> with <see cref="BreachBossLifeScalingPerLevelDat.byMonsterLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachBossLifeScalingPerLevelDat>> GetManyToManyByMonsterLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachBossLifeScalingPerLevelDat>>();
        }

        var items = new List<ResultItem<int, BreachBossLifeScalingPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachBossLifeScalingPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BreachBossLifeScalingPerLevelDat"/> with <see cref="BreachBossLifeScalingPerLevelDat.LifeMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeMultiplier(int? key, out BreachBossLifeScalingPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeMultiplier(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachBossLifeScalingPerLevelDat"/> with <see cref="BreachBossLifeScalingPerLevelDat.LifeMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeMultiplier(int? key, out IReadOnlyList<BreachBossLifeScalingPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BreachBossLifeScalingPerLevelDat>();
            return false;
        }

        if (byLifeMultiplier is null)
        {
            byLifeMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifeMultiplier;

                if (!byLifeMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BreachBossLifeScalingPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BreachBossLifeScalingPerLevelDat"/> with <see cref="BreachBossLifeScalingPerLevelDat.byLifeMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BreachBossLifeScalingPerLevelDat>> GetManyToManyByLifeMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BreachBossLifeScalingPerLevelDat>>();
        }

        var items = new List<ResultItem<int, BreachBossLifeScalingPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BreachBossLifeScalingPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BreachBossLifeScalingPerLevelDat[] Load()
    {
        const string filePath = "Data/BreachBossLifeScalingPerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BreachBossLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterLevel
            (var monsterlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeMultiplier
            (var lifemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BreachBossLifeScalingPerLevelDat()
            {
                MonsterLevel = monsterlevelLoading,
                LifeMultiplier = lifemultiplierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
