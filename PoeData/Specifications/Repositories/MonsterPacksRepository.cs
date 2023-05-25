using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterPacksDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterPacksRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterPacksDat> Items { get; }

    private Dictionary<string, List<MonsterPacksDat>>? byId;
    private Dictionary<int, List<MonsterPacksDat>>? byWorldAreasKeys;
    private Dictionary<int, List<MonsterPacksDat>>? byUnknown24;
    private Dictionary<int, List<MonsterPacksDat>>? byUnknown28;
    private Dictionary<int, List<MonsterPacksDat>>? byBossMonsterSpawnChance;
    private Dictionary<int, List<MonsterPacksDat>>? byBossMonsterCount;
    private Dictionary<int, List<MonsterPacksDat>>? byBossMonster_MonsterVarietiesKeys;
    private Dictionary<bool, List<MonsterPacksDat>>? byUnknown56;
    private Dictionary<int, List<MonsterPacksDat>>? byUnknown57;
    private Dictionary<string, List<MonsterPacksDat>>? byUnknown61;
    private Dictionary<int, List<MonsterPacksDat>>? byTagsKeys;
    private Dictionary<int, List<MonsterPacksDat>>? byMinLevel;
    private Dictionary<int, List<MonsterPacksDat>>? byMaxLevel;
    private Dictionary<int, List<MonsterPacksDat>>? byWorldAreas2;
    private Dictionary<int, List<MonsterPacksDat>>? byUnknown117;
    private Dictionary<int, List<MonsterPacksDat>>? byPackFormation;
    private Dictionary<int, List<MonsterPacksDat>>? byUnknown137;
    private Dictionary<bool, List<MonsterPacksDat>>? byUnknown141;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterPacksRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterPacksRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterPacksDat? item)
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
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
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterPacksDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<string, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKeys(int? key, out MonsterPacksDat? item)
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKeys(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
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
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byWorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByWorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.BossMonsterSpawnChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBossMonsterSpawnChance(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBossMonsterSpawnChance(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.BossMonsterSpawnChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBossMonsterSpawnChance(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byBossMonsterSpawnChance is null)
        {
            byBossMonsterSpawnChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.BossMonsterSpawnChance;

                if (!byBossMonsterSpawnChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBossMonsterSpawnChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBossMonsterSpawnChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byBossMonsterSpawnChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByBossMonsterSpawnChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBossMonsterSpawnChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.BossMonsterCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBossMonsterCount(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBossMonsterCount(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.BossMonsterCount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBossMonsterCount(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byBossMonsterCount is null)
        {
            byBossMonsterCount = new();
            foreach (var item in Items)
            {
                var itemKey = item.BossMonsterCount;

                if (!byBossMonsterCount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBossMonsterCount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBossMonsterCount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byBossMonsterCount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByBossMonsterCount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBossMonsterCount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.BossMonster_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBossMonster_MonsterVarietiesKeys(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBossMonster_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.BossMonster_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBossMonster_MonsterVarietiesKeys(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byBossMonster_MonsterVarietiesKeys is null)
        {
            byBossMonster_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BossMonster_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBossMonster_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBossMonster_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBossMonster_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byBossMonster_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByBossMonster_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBossMonster_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterPacksDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<bool, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown57(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown57(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown57(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown57 is null)
        {
            byUnknown57 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown57;

                if (!byUnknown57.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown57.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown57.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown57"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByUnknown57(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown57(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(string? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown61(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(string? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown61 is null)
        {
            byUnknown61 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown61;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown61.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown61.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown61.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterPacksDat>> GetManyToManyByUnknown61(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<string, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKeys(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKeys(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKeys(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byTagsKeys is null)
        {
            byTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.WorldAreas2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreas2(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreas2(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.WorldAreas2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreas2(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byWorldAreas2 is null)
        {
            byWorldAreas2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreas2;
                foreach (var listKey in itemKey)
                {
                    if (!byWorldAreas2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWorldAreas2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWorldAreas2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byWorldAreas2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByWorldAreas2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreas2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown117(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown117(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown117(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown117 is null)
        {
            byUnknown117 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown117;

                if (!byUnknown117.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown117.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown117.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown117"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByUnknown117(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown117(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.PackFormation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPackFormation(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPackFormation(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.PackFormation"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPackFormation(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byPackFormation is null)
        {
            byPackFormation = new();
            foreach (var item in Items)
            {
                var itemKey = item.PackFormation;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPackFormation.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPackFormation.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPackFormation.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byPackFormation"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByPackFormation(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPackFormation(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown137(int? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown137(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown137(int? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown137 is null)
        {
            byUnknown137 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown137;

                if (!byUnknown137.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown137.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown137.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown137"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterPacksDat>> GetManyToManyByUnknown137(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<int, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown137(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown141(bool? key, out MonsterPacksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown141(key, out var items))
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
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown141(bool? key, out IReadOnlyList<MonsterPacksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        if (byUnknown141 is null)
        {
            byUnknown141 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown141;

                if (!byUnknown141.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown141.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown141.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterPacksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterPacksDat"/> with <see cref="MonsterPacksDat.byUnknown141"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterPacksDat>> GetManyToManyByUnknown141(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterPacksDat>>();
        }

        var items = new List<ResultItem<bool, MonsterPacksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown141(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterPacksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterPacksDat[] Load()
    {
        const string filePath = "Data/MonsterPacks.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BossMonsterSpawnChance
            (var bossmonsterspawnchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BossMonsterCount
            (var bossmonstercountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BossMonster_MonsterVarietiesKeys
            (var tempbossmonster_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bossmonster_monstervarietieskeysLoading = tempbossmonster_monstervarietieskeysLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var tempunknown61Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown61Loading = tempunknown61Loading.AsReadOnly();

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WorldAreas2
            (var tempworldareas2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareas2Loading = tempworldareas2Loading.AsReadOnly();

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PackFormation
            (var packformationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterPacksDat()
            {
                Id = idLoading,
                WorldAreasKeys = worldareaskeysLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                BossMonsterSpawnChance = bossmonsterspawnchanceLoading,
                BossMonsterCount = bossmonstercountLoading,
                BossMonster_MonsterVarietiesKeys = bossmonster_monstervarietieskeysLoading,
                Unknown56 = unknown56Loading,
                Unknown57 = unknown57Loading,
                Unknown61 = unknown61Loading,
                TagsKeys = tagskeysLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                WorldAreas2 = worldareas2Loading,
                Unknown117 = unknown117Loading,
                PackFormation = packformationLoading,
                Unknown137 = unknown137Loading,
                Unknown141 = unknown141Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
