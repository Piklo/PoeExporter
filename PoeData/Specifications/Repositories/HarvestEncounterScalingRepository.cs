using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestEncounterScalingDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestEncounterScalingRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestEncounterScalingDat> Items { get; }

    private Dictionary<int, List<HarvestEncounterScalingDat>>? byLevel;
    private Dictionary<float, List<HarvestEncounterScalingDat>>? byMultiplier;
    private Dictionary<int, List<HarvestEncounterScalingDat>>? byStatsKeys;
    private Dictionary<int, List<HarvestEncounterScalingDat>>? byStatsValues;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestEncounterScalingRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestEncounterScalingRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out HarvestEncounterScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<HarvestEncounterScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestEncounterScalingDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestEncounterScalingDat>>();
        }

        var items = new List<ResultItem<int, HarvestEncounterScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestEncounterScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.Multiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMultiplier(float? key, out HarvestEncounterScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMultiplier(key, out var items))
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
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.Multiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMultiplier(float? key, out IReadOnlyList<HarvestEncounterScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        if (byMultiplier is null)
        {
            byMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Multiplier;

                if (!byMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.byMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HarvestEncounterScalingDat>> GetManyToManyByMultiplier(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HarvestEncounterScalingDat>>();
        }

        var items = new List<ResultItem<float, HarvestEncounterScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HarvestEncounterScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out HarvestEncounterScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys(key, out var items))
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
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<HarvestEncounterScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        if (byStatsKeys is null)
        {
            byStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestEncounterScalingDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestEncounterScalingDat>>();
        }

        var items = new List<ResultItem<int, HarvestEncounterScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestEncounterScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.StatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsValues(int? key, out HarvestEncounterScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsValues(key, out var items))
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
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.StatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsValues(int? key, out IReadOnlyList<HarvestEncounterScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        if (byStatsValues is null)
        {
            byStatsValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsValues;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestEncounterScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestEncounterScalingDat"/> with <see cref="HarvestEncounterScalingDat.byStatsValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestEncounterScalingDat>> GetManyToManyByStatsValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestEncounterScalingDat>>();
        }

        var items = new List<ResultItem<int, HarvestEncounterScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestEncounterScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestEncounterScalingDat[] Load()
    {
        const string filePath = "Data/HarvestEncounterScaling.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestEncounterScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Multiplier
            (var multiplierLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestEncounterScalingDat()
            {
                Level = levelLoading,
                Multiplier = multiplierLoading,
                StatsKeys = statskeysLoading,
                StatsValues = statsvaluesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
