using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="StrDexIntMissionsDat"/> related data and helper methods.
/// </summary>
public sealed class StrDexIntMissionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<StrDexIntMissionsDat> Items { get; }

    private Dictionary<string, List<StrDexIntMissionsDat>>? byId;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown8;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown12;
    private Dictionary<int, List<StrDexIntMissionsDat>>? bySpawnWeight;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown20;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown36;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byExtra_ModsKeys;
    private Dictionary<bool, List<StrDexIntMissionsDat>>? byUnknown68;
    private Dictionary<bool, List<StrDexIntMissionsDat>>? byUnknown69;
    private Dictionary<bool, List<StrDexIntMissionsDat>>? byUnknown70;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown71;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown87;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown91;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown95;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown99;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown103;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown119;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown135;
    private Dictionary<bool, List<StrDexIntMissionsDat>>? byUnknown151;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown152;
    private Dictionary<bool, List<StrDexIntMissionsDat>>? byUnknown168;
    private Dictionary<int, List<StrDexIntMissionsDat>>? byUnknown169;

    /// <summary>
    /// Initializes a new instance of the <see cref="StrDexIntMissionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal StrDexIntMissionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out StrDexIntMissionsDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
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
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, StrDexIntMissionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<string, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out StrDexIntMissionsDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
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
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown20.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown36.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Extra_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByExtra_ModsKeys(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByExtra_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Extra_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByExtra_ModsKeys(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byExtra_ModsKeys is null)
        {
            byExtra_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Extra_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byExtra_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byExtra_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byExtra_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byExtra_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByExtra_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByExtra_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(bool? key, out StrDexIntMissionsDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(bool? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
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
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionsDat>> GetManyToManyByUnknown68(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown69(bool? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown69(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown69(bool? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown69 is null)
        {
            byUnknown69 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown69;

                if (!byUnknown69.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown69.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown69.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown69"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionsDat>> GetManyToManyByUnknown69(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown69(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(bool? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown70(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(bool? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown70 is null)
        {
            byUnknown70 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown70;

                if (!byUnknown70.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown70.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown70.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionsDat>> GetManyToManyByUnknown70(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown71(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown71(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown71(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown71 is null)
        {
            byUnknown71 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown71;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown71.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown71.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown71.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown71"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown71(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown71(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown87(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown87(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown87(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown87 is null)
        {
            byUnknown87 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown87;

                if (!byUnknown87.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown87.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown87.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown87"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown87(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown87(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown91(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown91(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown91(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown91 is null)
        {
            byUnknown91 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown91;

                if (!byUnknown91.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown91.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown91.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown91"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown91(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown91(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown95(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown95(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown95(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown95 is null)
        {
            byUnknown95 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown95;

                if (!byUnknown95.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown95.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown95.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown95"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown95(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown95(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown99(int? key, out StrDexIntMissionsDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown99(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown99 is null)
        {
            byUnknown99 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown99;

                if (!byUnknown99.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown99.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown99.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown99"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown99(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown99(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown103(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown103(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown103(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown103 is null)
        {
            byUnknown103 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown103;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown103.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown103.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown103.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown103"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown103(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown103(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown119(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown119(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown119(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown119 is null)
        {
            byUnknown119 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown119;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown119.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown119.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown119.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown119"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown119(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown119(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown135"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown135(int? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown135(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown135"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown135(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown135 is null)
        {
            byUnknown135 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown135;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown135.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown135.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown135.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown135"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown135(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown135(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown151"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown151(bool? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown151(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown151"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown151(bool? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown151 is null)
        {
            byUnknown151 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown151;

                if (!byUnknown151.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown151.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown151.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown151"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionsDat>> GetManyToManyByUnknown151(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown151(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown152(int? key, out StrDexIntMissionsDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown152(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown152 is null)
        {
            byUnknown152 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown152;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown152.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown152.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown152.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown152"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown152(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown152(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown168(bool? key, out StrDexIntMissionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown168(key, out var items))
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown168(bool? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown168 is null)
        {
            byUnknown168 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown168;

                if (!byUnknown168.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown168.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown168.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown168"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, StrDexIntMissionsDat>> GetManyToManyByUnknown168(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<bool, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown168(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown169(int? key, out StrDexIntMissionsDat? item)
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
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown169(int? key, out IReadOnlyList<StrDexIntMissionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        if (byUnknown169 is null)
        {
            byUnknown169 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown169;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown169.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown169.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown169.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<StrDexIntMissionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="StrDexIntMissionsDat"/> with <see cref="StrDexIntMissionsDat.byUnknown169"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, StrDexIntMissionsDat>> GetManyToManyByUnknown169(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, StrDexIntMissionsDat>>();
        }

        var items = new List<ResultItem<int, StrDexIntMissionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown169(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, StrDexIntMissionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private StrDexIntMissionsDat[] Load()
    {
        const string filePath = "Data/StrDexIntMissions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StrDexIntMissionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Extra_ModsKeys
            (var tempextra_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var extra_modskeysLoading = tempextra_modskeysLoading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown135
            (var unknown135Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown151
            (var unknown151Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StrDexIntMissionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                SpawnWeight = spawnweightLoading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Extra_ModsKeys = extra_modskeysLoading,
                Unknown68 = unknown68Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown71 = unknown71Loading,
                Unknown87 = unknown87Loading,
                Unknown91 = unknown91Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown103 = unknown103Loading,
                Unknown119 = unknown119Loading,
                Unknown135 = unknown135Loading,
                Unknown151 = unknown151Loading,
                Unknown152 = unknown152Loading,
                Unknown168 = unknown168Loading,
                Unknown169 = unknown169Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
