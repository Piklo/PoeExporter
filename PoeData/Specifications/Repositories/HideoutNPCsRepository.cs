using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HideoutNPCsDat"/> related data and helper methods.
/// </summary>
public sealed class HideoutNPCsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HideoutNPCsDat> Items { get; }

    private Dictionary<int, List<HideoutNPCsDat>>? byHideout_NPCsKey;
    private Dictionary<int, List<HideoutNPCsDat>>? byRegular_NPCsKeys;
    private Dictionary<int, List<HideoutNPCsDat>>? byHideoutDoodadsKey;
    private Dictionary<int, List<HideoutNPCsDat>>? byNPCMasterKey;
    private Dictionary<int, List<HideoutNPCsDat>>? byUnknown52;
    private Dictionary<int, List<HideoutNPCsDat>>? byUnknown68;
    private Dictionary<int, List<HideoutNPCsDat>>? byUnknown84;
    private Dictionary<int, List<HideoutNPCsDat>>? byUnknown88;
    private Dictionary<int, List<HideoutNPCsDat>>? byUnknown104;
    private Dictionary<bool, List<HideoutNPCsDat>>? byUnknown108;
    private Dictionary<int, List<HideoutNPCsDat>>? byUnknown109;

    /// <summary>
    /// Initializes a new instance of the <see cref="HideoutNPCsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HideoutNPCsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Hideout_NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHideout_NPCsKey(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHideout_NPCsKey(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Hideout_NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHideout_NPCsKey(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byHideout_NPCsKey is null)
        {
            byHideout_NPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Hideout_NPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHideout_NPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHideout_NPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHideout_NPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byHideout_NPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByHideout_NPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHideout_NPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Regular_NPCsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRegular_NPCsKeys(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRegular_NPCsKeys(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Regular_NPCsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRegular_NPCsKeys(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byRegular_NPCsKeys is null)
        {
            byRegular_NPCsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Regular_NPCsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byRegular_NPCsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRegular_NPCsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRegular_NPCsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byRegular_NPCsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByRegular_NPCsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRegular_NPCsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.HideoutDoodadsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHideoutDoodadsKey(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHideoutDoodadsKey(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.HideoutDoodadsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHideoutDoodadsKey(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byHideoutDoodadsKey is null)
        {
            byHideoutDoodadsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HideoutDoodadsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHideoutDoodadsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHideoutDoodadsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHideoutDoodadsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byHideoutDoodadsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByHideoutDoodadsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHideoutDoodadsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.NPCMasterKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCMasterKey(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCMasterKey(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.NPCMasterKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCMasterKey(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byNPCMasterKey is null)
        {
            byNPCMasterKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCMasterKey;

                if (!byNPCMasterKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNPCMasterKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCMasterKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byNPCMasterKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByNPCMasterKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCMasterKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown52.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out HideoutNPCsDat? item)
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown68.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out HideoutNPCsDat? item)
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;

                if (!byUnknown84.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown84.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out HideoutNPCsDat? item)
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown88.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(bool? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown108(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(bool? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;

                if (!byUnknown108.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HideoutNPCsDat>> GetManyToManyByUnknown108(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<bool, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(int? key, out HideoutNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown109(key, out var items))
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
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(int? key, out IReadOnlyList<HideoutNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown109.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown109.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HideoutNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HideoutNPCsDat"/> with <see cref="HideoutNPCsDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HideoutNPCsDat>> GetManyToManyByUnknown109(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HideoutNPCsDat>>();
        }

        var items = new List<ResultItem<int, HideoutNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HideoutNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HideoutNPCsDat[] Load()
    {
        const string filePath = "Data/HideoutNPCs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HideoutNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Hideout_NPCsKey
            (var hideout_npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Regular_NPCsKeys
            (var tempregular_npcskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var regular_npcskeysLoading = tempregular_npcskeysLoading.AsReadOnly();

            // loading HideoutDoodadsKey
            (var hideoutdoodadskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading NPCMasterKey
            (var npcmasterkeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HideoutNPCsDat()
            {
                Hideout_NPCsKey = hideout_npcskeyLoading,
                Regular_NPCsKeys = regular_npcskeysLoading,
                HideoutDoodadsKey = hideoutdoodadskeyLoading,
                NPCMasterKey = npcmasterkeyLoading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
