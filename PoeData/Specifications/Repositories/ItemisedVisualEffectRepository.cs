using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemisedVisualEffectDat"/> related data and helper methods.
/// </summary>
public sealed class ItemisedVisualEffectRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemisedVisualEffectDat> Items { get; }

    private Dictionary<int, List<ItemisedVisualEffectDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byItemVisualEffectKey;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byItemVisualIdentityKey1;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byItemVisualIdentityKey2;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byStats;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byItemClasses;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byUnknown96;
    private Dictionary<bool, List<ItemisedVisualEffectDat>>? byUnknown112;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byUnknown113;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byUnknown129;
    private Dictionary<bool, List<ItemisedVisualEffectDat>>? byUnknown145;
    private Dictionary<int, List<ItemisedVisualEffectDat>>? byUnknown146;
    private Dictionary<bool, List<ItemisedVisualEffectDat>>? byUnknown162;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemisedVisualEffectRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemisedVisualEffectRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemVisualEffectKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualEffectKey(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualEffectKey(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemVisualEffectKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualEffectKey(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byItemVisualEffectKey is null)
        {
            byItemVisualEffectKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualEffectKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualEffectKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualEffectKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualEffectKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byItemVisualEffectKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByItemVisualEffectKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualEffectKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemVisualIdentityKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey1(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey1(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemVisualIdentityKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey1(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byItemVisualIdentityKey1 is null)
        {
            byItemVisualIdentityKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byItemVisualIdentityKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByItemVisualIdentityKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemVisualIdentityKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey2(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey2(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemVisualIdentityKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey2(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byItemVisualIdentityKey2 is null)
        {
            byItemVisualIdentityKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byItemVisualIdentityKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByItemVisualIdentityKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStats(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStats(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Stats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStats(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byStats is null)
        {
            byStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stats;
                foreach (var listKey in itemKey)
                {
                    if (!byStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemClasses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClasses(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClasses(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.ItemClasses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClasses(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byItemClasses is null)
        {
            byItemClasses = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClasses;
                foreach (var listKey in itemKey)
                {
                    if (!byItemClasses.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byItemClasses.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byItemClasses.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byItemClasses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByItemClasses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClasses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown96.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown96.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(bool? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown112(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(bool? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;

                if (!byUnknown112.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemisedVisualEffectDat>> GetManyToManyByUnknown112(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<bool, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown113(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown113(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown113(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown113 is null)
        {
            byUnknown113 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown113;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown113.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown113.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown113.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown113"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByUnknown113(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown113(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown129(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown129(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown129(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown129 is null)
        {
            byUnknown129 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown129;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown129.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown129.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown129.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown129"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByUnknown129(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown129(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown145(bool? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown145(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown145(bool? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown145 is null)
        {
            byUnknown145 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown145;

                if (!byUnknown145.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown145.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown145.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown145"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemisedVisualEffectDat>> GetManyToManyByUnknown145(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<bool, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown145(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown146(int? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown146(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown146(int? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown146 is null)
        {
            byUnknown146 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown146;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown146.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown146.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown146.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown146"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemisedVisualEffectDat>> GetManyToManyByUnknown146(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown146(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown162"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown162(bool? key, out ItemisedVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown162(key, out var items))
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
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.Unknown162"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown162(bool? key, out IReadOnlyList<ItemisedVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        if (byUnknown162 is null)
        {
            byUnknown162 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown162;

                if (!byUnknown162.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown162.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown162.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemisedVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemisedVisualEffectDat"/> with <see cref="ItemisedVisualEffectDat.byUnknown162"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemisedVisualEffectDat>> GetManyToManyByUnknown162(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemisedVisualEffectDat>>();
        }

        var items = new List<ResultItem<bool, ItemisedVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown162(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemisedVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemisedVisualEffectDat[] Load()
    {
        const string filePath = "Data/ItemisedVisualEffect.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemisedVisualEffectDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualEffectKey
            (var itemvisualeffectkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey1
            (var itemvisualidentitykey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey2
            (var itemvisualidentitykey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading ItemClasses
            (var tempitemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemclassesLoading = tempitemclassesLoading.AsReadOnly();

            // loading Unknown96
            (var tempunknown96Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown96Loading = tempunknown96Loading.AsReadOnly();

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown113
            (var tempunknown113Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown113Loading = tempunknown113Loading.AsReadOnly();

            // loading Unknown129
            (var tempunknown129Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown129Loading = tempunknown129Loading.AsReadOnly();

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown146
            (var tempunknown146Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown146Loading = tempunknown146Loading.AsReadOnly();

            // loading Unknown162
            (var unknown162Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemisedVisualEffectDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ItemVisualEffectKey = itemvisualeffectkeyLoading,
                ItemVisualIdentityKey1 = itemvisualidentitykey1Loading,
                ItemVisualIdentityKey2 = itemvisualidentitykey2Loading,
                Stats = statsLoading,
                ItemClasses = itemclassesLoading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown113 = unknown113Loading,
                Unknown129 = unknown129Loading,
                Unknown145 = unknown145Loading,
                Unknown146 = unknown146Loading,
                Unknown162 = unknown162Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
