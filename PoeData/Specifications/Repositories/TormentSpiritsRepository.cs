using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="TormentSpiritsDat"/> related data and helper methods.
/// </summary>
public sealed class TormentSpiritsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<TormentSpiritsDat> Items { get; }

    private Dictionary<int, List<TormentSpiritsDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<TormentSpiritsDat>>? bySpirit_ModsKeys;
    private Dictionary<int, List<TormentSpiritsDat>>? byTouched_ModsKeys;
    private Dictionary<int, List<TormentSpiritsDat>>? byPossessed_ModsKeys;
    private Dictionary<int, List<TormentSpiritsDat>>? byMinZoneLevel;
    private Dictionary<int, List<TormentSpiritsDat>>? byMaxZoneLevel;
    private Dictionary<int, List<TormentSpiritsDat>>? bySpawnWeight;
    private Dictionary<int, List<TormentSpiritsDat>>? bySummonedMonster_MonsterVarietiesKey;
    private Dictionary<int, List<TormentSpiritsDat>>? byUnknown92;
    private Dictionary<int, List<TormentSpiritsDat>>? byModsKeys0;
    private Dictionary<int, List<TormentSpiritsDat>>? byModsKeys1;

    /// <summary>
    /// Initializes a new instance of the <see cref="TormentSpiritsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal TormentSpiritsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Spirit_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpirit_ModsKeys(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpirit_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Spirit_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpirit_ModsKeys(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (bySpirit_ModsKeys is null)
        {
            bySpirit_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Spirit_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySpirit_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpirit_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpirit_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.bySpirit_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyBySpirit_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpirit_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Touched_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTouched_ModsKeys(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTouched_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Touched_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTouched_ModsKeys(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byTouched_ModsKeys is null)
        {
            byTouched_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Touched_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTouched_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTouched_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTouched_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byTouched_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByTouched_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTouched_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Possessed_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPossessed_ModsKeys(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPossessed_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Possessed_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPossessed_ModsKeys(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byPossessed_ModsKeys is null)
        {
            byPossessed_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Possessed_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byPossessed_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPossessed_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPossessed_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byPossessed_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByPossessed_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPossessed_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.MinZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinZoneLevel(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinZoneLevel(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.MinZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinZoneLevel(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byMinZoneLevel is null)
        {
            byMinZoneLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinZoneLevel;

                if (!byMinZoneLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinZoneLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinZoneLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byMinZoneLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByMinZoneLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinZoneLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.MaxZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxZoneLevel(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxZoneLevel(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.MaxZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxZoneLevel(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byMaxZoneLevel is null)
        {
            byMaxZoneLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxZoneLevel;

                if (!byMaxZoneLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxZoneLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxZoneLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byMaxZoneLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByMaxZoneLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxZoneLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;

                if (!bySpawnWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.SummonedMonster_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySummonedMonster_MonsterVarietiesKey(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySummonedMonster_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.SummonedMonster_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySummonedMonster_MonsterVarietiesKey(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (bySummonedMonster_MonsterVarietiesKey is null)
        {
            bySummonedMonster_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SummonedMonster_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySummonedMonster_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySummonedMonster_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySummonedMonster_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.bySummonedMonster_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyBySummonedMonster_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySummonedMonster_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown92(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;

                if (!byUnknown92.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.ModsKeys0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys0(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys0(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.ModsKeys0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys0(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byModsKeys0 is null)
        {
            byModsKeys0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys0;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys0.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys0.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byModsKeys0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByModsKeys0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.ModsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys1(int? key, out TormentSpiritsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys1(key, out var items))
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
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.ModsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys1(int? key, out IReadOnlyList<TormentSpiritsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        if (byModsKeys1 is null)
        {
            byModsKeys1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys1;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TormentSpiritsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TormentSpiritsDat"/> with <see cref="TormentSpiritsDat.byModsKeys1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TormentSpiritsDat>> GetManyToManyByModsKeys1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TormentSpiritsDat>>();
        }

        var items = new List<ResultItem<int, TormentSpiritsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TormentSpiritsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private TormentSpiritsDat[] Load()
    {
        const string filePath = "Data/TormentSpirits.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TormentSpiritsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Spirit_ModsKeys
            (var tempspirit_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spirit_modskeysLoading = tempspirit_modskeysLoading.AsReadOnly();

            // loading Touched_ModsKeys
            (var temptouched_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var touched_modskeysLoading = temptouched_modskeysLoading.AsReadOnly();

            // loading Possessed_ModsKeys
            (var temppossessed_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var possessed_modskeysLoading = temppossessed_modskeysLoading.AsReadOnly();

            // loading MinZoneLevel
            (var minzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxZoneLevel
            (var maxzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SummonedMonster_MonsterVarietiesKey
            (var summonedmonster_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKeys0
            (var tempmodskeys0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeys0Loading = tempmodskeys0Loading.AsReadOnly();

            // loading ModsKeys1
            (var tempmodskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeys1Loading = tempmodskeys1Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TormentSpiritsDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Spirit_ModsKeys = spirit_modskeysLoading,
                Touched_ModsKeys = touched_modskeysLoading,
                Possessed_ModsKeys = possessed_modskeysLoading,
                MinZoneLevel = minzonelevelLoading,
                MaxZoneLevel = maxzonelevelLoading,
                SpawnWeight = spawnweightLoading,
                SummonedMonster_MonsterVarietiesKey = summonedmonster_monstervarietieskeyLoading,
                Unknown92 = unknown92Loading,
                ModsKeys0 = modskeys0Loading,
                ModsKeys1 = modskeys1Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
