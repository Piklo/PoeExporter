using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AdditionalMonsterPacksFromStatsDat"/> related data and helper methods.
/// </summary>
public sealed class AdditionalMonsterPacksFromStatsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AdditionalMonsterPacksFromStatsDat> Items { get; }

    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byStatsKey;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byUnknown16;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byMonsterPacksKeys;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byAdditionalMonsterPacksStatMode;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byPackCountStatsKey;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byStatsKeys;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byStatsValues;
    private Dictionary<int, List<AdditionalMonsterPacksFromStatsDat>>? byUnknown88;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdditionalMonsterPacksFromStatsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AdditionalMonsterPacksFromStatsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out AdditionalMonsterPacksFromStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key.Value);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key.Value);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out AdditionalMonsterPacksFromStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.MonsterPacksKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterPacksKeys(int? key, out AdditionalMonsterPacksFromStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterPacksKeys(key, out var items))
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
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.MonsterPacksKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterPacksKeys(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        if (byMonsterPacksKeys is null)
        {
            byMonsterPacksKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterPacksKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterPacksKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterPacksKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterPacksKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byMonsterPacksKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByMonsterPacksKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterPacksKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.AdditionalMonsterPacksStatMode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAdditionalMonsterPacksStatMode(int? key, out AdditionalMonsterPacksFromStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAdditionalMonsterPacksStatMode(key, out var items))
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
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.AdditionalMonsterPacksStatMode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAdditionalMonsterPacksStatMode(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        if (byAdditionalMonsterPacksStatMode is null)
        {
            byAdditionalMonsterPacksStatMode = new();
            foreach (var item in Items)
            {
                var itemKey = item.AdditionalMonsterPacksStatMode;

                if (!byAdditionalMonsterPacksStatMode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAdditionalMonsterPacksStatMode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAdditionalMonsterPacksStatMode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byAdditionalMonsterPacksStatMode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByAdditionalMonsterPacksStatMode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAdditionalMonsterPacksStatMode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.PackCountStatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPackCountStatsKey(int? key, out AdditionalMonsterPacksFromStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPackCountStatsKey(key, out var items))
        {
            item = null;
            return false;
        }

        if (items.Count == 0)
        {
            logger.Warning("failed to find item with key = {key}", key.Value);
            item = null;
            return false;
        }

        if (items.Count > 1)
        {
            logger.Warning("found too many items with key = {key}", key.Value);
            item = null;
            return false;
        }

        item = items[0];
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.PackCountStatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPackCountStatsKey(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        if (byPackCountStatsKey is null)
        {
            byPackCountStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.PackCountStatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPackCountStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPackCountStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPackCountStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byPackCountStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByPackCountStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPackCountStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out AdditionalMonsterPacksFromStatsDat? item)
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
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
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
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.StatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsValues(int? key, out AdditionalMonsterPacksFromStatsDat? item)
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
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.StatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsValues(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
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
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byStatsValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByStatsValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out AdditionalMonsterPacksFromStatsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<AdditionalMonsterPacksFromStatsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AdditionalMonsterPacksFromStatsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AdditionalMonsterPacksFromStatsDat"/> with <see cref="AdditionalMonsterPacksFromStatsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AdditionalMonsterPacksFromStatsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();
        }

        var items = new List<ResultItem<int, AdditionalMonsterPacksFromStatsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AdditionalMonsterPacksFromStatsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AdditionalMonsterPacksFromStatsDat[] Load()
    {
        const string filePath = "Data/AdditionalMonsterPacksFromStats.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AdditionalMonsterPacksFromStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            // loading AdditionalMonsterPacksStatMode
            (var additionalmonsterpacksstatmodeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PackCountStatsKey
            (var packcountstatskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AdditionalMonsterPacksFromStatsDat()
            {
                StatsKey = statskeyLoading,
                Unknown16 = unknown16Loading,
                MonsterPacksKeys = monsterpackskeysLoading,
                AdditionalMonsterPacksStatMode = additionalmonsterpacksstatmodeLoading,
                PackCountStatsKey = packcountstatskeyLoading,
                StatsKeys = statskeysLoading,
                StatsValues = statsvaluesLoading,
                Unknown88 = unknown88Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
