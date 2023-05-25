using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterDeathAchievementsDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterDeathAchievementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterDeathAchievementsDat> Items { get; }

    private Dictionary<string, List<MonsterDeathAchievementsDat>>? byId;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byMonsterVarietiesKeys;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byAchievementItemsKeys;
    private Dictionary<bool, List<MonsterDeathAchievementsDat>>? byUnknown40;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byPlayerConditionsKeys;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byMonsterDeathConditionsKeys;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown73;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown89;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown93;
    private Dictionary<bool, List<MonsterDeathAchievementsDat>>? byUnknown97;
    private Dictionary<bool, List<MonsterDeathAchievementsDat>>? byUnknown98;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown99;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown115;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown131;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown147;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown163;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown179;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byNearbyMonsterConditionsKeys;
    private Dictionary<bool, List<MonsterDeathAchievementsDat>>? byUnknown199;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byMultiPartAchievementConditionsKeys;
    private Dictionary<int, List<MonsterDeathAchievementsDat>>? byUnknown216;
    private Dictionary<bool, List<MonsterDeathAchievementsDat>>? byUnknown220;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterDeathAchievementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterDeathAchievementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byId is null)
        {
            byId = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id;

                if (!byId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterDeathAchievementsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<string, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKeys(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKeys(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byMonsterVarietiesKeys is null)
        {
            byMonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byMonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByMonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out MonsterDeathAchievementsDat? item)
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
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
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(bool? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(bool? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathAchievementsDat>> GetManyToManyByUnknown40(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.PlayerConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayerConditionsKeys(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayerConditionsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.PlayerConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayerConditionsKeys(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byPlayerConditionsKeys is null)
        {
            byPlayerConditionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.PlayerConditionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPlayerConditionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPlayerConditionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPlayerConditionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byPlayerConditionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByPlayerConditionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayerConditionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.MonsterDeathConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterDeathConditionsKeys(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterDeathConditionsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.MonsterDeathConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterDeathConditionsKeys(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byMonsterDeathConditionsKeys is null)
        {
            byMonsterDeathConditionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterDeathConditionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterDeathConditionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterDeathConditionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterDeathConditionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byMonsterDeathConditionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByMonsterDeathConditionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterDeathConditionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown73(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown73 is null)
        {
            byUnknown73 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown73;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown73.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown73.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown73.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown73(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown89(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown89(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown89(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown89 is null)
        {
            byUnknown89 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown89;

                if (!byUnknown89.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown89.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown89.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown89"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown89(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown89(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown93"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown93(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown93(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown93"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown93(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown93 is null)
        {
            byUnknown93 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown93;

                if (!byUnknown93.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown93.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown93.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown93"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown93(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown93(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown97(bool? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown97(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown97(bool? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown97 is null)
        {
            byUnknown97 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown97;

                if (!byUnknown97.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown97.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown97.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown97"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathAchievementsDat>> GetManyToManyByUnknown97(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown97(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown98(bool? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown98(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown98(bool? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown98 is null)
        {
            byUnknown98 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown98;

                if (!byUnknown98.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown98.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown98.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown98"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathAchievementsDat>> GetManyToManyByUnknown98(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown98(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown99(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown99(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown99(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown99 is null)
        {
            byUnknown99 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown99;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown99.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown99.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown99.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown99"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown99(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown99(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown115(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown115 is null)
        {
            byUnknown115 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown115;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown115.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown115.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown115.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown115(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown131"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown131(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown131(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown131"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown131(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown131 is null)
        {
            byUnknown131 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown131;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown131.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown131.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown131.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown131"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown131(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown131(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown147"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown147(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown147(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown147"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown147(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown147 is null)
        {
            byUnknown147 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown147;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown147.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown147.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown147.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown147"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown147(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown147(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown163"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown163(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown163(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown163"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown163(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown163 is null)
        {
            byUnknown163 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown163;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown163.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown163.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown163.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown163"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown163(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown163(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown179"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown179(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown179(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown179"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown179(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown179 is null)
        {
            byUnknown179 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown179;

                if (!byUnknown179.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown179.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown179.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown179"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown179(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown179(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.NearbyMonsterConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNearbyMonsterConditionsKeys(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNearbyMonsterConditionsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.NearbyMonsterConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNearbyMonsterConditionsKeys(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byNearbyMonsterConditionsKeys is null)
        {
            byNearbyMonsterConditionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.NearbyMonsterConditionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byNearbyMonsterConditionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNearbyMonsterConditionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNearbyMonsterConditionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byNearbyMonsterConditionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByNearbyMonsterConditionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNearbyMonsterConditionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown199"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown199(bool? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown199(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown199"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown199(bool? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown199 is null)
        {
            byUnknown199 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown199;

                if (!byUnknown199.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown199.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown199.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown199"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathAchievementsDat>> GetManyToManyByUnknown199(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown199(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.MultiPartAchievementConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMultiPartAchievementConditionsKeys(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMultiPartAchievementConditionsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.MultiPartAchievementConditionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMultiPartAchievementConditionsKeys(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byMultiPartAchievementConditionsKeys is null)
        {
            byMultiPartAchievementConditionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MultiPartAchievementConditionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMultiPartAchievementConditionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMultiPartAchievementConditionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMultiPartAchievementConditionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byMultiPartAchievementConditionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByMultiPartAchievementConditionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMultiPartAchievementConditionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown216(int? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown216(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown216(int? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown216 is null)
        {
            byUnknown216 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown216;

                if (!byUnknown216.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown216.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown216.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown216"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathAchievementsDat>> GetManyToManyByUnknown216(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown216(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown220(bool? key, out MonsterDeathAchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown220(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown220(bool? key, out IReadOnlyList<MonsterDeathAchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        if (byUnknown220 is null)
        {
            byUnknown220 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown220;

                if (!byUnknown220.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown220.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown220.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathAchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathAchievementsDat"/> with <see cref="MonsterDeathAchievementsDat.byUnknown220"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathAchievementsDat>> GetManyToManyByUnknown220(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathAchievementsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathAchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown220(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathAchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterDeathAchievementsDat[] Load()
    {
        const string filePath = "Data/MonsterDeathAchievements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterDeathAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKeys
            (var tempmonstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeysLoading = tempmonstervarietieskeysLoading.AsReadOnly();

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PlayerConditionsKeys
            (var tempplayerconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var playerconditionskeysLoading = tempplayerconditionskeysLoading.AsReadOnly();

            // loading MonsterDeathConditionsKeys
            (var tempmonsterdeathconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterdeathconditionskeysLoading = tempmonsterdeathconditionskeysLoading.AsReadOnly();

            // loading Unknown73
            (var tempunknown73Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown73Loading = tempunknown73Loading.AsReadOnly();

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown93
            (var unknown93Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown115
            (var tempunknown115Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown115Loading = tempunknown115Loading.AsReadOnly();

            // loading Unknown131
            (var tempunknown131Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown131Loading = tempunknown131Loading.AsReadOnly();

            // loading Unknown147
            (var tempunknown147Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown147Loading = tempunknown147Loading.AsReadOnly();

            // loading Unknown163
            (var unknown163Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown179
            (var unknown179Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading NearbyMonsterConditionsKeys
            (var tempnearbymonsterconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var nearbymonsterconditionskeysLoading = tempnearbymonsterconditionskeysLoading.AsReadOnly();

            // loading Unknown199
            (var unknown199Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MultiPartAchievementConditionsKeys
            (var tempmultipartachievementconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var multipartachievementconditionskeysLoading = tempmultipartachievementconditionskeysLoading.AsReadOnly();

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterDeathAchievementsDat()
            {
                Id = idLoading,
                MonsterVarietiesKeys = monstervarietieskeysLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown40 = unknown40Loading,
                PlayerConditionsKeys = playerconditionskeysLoading,
                MonsterDeathConditionsKeys = monsterdeathconditionskeysLoading,
                Unknown73 = unknown73Loading,
                Unknown89 = unknown89Loading,
                Unknown93 = unknown93Loading,
                Unknown97 = unknown97Loading,
                Unknown98 = unknown98Loading,
                Unknown99 = unknown99Loading,
                Unknown115 = unknown115Loading,
                Unknown131 = unknown131Loading,
                Unknown147 = unknown147Loading,
                Unknown163 = unknown163Loading,
                Unknown179 = unknown179Loading,
                NearbyMonsterConditionsKeys = nearbymonsterconditionskeysLoading,
                Unknown199 = unknown199Loading,
                MultiPartAchievementConditionsKeys = multipartachievementconditionskeysLoading,
                Unknown216 = unknown216Loading,
                Unknown220 = unknown220Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
