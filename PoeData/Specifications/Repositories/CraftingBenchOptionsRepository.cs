using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CraftingBenchOptionsDat"/> related data and helper methods.
/// </summary>
public sealed class CraftingBenchOptionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CraftingBenchOptionsDat> Items { get; }

    private Dictionary<int, List<CraftingBenchOptionsDat>>? byHideoutNPCsKey;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byOrder;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byAddMod;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byCost_BaseItemTypes;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byCost_Values;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byRequiredLevel;
    private Dictionary<string, List<CraftingBenchOptionsDat>>? byName;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byCraftingBenchCustomAction;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byItemClasses;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byLinks;
    private Dictionary<string, List<CraftingBenchOptionsDat>>? bySocketColours;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? bySockets;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byItemQuantity;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown120;
    private Dictionary<string, List<CraftingBenchOptionsDat>>? byDescription;
    private Dictionary<bool, List<CraftingBenchOptionsDat>>? byIsDisabled;
    private Dictionary<bool, List<CraftingBenchOptionsDat>>? byIsAreaOption;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byRecipeIds;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byTier;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byCraftingItemClassCategories;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown182;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnlockCategory;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnveilsRequired;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnveilsRequired2;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown210;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown226;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown242;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown246;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown250;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byAddEnchantment;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? bySortCategory;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown298;
    private Dictionary<bool, List<CraftingBenchOptionsDat>>? byUnknown314;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown315;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown319;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown335;
    private Dictionary<int, List<CraftingBenchOptionsDat>>? byUnknown351;

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchOptionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CraftingBenchOptionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.HideoutNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHideoutNPCsKey(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHideoutNPCsKey(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.HideoutNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHideoutNPCsKey(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byHideoutNPCsKey is null)
        {
            byHideoutNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HideoutNPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHideoutNPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHideoutNPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHideoutNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byHideoutNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByHideoutNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHideoutNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Order"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOrder(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOrder(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Order"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOrder(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byOrder is null)
        {
            byOrder = new();
            foreach (var item in Items)
            {
                var itemKey = item.Order;

                if (!byOrder.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOrder.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOrder.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byOrder"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByOrder(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOrder(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.AddMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAddMod(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAddMod(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.AddMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAddMod(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byAddMod is null)
        {
            byAddMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.AddMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAddMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAddMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAddMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byAddMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByAddMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAddMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Cost_BaseItemTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost_BaseItemTypes(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost_BaseItemTypes(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Cost_BaseItemTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost_BaseItemTypes(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byCost_BaseItemTypes is null)
        {
            byCost_BaseItemTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost_BaseItemTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byCost_BaseItemTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost_BaseItemTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost_BaseItemTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byCost_BaseItemTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByCost_BaseItemTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost_BaseItemTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Cost_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost_Values(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost_Values(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Cost_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost_Values(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byCost_Values is null)
        {
            byCost_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byCost_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byCost_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByCost_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.RequiredLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredLevel(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredLevel(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.RequiredLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredLevel(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byRequiredLevel is null)
        {
            byRequiredLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredLevel;

                if (!byRequiredLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRequiredLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRequiredLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byRequiredLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByRequiredLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchOptionsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.CraftingBenchCustomAction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCraftingBenchCustomAction(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCraftingBenchCustomAction(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.CraftingBenchCustomAction"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCraftingBenchCustomAction(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byCraftingBenchCustomAction is null)
        {
            byCraftingBenchCustomAction = new();
            foreach (var item in Items)
            {
                var itemKey = item.CraftingBenchCustomAction;

                if (!byCraftingBenchCustomAction.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCraftingBenchCustomAction.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCraftingBenchCustomAction.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byCraftingBenchCustomAction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByCraftingBenchCustomAction(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCraftingBenchCustomAction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.ItemClasses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClasses(int? key, out CraftingBenchOptionsDat? item)
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.ItemClasses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClasses(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
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
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byItemClasses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByItemClasses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClasses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Links"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLinks(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLinks(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Links"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLinks(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byLinks is null)
        {
            byLinks = new();
            foreach (var item in Items)
            {
                var itemKey = item.Links;

                if (!byLinks.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLinks.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLinks.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byLinks"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByLinks(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLinks(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.SocketColours"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySocketColours(string? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySocketColours(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.SocketColours"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySocketColours(string? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (bySocketColours is null)
        {
            bySocketColours = new();
            foreach (var item in Items)
            {
                var itemKey = item.SocketColours;

                if (!bySocketColours.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySocketColours.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySocketColours.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.bySocketColours"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchOptionsDat>> GetManyToManyBySocketColours(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySocketColours(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Sockets"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySockets(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySockets(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Sockets"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySockets(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (bySockets is null)
        {
            bySockets = new();
            foreach (var item in Items)
            {
                var itemKey = item.Sockets;

                if (!bySockets.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySockets.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySockets.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.bySockets"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyBySockets(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySockets(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.ItemQuantity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemQuantity(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemQuantity(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.ItemQuantity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemQuantity(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byItemQuantity is null)
        {
            byItemQuantity = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemQuantity;

                if (!byItemQuantity.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byItemQuantity.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byItemQuantity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byItemQuantity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByItemQuantity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemQuantity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out CraftingBenchOptionsDat? item)
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown120.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown120.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CraftingBenchOptionsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<string, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDisabled(bool? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsDisabled(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDisabled(bool? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byIsDisabled is null)
        {
            byIsDisabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsDisabled;

                if (!byIsDisabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsDisabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsDisabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byIsDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CraftingBenchOptionsDat>> GetManyToManyByIsDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<bool, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.IsAreaOption"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsAreaOption(bool? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsAreaOption(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.IsAreaOption"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsAreaOption(bool? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byIsAreaOption is null)
        {
            byIsAreaOption = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsAreaOption;

                if (!byIsAreaOption.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsAreaOption.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsAreaOption.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byIsAreaOption"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CraftingBenchOptionsDat>> GetManyToManyByIsAreaOption(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<bool, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsAreaOption(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.RecipeIds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecipeIds(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRecipeIds(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.RecipeIds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecipeIds(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byRecipeIds is null)
        {
            byRecipeIds = new();
            foreach (var item in Items)
            {
                var itemKey = item.RecipeIds;
                foreach (var listKey in itemKey)
                {
                    if (!byRecipeIds.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRecipeIds.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRecipeIds.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byRecipeIds"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByRecipeIds(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecipeIds(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.CraftingItemClassCategories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCraftingItemClassCategories(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCraftingItemClassCategories(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.CraftingItemClassCategories"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCraftingItemClassCategories(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byCraftingItemClassCategories is null)
        {
            byCraftingItemClassCategories = new();
            foreach (var item in Items)
            {
                var itemKey = item.CraftingItemClassCategories;
                foreach (var listKey in itemKey)
                {
                    if (!byCraftingItemClassCategories.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCraftingItemClassCategories.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCraftingItemClassCategories.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byCraftingItemClassCategories"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByCraftingItemClassCategories(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCraftingItemClassCategories(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown182"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown182(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown182(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown182"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown182(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown182 is null)
        {
            byUnknown182 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown182;

                if (!byUnknown182.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown182.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown182.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown182"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown182(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown182(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.UnlockCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnlockCategory(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnlockCategory(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.UnlockCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnlockCategory(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnlockCategory is null)
        {
            byUnlockCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnlockCategory;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnlockCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnlockCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnlockCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnlockCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnlockCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnlockCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.UnveilsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnveilsRequired(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnveilsRequired(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.UnveilsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnveilsRequired(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnveilsRequired is null)
        {
            byUnveilsRequired = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnveilsRequired;

                if (!byUnveilsRequired.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnveilsRequired.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnveilsRequired.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnveilsRequired"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnveilsRequired(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnveilsRequired(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.UnveilsRequired2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnveilsRequired2(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnveilsRequired2(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.UnveilsRequired2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnveilsRequired2(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnveilsRequired2 is null)
        {
            byUnveilsRequired2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnveilsRequired2;

                if (!byUnveilsRequired2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnveilsRequired2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnveilsRequired2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnveilsRequired2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnveilsRequired2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnveilsRequired2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown210"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown210(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown210(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown210"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown210(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown210 is null)
        {
            byUnknown210 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown210;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown210.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown210.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown210.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown210"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown210(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown210(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown226"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown226(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown226(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown226"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown226(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown226 is null)
        {
            byUnknown226 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown226;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown226.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown226.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown226.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown226"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown226(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown226(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown242"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown242(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown242(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown242"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown242(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown242 is null)
        {
            byUnknown242 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown242;

                if (!byUnknown242.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown242.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown242.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown242"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown242(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown242(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown246"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown246(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown246(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown246"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown246(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown246 is null)
        {
            byUnknown246 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown246;

                if (!byUnknown246.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown246.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown246.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown246"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown246(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown246(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown250"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown250(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown250(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown250"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown250(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown250 is null)
        {
            byUnknown250 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown250;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown250.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown250.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown250.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown250"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown250(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown250(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.AddEnchantment"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAddEnchantment(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAddEnchantment(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.AddEnchantment"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAddEnchantment(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byAddEnchantment is null)
        {
            byAddEnchantment = new();
            foreach (var item in Items)
            {
                var itemKey = item.AddEnchantment;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAddEnchantment.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAddEnchantment.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAddEnchantment.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byAddEnchantment"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByAddEnchantment(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAddEnchantment(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.SortCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySortCategory(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySortCategory(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.SortCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySortCategory(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (bySortCategory is null)
        {
            bySortCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.SortCategory;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySortCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySortCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySortCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.bySortCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyBySortCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySortCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown298"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown298(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown298(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown298"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown298(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown298 is null)
        {
            byUnknown298 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown298;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown298.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown298.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown298.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown298"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown298(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown298(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown314"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown314(bool? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown314(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown314"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown314(bool? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown314 is null)
        {
            byUnknown314 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown314;

                if (!byUnknown314.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown314.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown314.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown314"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CraftingBenchOptionsDat>> GetManyToManyByUnknown314(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<bool, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown314(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown315"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown315(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown315(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown315"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown315(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown315 is null)
        {
            byUnknown315 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown315;

                if (!byUnknown315.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown315.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown315.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown315"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown315(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown315(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown319"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown319(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown319(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown319"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown319(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown319 is null)
        {
            byUnknown319 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown319;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown319.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown319.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown319.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown319"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown319(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown319(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown335"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown335(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown335(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown335"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown335(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown335 is null)
        {
            byUnknown335 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown335;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown335.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown335.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown335.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown335"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown335(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown335(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown351"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown351(int? key, out CraftingBenchOptionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown351(key, out var items))
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
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.Unknown351"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown351(int? key, out IReadOnlyList<CraftingBenchOptionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        if (byUnknown351 is null)
        {
            byUnknown351 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown351;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown351.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown351.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown351.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CraftingBenchOptionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CraftingBenchOptionsDat"/> with <see cref="CraftingBenchOptionsDat.byUnknown351"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CraftingBenchOptionsDat>> GetManyToManyByUnknown351(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CraftingBenchOptionsDat>>();
        }

        var items = new List<ResultItem<int, CraftingBenchOptionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown351(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CraftingBenchOptionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CraftingBenchOptionsDat[] Load()
    {
        const string filePath = "Data/CraftingBenchOptions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CraftingBenchOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HideoutNPCsKey
            (var hideoutnpcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Order
            (var orderLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AddMod
            (var addmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Cost_BaseItemTypes
            (var tempcost_baseitemtypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost_baseitemtypesLoading = tempcost_baseitemtypesLoading.AsReadOnly();

            // loading Cost_Values
            (var tempcost_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost_valuesLoading = tempcost_valuesLoading.AsReadOnly();

            // loading RequiredLevel
            (var requiredlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CraftingBenchCustomAction
            (var craftingbenchcustomactionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemClasses
            (var tempitemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemclassesLoading = tempitemclassesLoading.AsReadOnly();

            // loading Links
            (var linksLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SocketColours
            (var socketcoloursLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Sockets
            (var socketsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemQuantity
            (var itemquantityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var tempunknown120Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown120Loading = tempunknown120Loading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsAreaOption
            (var isareaoptionLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading RecipeIds
            (var temprecipeidsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var recipeidsLoading = temprecipeidsLoading.AsReadOnly();

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CraftingItemClassCategories
            (var tempcraftingitemclasscategoriesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclasscategoriesLoading = tempcraftingitemclasscategoriesLoading.AsReadOnly();

            // loading Unknown182
            (var unknown182Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnlockCategory
            (var unlockcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UnveilsRequired
            (var unveilsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnveilsRequired2
            (var unveilsrequired2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown210
            (var tempunknown210Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown210Loading = tempunknown210Loading.AsReadOnly();

            // loading Unknown226
            (var tempunknown226Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown226Loading = tempunknown226Loading.AsReadOnly();

            // loading Unknown242
            (var unknown242Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown246
            (var unknown246Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown250
            (var unknown250Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AddEnchantment
            (var addenchantmentLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SortCategory
            (var sortcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown298
            (var unknown298Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown314
            (var unknown314Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown315
            (var unknown315Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown319
            (var unknown319Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown335
            (var unknown335Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown351
            (var unknown351Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CraftingBenchOptionsDat()
            {
                HideoutNPCsKey = hideoutnpcskeyLoading,
                Order = orderLoading,
                AddMod = addmodLoading,
                Cost_BaseItemTypes = cost_baseitemtypesLoading,
                Cost_Values = cost_valuesLoading,
                RequiredLevel = requiredlevelLoading,
                Name = nameLoading,
                CraftingBenchCustomAction = craftingbenchcustomactionLoading,
                ItemClasses = itemclassesLoading,
                Links = linksLoading,
                SocketColours = socketcoloursLoading,
                Sockets = socketsLoading,
                ItemQuantity = itemquantityLoading,
                Unknown120 = unknown120Loading,
                Description = descriptionLoading,
                IsDisabled = isdisabledLoading,
                IsAreaOption = isareaoptionLoading,
                RecipeIds = recipeidsLoading,
                Tier = tierLoading,
                CraftingItemClassCategories = craftingitemclasscategoriesLoading,
                Unknown182 = unknown182Loading,
                UnlockCategory = unlockcategoryLoading,
                UnveilsRequired = unveilsrequiredLoading,
                UnveilsRequired2 = unveilsrequired2Loading,
                Unknown210 = unknown210Loading,
                Unknown226 = unknown226Loading,
                Unknown242 = unknown242Loading,
                Unknown246 = unknown246Loading,
                Unknown250 = unknown250Loading,
                AddEnchantment = addenchantmentLoading,
                SortCategory = sortcategoryLoading,
                Unknown298 = unknown298Loading,
                Unknown314 = unknown314Loading,
                Unknown315 = unknown315Loading,
                Unknown319 = unknown319Loading,
                Unknown335 = unknown335Loading,
                Unknown351 = unknown351Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
