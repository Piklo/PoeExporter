using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemVisualHeldBodyModelDat"/> related data and helper methods.
/// </summary>
public sealed class ItemVisualHeldBodyModelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemVisualHeldBodyModelDat> Items { get; }

    private Dictionary<int, List<ItemVisualHeldBodyModelDat>>? byItemVisualIdentity;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byMarauderAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byRangerAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byWitchAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byDuelistAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byTemplarAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byShadowAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byScionAnimatedObject;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byMarauderBone;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byRangerBone;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byWitchBone;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byDuelistBone;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byTemplarBone;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byShadowBone;
    private Dictionary<string, List<ItemVisualHeldBodyModelDat>>? byScionBone;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemVisualHeldBodyModelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemVisualHeldBodyModelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ItemVisualIdentity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentity(int? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentity(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ItemVisualIdentity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentity(int? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byItemVisualIdentity is null)
        {
            byItemVisualIdentity = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentity;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentity.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentity.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byItemVisualIdentity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualHeldBodyModelDat>> GetManyToManyByItemVisualIdentity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.MarauderAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMarauderAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMarauderAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.MarauderAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMarauderAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byMarauderAnimatedObject is null)
        {
            byMarauderAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.MarauderAnimatedObject;

                if (!byMarauderAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMarauderAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMarauderAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byMarauderAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByMarauderAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMarauderAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.RangerAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRangerAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRangerAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.RangerAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRangerAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byRangerAnimatedObject is null)
        {
            byRangerAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.RangerAnimatedObject;

                if (!byRangerAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRangerAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRangerAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byRangerAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByRangerAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRangerAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.WitchAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWitchAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWitchAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.WitchAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWitchAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byWitchAnimatedObject is null)
        {
            byWitchAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.WitchAnimatedObject;

                if (!byWitchAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWitchAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWitchAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byWitchAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByWitchAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWitchAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.DuelistAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDuelistAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDuelistAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.DuelistAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDuelistAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byDuelistAnimatedObject is null)
        {
            byDuelistAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.DuelistAnimatedObject;

                if (!byDuelistAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDuelistAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDuelistAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byDuelistAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByDuelistAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDuelistAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.TemplarAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTemplarAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTemplarAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.TemplarAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTemplarAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byTemplarAnimatedObject is null)
        {
            byTemplarAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.TemplarAnimatedObject;

                if (!byTemplarAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTemplarAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTemplarAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byTemplarAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByTemplarAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTemplarAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ShadowAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShadowAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShadowAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ShadowAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShadowAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byShadowAnimatedObject is null)
        {
            byShadowAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShadowAnimatedObject;

                if (!byShadowAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShadowAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShadowAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byShadowAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByShadowAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShadowAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ScionAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScionAnimatedObject(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScionAnimatedObject(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ScionAnimatedObject"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScionAnimatedObject(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byScionAnimatedObject is null)
        {
            byScionAnimatedObject = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScionAnimatedObject;

                if (!byScionAnimatedObject.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScionAnimatedObject.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScionAnimatedObject.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byScionAnimatedObject"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByScionAnimatedObject(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScionAnimatedObject(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.MarauderBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMarauderBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMarauderBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.MarauderBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMarauderBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byMarauderBone is null)
        {
            byMarauderBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.MarauderBone;

                if (!byMarauderBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMarauderBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMarauderBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byMarauderBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByMarauderBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMarauderBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.RangerBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRangerBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRangerBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.RangerBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRangerBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byRangerBone is null)
        {
            byRangerBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.RangerBone;

                if (!byRangerBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRangerBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRangerBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byRangerBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByRangerBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRangerBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.WitchBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWitchBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWitchBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.WitchBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWitchBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byWitchBone is null)
        {
            byWitchBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.WitchBone;

                if (!byWitchBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWitchBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWitchBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byWitchBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByWitchBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWitchBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.DuelistBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDuelistBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDuelistBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.DuelistBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDuelistBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byDuelistBone is null)
        {
            byDuelistBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.DuelistBone;

                if (!byDuelistBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDuelistBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDuelistBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byDuelistBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByDuelistBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDuelistBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.TemplarBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTemplarBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTemplarBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.TemplarBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTemplarBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byTemplarBone is null)
        {
            byTemplarBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.TemplarBone;

                if (!byTemplarBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTemplarBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTemplarBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byTemplarBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByTemplarBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTemplarBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ShadowBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShadowBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShadowBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ShadowBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShadowBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byShadowBone is null)
        {
            byShadowBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShadowBone;

                if (!byShadowBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShadowBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShadowBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byShadowBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByShadowBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShadowBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ScionBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScionBone(string? key, out ItemVisualHeldBodyModelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScionBone(key, out var items))
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
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.ScionBone"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScionBone(string? key, out IReadOnlyList<ItemVisualHeldBodyModelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        if (byScionBone is null)
        {
            byScionBone = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScionBone;

                if (!byScionBone.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScionBone.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScionBone.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualHeldBodyModelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualHeldBodyModelDat"/> with <see cref="ItemVisualHeldBodyModelDat.byScionBone"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualHeldBodyModelDat>> GetManyToManyByScionBone(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualHeldBodyModelDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualHeldBodyModelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScionBone(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualHeldBodyModelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemVisualHeldBodyModelDat[] Load()
    {
        const string filePath = "Data/ItemVisualHeldBodyModel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemVisualHeldBodyModelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ItemVisualIdentity
            (var itemvisualidentityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MarauderAnimatedObject
            (var marauderanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RangerAnimatedObject
            (var rangeranimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitchAnimatedObject
            (var witchanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DuelistAnimatedObject
            (var duelistanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TemplarAnimatedObject
            (var templaranimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShadowAnimatedObject
            (var shadowanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ScionAnimatedObject
            (var scionanimatedobjectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MarauderBone
            (var marauderboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RangerBone
            (var rangerboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitchBone
            (var witchboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DuelistBone
            (var duelistboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TemplarBone
            (var templarboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShadowBone
            (var shadowboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ScionBone
            (var scionboneLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemVisualHeldBodyModelDat()
            {
                ItemVisualIdentity = itemvisualidentityLoading,
                MarauderAnimatedObject = marauderanimatedobjectLoading,
                RangerAnimatedObject = rangeranimatedobjectLoading,
                WitchAnimatedObject = witchanimatedobjectLoading,
                DuelistAnimatedObject = duelistanimatedobjectLoading,
                TemplarAnimatedObject = templaranimatedobjectLoading,
                ShadowAnimatedObject = shadowanimatedobjectLoading,
                ScionAnimatedObject = scionanimatedobjectLoading,
                MarauderBone = marauderboneLoading,
                RangerBone = rangerboneLoading,
                WitchBone = witchboneLoading,
                DuelistBone = duelistboneLoading,
                TemplarBone = templarboneLoading,
                ShadowBone = shadowboneLoading,
                ScionBone = scionboneLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
