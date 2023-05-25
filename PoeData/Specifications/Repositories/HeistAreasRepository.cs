using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistAreasDat"/> related data and helper methods.
/// </summary>
public sealed class HeistAreasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistAreasDat> Items { get; }

    private Dictionary<string, List<HeistAreasDat>>? byId;
    private Dictionary<int, List<HeistAreasDat>>? byWorldAreasKeys;
    private Dictionary<int, List<HeistAreasDat>>? byUnknown24;
    private Dictionary<int, List<HeistAreasDat>>? byEnvironmentsKey1;
    private Dictionary<int, List<HeistAreasDat>>? byEnvironmentsKey2;
    private Dictionary<int, List<HeistAreasDat>>? byHeistJobsKeys;
    private Dictionary<int, List<HeistAreasDat>>? byContract_BaseItemTypesKey;
    private Dictionary<int, List<HeistAreasDat>>? byBlueprint_BaseItemTypesKey;
    private Dictionary<string, List<HeistAreasDat>>? byDGRFile;
    private Dictionary<int, List<HeistAreasDat>>? byUnknown116;
    private Dictionary<int, List<HeistAreasDat>>? byUnknown120;
    private Dictionary<bool, List<HeistAreasDat>>? byUnknown124;
    private Dictionary<bool, List<HeistAreasDat>>? byUnknown125;
    private Dictionary<string, List<HeistAreasDat>>? byBlueprint_DDSFile;
    private Dictionary<int, List<HeistAreasDat>>? byAchievements1;
    private Dictionary<int, List<HeistAreasDat>>? byAchievements2;
    private Dictionary<int, List<HeistAreasDat>>? byReward;
    private Dictionary<int, List<HeistAreasDat>>? byAchievements3;
    private Dictionary<int, List<HeistAreasDat>>? byRewardHardmode;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistAreasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistAreasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HeistAreasDat? item)
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
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
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistAreasDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistAreasDat>>();
        }

        var items = new List<ResultItem<string, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKeys(int? key, out HeistAreasDat? item)
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKeys(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
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
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byWorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByWorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out HeistAreasDat? item)
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
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
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.EnvironmentsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnvironmentsKey1(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnvironmentsKey1(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.EnvironmentsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnvironmentsKey1(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byEnvironmentsKey1 is null)
        {
            byEnvironmentsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnvironmentsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnvironmentsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnvironmentsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnvironmentsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byEnvironmentsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByEnvironmentsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnvironmentsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.EnvironmentsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnvironmentsKey2(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnvironmentsKey2(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.EnvironmentsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnvironmentsKey2(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byEnvironmentsKey2 is null)
        {
            byEnvironmentsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnvironmentsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnvironmentsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnvironmentsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnvironmentsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byEnvironmentsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByEnvironmentsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnvironmentsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.HeistJobsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistJobsKeys(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistJobsKeys(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.HeistJobsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistJobsKeys(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byHeistJobsKeys is null)
        {
            byHeistJobsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistJobsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byHeistJobsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byHeistJobsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byHeistJobsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byHeistJobsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByHeistJobsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistJobsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Contract_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByContract_BaseItemTypesKey(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByContract_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Contract_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByContract_BaseItemTypesKey(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byContract_BaseItemTypesKey is null)
        {
            byContract_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Contract_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byContract_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byContract_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byContract_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byContract_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByContract_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByContract_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Blueprint_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlueprint_BaseItemTypesKey(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlueprint_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Blueprint_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlueprint_BaseItemTypesKey(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byBlueprint_BaseItemTypesKey is null)
        {
            byBlueprint_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Blueprint_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBlueprint_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBlueprint_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBlueprint_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byBlueprint_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByBlueprint_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlueprint_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.DGRFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDGRFile(string? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDGRFile(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.DGRFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDGRFile(string? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byDGRFile is null)
        {
            byDGRFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DGRFile;

                if (!byDGRFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDGRFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDGRFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byDGRFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistAreasDat>> GetManyToManyByDGRFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistAreasDat>>();
        }

        var items = new List<ResultItem<string, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDGRFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;

                if (!byUnknown116.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown116.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByUnknown116(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown120(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;

                if (!byUnknown120.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown120.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(bool? key, out HeistAreasDat? item)
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(bool? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
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
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistAreasDat>> GetManyToManyByUnknown124(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistAreasDat>>();
        }

        var items = new List<ResultItem<bool, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown125(bool? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown125(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown125(bool? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byUnknown125 is null)
        {
            byUnknown125 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown125;

                if (!byUnknown125.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown125.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown125.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byUnknown125"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistAreasDat>> GetManyToManyByUnknown125(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistAreasDat>>();
        }

        var items = new List<ResultItem<bool, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown125(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Blueprint_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlueprint_DDSFile(string? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlueprint_DDSFile(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Blueprint_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlueprint_DDSFile(string? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byBlueprint_DDSFile is null)
        {
            byBlueprint_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Blueprint_DDSFile;

                if (!byBlueprint_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBlueprint_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBlueprint_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byBlueprint_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistAreasDat>> GetManyToManyByBlueprint_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistAreasDat>>();
        }

        var items = new List<ResultItem<string, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlueprint_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Achievements1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements1(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements1(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Achievements1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements1(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byAchievements1 is null)
        {
            byAchievements1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements1;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byAchievements1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByAchievements1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Achievements2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements2(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements2(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Achievements2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements2(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byAchievements2 is null)
        {
            byAchievements2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements2;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byAchievements2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByAchievements2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Reward"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReward(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReward(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Reward"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReward(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byReward is null)
        {
            byReward = new();
            foreach (var item in Items)
            {
                var itemKey = item.Reward;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byReward.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byReward.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byReward.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byReward"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByReward(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReward(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Achievements3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements3(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements3(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.Achievements3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements3(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byAchievements3 is null)
        {
            byAchievements3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements3;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements3.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements3.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byAchievements3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByAchievements3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.RewardHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardHardmode(int? key, out HeistAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardHardmode(key, out var items))
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
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.RewardHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardHardmode(int? key, out IReadOnlyList<HeistAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        if (byRewardHardmode is null)
        {
            byRewardHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardHardmode;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRewardHardmode.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRewardHardmode.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistAreasDat"/> with <see cref="HeistAreasDat.byRewardHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistAreasDat>> GetManyToManyByRewardHardmode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistAreasDat>>();
        }

        var items = new List<ResultItem<int, HeistAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistAreasDat[] Load()
    {
        const string filePath = "Data/HeistAreas.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistAreasDat[tableRows];
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

            // loading EnvironmentsKey1
            (var environmentskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading EnvironmentsKey2
            (var environmentskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistJobsKeys
            (var tempheistjobskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistjobskeysLoading = tempheistjobskeysLoading.AsReadOnly();

            // loading Contract_BaseItemTypesKey
            (var contract_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Blueprint_BaseItemTypesKey
            (var blueprint_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DGRFile
            (var dgrfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Blueprint_DDSFile
            (var blueprint_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Achievements1
            (var tempachievements1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements1Loading = tempachievements1Loading.AsReadOnly();

            // loading Achievements2
            (var tempachievements2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements2Loading = tempachievements2Loading.AsReadOnly();

            // loading Reward
            (var rewardLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Achievements3
            (var tempachievements3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements3Loading = tempachievements3Loading.AsReadOnly();

            // loading RewardHardmode
            (var rewardhardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistAreasDat()
            {
                Id = idLoading,
                WorldAreasKeys = worldareaskeysLoading,
                Unknown24 = unknown24Loading,
                EnvironmentsKey1 = environmentskey1Loading,
                EnvironmentsKey2 = environmentskey2Loading,
                HeistJobsKeys = heistjobskeysLoading,
                Contract_BaseItemTypesKey = contract_baseitemtypeskeyLoading,
                Blueprint_BaseItemTypesKey = blueprint_baseitemtypeskeyLoading,
                DGRFile = dgrfileLoading,
                Unknown116 = unknown116Loading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                Unknown125 = unknown125Loading,
                Blueprint_DDSFile = blueprint_ddsfileLoading,
                Achievements1 = achievements1Loading,
                Achievements2 = achievements2Loading,
                Reward = rewardLoading,
                Achievements3 = achievements3Loading,
                RewardHardmode = rewardhardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
