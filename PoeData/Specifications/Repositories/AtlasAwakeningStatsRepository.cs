using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasAwakeningStatsDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasAwakeningStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasAwakeningStatsDat> Items { get; }

    private Dictionary<int, List<AtlasAwakeningStatsDat>>? byAwakeningLevel;
    private Dictionary<int, List<AtlasAwakeningStatsDat>>? byStats;
    private Dictionary<int, List<AtlasAwakeningStatsDat>>? byValues;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasAwakeningStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasAwakeningStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.AwakeningLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAwakeningLevel(int? key, out AtlasAwakeningStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAwakeningLevel(key, out var items))
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
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.AwakeningLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAwakeningLevel(int? key, out IReadOnlyList<AtlasAwakeningStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasAwakeningStatsDat>();
            return false;
        }

        if (byAwakeningLevel is null)
        {
            byAwakeningLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AwakeningLevel;

                if (!byAwakeningLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAwakeningLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAwakeningLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasAwakeningStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.byAwakeningLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasAwakeningStatsDat>> GetManyToManyByAwakeningLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasAwakeningStatsDat>>();
        }

        var items = new List<ResultItem<int, AtlasAwakeningStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAwakeningLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasAwakeningStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStats(int? key, out AtlasAwakeningStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStats(key, out var items))
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
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStats(int? key, out IReadOnlyList<AtlasAwakeningStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasAwakeningStatsDat>();
            return false;
        }

        if (byStats is null)
        {
            byStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stats;
                foreach (var listKey in itemKey)
                {
                    if (!byStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasAwakeningStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.byStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasAwakeningStatsDat>> GetManyToManyByStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasAwakeningStatsDat>>();
        }

        var items = new List<ResultItem<int, AtlasAwakeningStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasAwakeningStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByValues(int? key, out AtlasAwakeningStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByValues(key, out var items))
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
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByValues(int? key, out IReadOnlyList<AtlasAwakeningStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasAwakeningStatsDat>();
            return false;
        }

        if (byValues is null)
        {
            byValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.Values;
                foreach (var listKey in itemKey)
                {
                    if (!byValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasAwakeningStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasAwakeningStatsDat"/> with <see cref="AtlasAwakeningStatsDat.byValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasAwakeningStatsDat>> GetManyToManyByValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasAwakeningStatsDat>>();
        }

        var items = new List<ResultItem<int, AtlasAwakeningStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasAwakeningStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasAwakeningStatsDat[] Load()
    {
        const string filePath = "Data/AtlasAwakeningStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasAwakeningStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AwakeningLevel
            (var awakeninglevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading Values
            (var tempvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var valuesLoading = tempvaluesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasAwakeningStatsDat()
            {
                AwakeningLevel = awakeninglevelLoading,
                Stats = statsLoading,
                Values = valuesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
