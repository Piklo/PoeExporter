using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NPCMasterDat"/> related data and helper methods.
/// </summary>
public sealed class NPCMasterRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NPCMasterDat> Items { get; }

    private Dictionary<string, List<NPCMasterDat>>? byId;
    private Dictionary<bool, List<NPCMasterDat>>? byUnknown8;
    private Dictionary<bool, List<NPCMasterDat>>? byUnknown9;
    private Dictionary<int, List<NPCMasterDat>>? bySignature_ModsKey;
    private Dictionary<bool, List<NPCMasterDat>>? byUnknown26;
    private Dictionary<int, List<NPCMasterDat>>? bySpawnWeight_TagsKeys;
    private Dictionary<int, List<NPCMasterDat>>? bySpawnWeight_Values;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown59;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown75;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown91;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown107;
    private Dictionary<string, List<NPCMasterDat>>? byAreaDescription;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown119;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown135;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown139;
    private Dictionary<bool, List<NPCMasterDat>>? byHasAreaMissions;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown156;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown172;
    private Dictionary<int, List<NPCMasterDat>>? byUnknown188;

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCMasterRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NPCMasterRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
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
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCMasterDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCMasterDat>>();
        }

        var items = new List<ResultItem<string, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(bool? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(bool? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
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
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCMasterDat>> GetManyToManyByUnknown8(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCMasterDat>>();
        }

        var items = new List<ResultItem<bool, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown9(bool? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown9(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown9(bool? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown9 is null)
        {
            byUnknown9 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown9;

                if (!byUnknown9.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown9.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown9.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown9"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCMasterDat>> GetManyToManyByUnknown9(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCMasterDat>>();
        }

        var items = new List<ResultItem<bool, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown9(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Signature_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySignature_ModsKey(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySignature_ModsKey(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Signature_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySignature_ModsKey(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (bySignature_ModsKey is null)
        {
            bySignature_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Signature_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySignature_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySignature_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySignature_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.bySignature_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyBySignature_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySignature_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown26(bool? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown26(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown26(bool? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown26 is null)
        {
            byUnknown26 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown26;

                if (!byUnknown26.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown26.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown26.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown26"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCMasterDat>> GetManyToManyByUnknown26(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCMasterDat>>();
        }

        var items = new List<ResultItem<bool, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown26(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_TagsKeys(int? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_TagsKeys(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
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
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.bySpawnWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyBySpawnWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_Values(int? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_Values(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
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
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.bySpawnWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyBySpawnWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown59"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown59(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown59(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown59"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown59(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown59 is null)
        {
            byUnknown59 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown59;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown59.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown59.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown59.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown59"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown59(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown59(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown75"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown75(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown75(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown75"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown75(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown75 is null)
        {
            byUnknown75 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown75;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown75.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown75.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown75.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown75"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown75(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown75(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown91(int? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown91(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown91 is null)
        {
            byUnknown91 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown91;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown91.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown91.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown91.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown91"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown91(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown91(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown107(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown107(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown107(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown107 is null)
        {
            byUnknown107 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown107;

                if (!byUnknown107.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown107.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown107.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown107"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown107(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown107(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.AreaDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaDescription(string? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaDescription(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.AreaDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaDescription(string? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byAreaDescription is null)
        {
            byAreaDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaDescription;

                if (!byAreaDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byAreaDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NPCMasterDat>> GetManyToManyByAreaDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NPCMasterDat>>();
        }

        var items = new List<ResultItem<string, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown119(int? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown119"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown119(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
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
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown119"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown119(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown119(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown135"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown135(int? key, out NPCMasterDat? item)
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown135"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown135(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown135 is null)
        {
            byUnknown135 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown135;

                if (!byUnknown135.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown135.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown135.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown135"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown135(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown135(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown139"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown139(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown139(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown139"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown139(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown139 is null)
        {
            byUnknown139 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown139;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown139.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown139.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown139.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown139"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown139(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown139(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.HasAreaMissions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasAreaMissions(bool? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasAreaMissions(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.HasAreaMissions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasAreaMissions(bool? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byHasAreaMissions is null)
        {
            byHasAreaMissions = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasAreaMissions;

                if (!byHasAreaMissions.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasAreaMissions.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasAreaMissions.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byHasAreaMissions"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NPCMasterDat>> GetManyToManyByHasAreaMissions(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NPCMasterDat>>();
        }

        var items = new List<ResultItem<bool, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasAreaMissions(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown156(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown156(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown156(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown156 is null)
        {
            byUnknown156 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown156;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown156.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown156.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown156.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown156"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown156(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown156(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown172(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown172(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown172(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown172 is null)
        {
            byUnknown172 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown172;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown172.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown172.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown172.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown172"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown172(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown172(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown188"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown188(int? key, out NPCMasterDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown188(key, out var items))
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
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.Unknown188"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown188(int? key, out IReadOnlyList<NPCMasterDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        if (byUnknown188 is null)
        {
            byUnknown188 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown188;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown188.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown188.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown188.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NPCMasterDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NPCMasterDat"/> with <see cref="NPCMasterDat.byUnknown188"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NPCMasterDat>> GetManyToManyByUnknown188(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NPCMasterDat>>();
        }

        var items = new List<ResultItem<int, NPCMasterDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown188(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NPCMasterDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NPCMasterDat[] Load()
    {
        const string filePath = "Data/NPCMaster.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCMasterDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Signature_ModsKey
            (var signature_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading Unknown59
            (var tempunknown59Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown59Loading = tempunknown59Loading.AsReadOnly();

            // loading Unknown75
            (var tempunknown75Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown75Loading = tempunknown75Loading.AsReadOnly();

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AreaDescription
            (var areadescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown135
            (var unknown135Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown139
            (var unknown139Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HasAreaMissions
            (var hasareamissionsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown156
            (var tempunknown156Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown156Loading = tempunknown156Loading.AsReadOnly();

            // loading Unknown172
            (var tempunknown172Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown172Loading = tempunknown172Loading.AsReadOnly();

            // loading Unknown188
            (var unknown188Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCMasterDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Signature_ModsKey = signature_modskeyLoading,
                Unknown26 = unknown26Loading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                Unknown59 = unknown59Loading,
                Unknown75 = unknown75Loading,
                Unknown91 = unknown91Loading,
                Unknown107 = unknown107Loading,
                AreaDescription = areadescriptionLoading,
                Unknown119 = unknown119Loading,
                Unknown135 = unknown135Loading,
                Unknown139 = unknown139Loading,
                HasAreaMissions = hasareamissionsLoading,
                Unknown156 = unknown156Loading,
                Unknown172 = unknown172Loading,
                Unknown188 = unknown188Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
