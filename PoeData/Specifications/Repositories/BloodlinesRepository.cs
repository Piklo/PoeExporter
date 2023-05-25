using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BloodlinesDat"/> related data and helper methods.
/// </summary>
public sealed class BloodlinesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BloodlinesDat> Items { get; }

    private Dictionary<string, List<BloodlinesDat>>? byId;
    private Dictionary<int, List<BloodlinesDat>>? byModsKeys;
    private Dictionary<int, List<BloodlinesDat>>? byMinZoneLevel;
    private Dictionary<int, List<BloodlinesDat>>? byMaxZoneLevel;
    private Dictionary<int, List<BloodlinesDat>>? bySpawnWeight_TagsKeys;
    private Dictionary<int, List<BloodlinesDat>>? bySpawnWeight_Values;
    private Dictionary<int, List<BloodlinesDat>>? byUnknown64;
    private Dictionary<int, List<BloodlinesDat>>? byBuffDefinitionsKey;
    private Dictionary<int, List<BloodlinesDat>>? byUnknown84;
    private Dictionary<int, List<BloodlinesDat>>? byItemWeight_TagsKeys;
    private Dictionary<int, List<BloodlinesDat>>? byItemWeight_Values;
    private Dictionary<int, List<BloodlinesDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<BloodlinesDat>>? byUnknown148;
    private Dictionary<bool, List<BloodlinesDat>>? byUnknown152;
    private Dictionary<int, List<BloodlinesDat>>? byAchievementItemsKey;
    private Dictionary<bool, List<BloodlinesDat>>? byUnknown169;

    /// <summary>
    /// Initializes a new instance of the <see cref="BloodlinesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BloodlinesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BloodlinesDat? item)
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
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
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BloodlinesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BloodlinesDat>>();
        }

        var items = new List<ResultItem<string, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byModsKeys is null)
        {
            byModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.MinZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinZoneLevel(int? key, out BloodlinesDat? item)
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.MinZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinZoneLevel(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
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
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byMinZoneLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByMinZoneLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinZoneLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.MaxZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxZoneLevel(int? key, out BloodlinesDat? item)
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.MaxZoneLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxZoneLevel(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
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
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byMaxZoneLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByMaxZoneLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxZoneLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_TagsKeys(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_TagsKeys(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (bySpawnWeight_TagsKeys is null)
        {
            bySpawnWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.bySpawnWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyBySpawnWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_Values(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight_Values(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_Values(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (bySpawnWeight_Values is null)
        {
            bySpawnWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.bySpawnWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyBySpawnWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDefinitionsKey(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byBuffDefinitionsKey is null)
        {
            byBuffDefinitionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDefinitionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffDefinitionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffDefinitionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDefinitionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byBuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByBuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown84(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown84.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown84.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.ItemWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemWeight_TagsKeys(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.ItemWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemWeight_TagsKeys(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byItemWeight_TagsKeys is null)
        {
            byItemWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemWeight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byItemWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byItemWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byItemWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byItemWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByItemWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.ItemWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemWeight_Values(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemWeight_Values(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.ItemWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemWeight_Values(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byItemWeight_Values is null)
        {
            byItemWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemWeight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byItemWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byItemWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byItemWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byItemWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByItemWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out BloodlinesDat? item)
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
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
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown148(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;

                if (!byUnknown148.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByUnknown148(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown152(bool? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown152(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown152(bool? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byUnknown152 is null)
        {
            byUnknown152 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown152;

                if (!byUnknown152.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown152.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown152.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byUnknown152"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BloodlinesDat>> GetManyToManyByUnknown152(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BloodlinesDat>>();
        }

        var items = new List<ResultItem<bool, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown152(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BloodlinesDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BloodlinesDat>>();
        }

        var items = new List<ResultItem<int, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown169(bool? key, out BloodlinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown169(key, out var items))
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
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown169(bool? key, out IReadOnlyList<BloodlinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        if (byUnknown169 is null)
        {
            byUnknown169 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown169;

                if (!byUnknown169.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown169.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown169.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BloodlinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BloodlinesDat"/> with <see cref="BloodlinesDat.byUnknown169"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BloodlinesDat>> GetManyToManyByUnknown169(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BloodlinesDat>>();
        }

        var items = new List<ResultItem<bool, BloodlinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown169(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BloodlinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BloodlinesDat[] Load()
    {
        const string filePath = "Data/Bloodlines.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BloodlinesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading MinZoneLevel
            (var minzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxZoneLevel
            (var maxzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading ItemWeight_TagsKeys
            (var tempitemweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemweight_tagskeysLoading = tempitemweight_tagskeysLoading.AsReadOnly();

            // loading ItemWeight_Values
            (var tempitemweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var itemweight_valuesLoading = tempitemweight_valuesLoading.AsReadOnly();

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BloodlinesDat()
            {
                Id = idLoading,
                ModsKeys = modskeysLoading,
                MinZoneLevel = minzonelevelLoading,
                MaxZoneLevel = maxzonelevelLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                Unknown64 = unknown64Loading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown84 = unknown84Loading,
                ItemWeight_TagsKeys = itemweight_tagskeysLoading,
                ItemWeight_Values = itemweight_valuesLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Unknown148 = unknown148Loading,
                Unknown152 = unknown152Loading,
                AchievementItemsKey = achievementitemskeyLoading,
                Unknown169 = unknown169Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
