using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="QuestStaticRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class QuestStaticRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<QuestStaticRewardsDat> Items { get; }

    private Dictionary<int, List<QuestStaticRewardsDat>>? byQuestFlag;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byUnknown16;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byStatsKeys;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byStatValues;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byQuestKey;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byUnknown68;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byClientStringsKey;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byUnknown88;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byStatValuesHardmode;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byClientStringHardmode;
    private Dictionary<int, List<QuestStaticRewardsDat>>? byUnknown124;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestStaticRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal QuestStaticRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestFlag(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestFlag(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestFlag(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byQuestFlag is null)
        {
            byQuestFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestFlag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestFlag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestFlag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestFlag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byQuestFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByQuestFlag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out QuestStaticRewardsDat? item)
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
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
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out QuestStaticRewardsDat? item)
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
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
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValues(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValues(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValues(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byStatValues is null)
        {
            byStatValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValues;
                foreach (var listKey in itemKey)
                {
                    if (!byStatValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byStatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByStatValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.QuestKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestKey(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestKey(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.QuestKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestKey(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byQuestKey is null)
        {
            byQuestKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byQuestKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByQuestKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.ClientStringsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientStringsKey(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientStringsKey(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.ClientStringsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientStringsKey(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byClientStringsKey is null)
        {
            byClientStringsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientStringsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClientStringsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClientStringsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClientStringsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byClientStringsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByClientStringsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientStringsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out QuestStaticRewardsDat? item)
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
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
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.StatValuesHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValuesHardmode(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValuesHardmode(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.StatValuesHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValuesHardmode(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byStatValuesHardmode is null)
        {
            byStatValuesHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValuesHardmode;
                foreach (var listKey in itemKey)
                {
                    if (!byStatValuesHardmode.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatValuesHardmode.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatValuesHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byStatValuesHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByStatValuesHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValuesHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.ClientStringHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClientStringHardmode(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClientStringHardmode(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.ClientStringHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClientStringHardmode(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byClientStringHardmode is null)
        {
            byClientStringHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClientStringHardmode;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byClientStringHardmode.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byClientStringHardmode.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byClientStringHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byClientStringHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByClientStringHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClientStringHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out QuestStaticRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown124(key, out var items))
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
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<QuestStaticRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;

                if (!byUnknown124.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown124.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestStaticRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestStaticRewardsDat"/> with <see cref="QuestStaticRewardsDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestStaticRewardsDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestStaticRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestStaticRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestStaticRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private QuestStaticRewardsDat[] Load()
    {
        const string filePath = "Data/QuestStaticRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestStaticRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading QuestKey
            (var questkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ClientStringsKey
            (var clientstringskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatValuesHardmode
            (var tempstatvalueshardmodeLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvalueshardmodeLoading = tempstatvalueshardmodeLoading.AsReadOnly();

            // loading ClientStringHardmode
            (var clientstringhardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestStaticRewardsDat()
            {
                QuestFlag = questflagLoading,
                Unknown16 = unknown16Loading,
                StatsKeys = statskeysLoading,
                StatValues = statvaluesLoading,
                QuestKey = questkeyLoading,
                Unknown68 = unknown68Loading,
                ClientStringsKey = clientstringskeyLoading,
                Unknown88 = unknown88Loading,
                StatValuesHardmode = statvalueshardmodeLoading,
                ClientStringHardmode = clientstringhardmodeLoading,
                Unknown124 = unknown124Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
