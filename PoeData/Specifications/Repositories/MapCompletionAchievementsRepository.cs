using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapCompletionAchievementsDat"/> related data and helper methods.
/// </summary>
public sealed class MapCompletionAchievementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapCompletionAchievementsDat> Items { get; }

    private Dictionary<string, List<MapCompletionAchievementsDat>>? byUnknown0;
    private Dictionary<int, List<MapCompletionAchievementsDat>>? byMapStatConditionsKeys;
    private Dictionary<int, List<MapCompletionAchievementsDat>>? byStatsKeys;
    private Dictionary<int, List<MapCompletionAchievementsDat>>? byAchievementItemsKeys;
    private Dictionary<int, List<MapCompletionAchievementsDat>>? byMapTierAchievementsKeys;
    private Dictionary<bool, List<MapCompletionAchievementsDat>>? byUnknown72;
    private Dictionary<int, List<MapCompletionAchievementsDat>>? byWorldAreasKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapCompletionAchievementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapCompletionAchievementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(string? key, out MapCompletionAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(string? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;

                if (!byUnknown0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapCompletionAchievementsDat>> GetManyToManyByUnknown0(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<string, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.MapStatConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapStatConditionsKeys(int? key, out MapCompletionAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapStatConditionsKeys(key, out var items))
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.MapStatConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapStatConditionsKeys(int? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        if (byMapStatConditionsKeys is null)
        {
            byMapStatConditionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapStatConditionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMapStatConditionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapStatConditionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapStatConditionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byMapStatConditionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCompletionAchievementsDat>> GetManyToManyByMapStatConditionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapStatConditionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out MapCompletionAchievementsDat? item)
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
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
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCompletionAchievementsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out MapCompletionAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        if (byAchievementItemsKeys is null)
        {
            byAchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCompletionAchievementsDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.MapTierAchievementsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapTierAchievementsKeys(int? key, out MapCompletionAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapTierAchievementsKeys(key, out var items))
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.MapTierAchievementsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapTierAchievementsKeys(int? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        if (byMapTierAchievementsKeys is null)
        {
            byMapTierAchievementsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapTierAchievementsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMapTierAchievementsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMapTierAchievementsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMapTierAchievementsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byMapTierAchievementsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCompletionAchievementsDat>> GetManyToManyByMapTierAchievementsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapTierAchievementsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(bool? key, out MapCompletionAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(bool? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapCompletionAchievementsDat>> GetManyToManyByUnknown72(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<bool, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKeys(int? key, out MapCompletionAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKeys(int? key, out IReadOnlyList<MapCompletionAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        if (byWorldAreasKeys is null)
        {
            byWorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapCompletionAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapCompletionAchievementsDat"/> with <see cref="MapCompletionAchievementsDat.byWorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapCompletionAchievementsDat>> GetManyToManyByWorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapCompletionAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MapCompletionAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapCompletionAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapCompletionAchievementsDat[] Load()
    {
        const string filePath = "Data/MapCompletionAchievements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapCompletionAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapStatConditionsKeys
            (var tempmapstatconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mapstatconditionskeysLoading = tempmapstatconditionskeysLoading.AsReadOnly();

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading MapTierAchievementsKeys
            (var tempmaptierachievementskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var maptierachievementskeysLoading = tempmaptierachievementskeysLoading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapCompletionAchievementsDat()
            {
                Unknown0 = unknown0Loading,
                MapStatConditionsKeys = mapstatconditionskeysLoading,
                StatsKeys = statskeysLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                MapTierAchievementsKeys = maptierachievementskeysLoading,
                Unknown72 = unknown72Loading,
                WorldAreasKeys = worldareaskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
