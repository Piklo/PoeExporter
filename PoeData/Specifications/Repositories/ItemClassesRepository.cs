using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemClassesDat"/> related data and helper methods.
/// </summary>
public sealed class ItemClassesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemClassesDat> Items { get; }

    private Dictionary<string, List<ItemClassesDat>>? byId;
    private Dictionary<string, List<ItemClassesDat>>? byName;
    private Dictionary<int, List<ItemClassesDat>>? byTradeMarketCategory;
    private Dictionary<int, List<ItemClassesDat>>? byItemClassCategory;
    private Dictionary<bool, List<ItemClassesDat>>? byRemovedIfLeavesArea;
    private Dictionary<int, List<ItemClassesDat>>? byUnknown49;
    private Dictionary<int, List<ItemClassesDat>>? byIdentifyAchievements;
    private Dictionary<bool, List<ItemClassesDat>>? byAllocateToMapOwner;
    private Dictionary<bool, List<ItemClassesDat>>? byAlwaysAllocate;
    private Dictionary<bool, List<ItemClassesDat>>? byCanHaveVeiledMods;
    private Dictionary<int, List<ItemClassesDat>>? byPickedUpQuest;
    private Dictionary<int, List<ItemClassesDat>>? byUnknown100;
    private Dictionary<bool, List<ItemClassesDat>>? byAlwaysShow;
    private Dictionary<bool, List<ItemClassesDat>>? byCanBeCorrupted;
    private Dictionary<bool, List<ItemClassesDat>>? byCanHaveIncubators;
    private Dictionary<bool, List<ItemClassesDat>>? byCanHaveInfluence;
    private Dictionary<bool, List<ItemClassesDat>>? byCanBeDoubleCorrupted;
    private Dictionary<bool, List<ItemClassesDat>>? byCanHaveAspects;
    private Dictionary<bool, List<ItemClassesDat>>? byCanTransferSkin;
    private Dictionary<int, List<ItemClassesDat>>? byItemStance;
    private Dictionary<bool, List<ItemClassesDat>>? byCanScourge;
    private Dictionary<bool, List<ItemClassesDat>>? byCanUpgradeRarity;
    private Dictionary<bool, List<ItemClassesDat>>? byUnknown129;
    private Dictionary<bool, List<ItemClassesDat>>? byUnknown130;
    private Dictionary<int, List<ItemClassesDat>>? byMaxInventoryDimensions;
    private Dictionary<int, List<ItemClassesDat>>? byFlags;
    private Dictionary<bool, List<ItemClassesDat>>? byIsUnmodifiable;
    private Dictionary<bool, List<ItemClassesDat>>? byCanBeFractured;
    private Dictionary<int, List<ItemClassesDat>>? byEquipAchievements;
    private Dictionary<bool, List<ItemClassesDat>>? byUnknown181;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemClassesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemClassesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ItemClassesDat? item)
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
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
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemClassesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemClassesDat>>();
        }

        var items = new List<ResultItem<string, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out ItemClassesDat? item)
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
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
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemClassesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemClassesDat>>();
        }

        var items = new List<ResultItem<string, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.TradeMarketCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTradeMarketCategory(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTradeMarketCategory(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.TradeMarketCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTradeMarketCategory(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byTradeMarketCategory is null)
        {
            byTradeMarketCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.TradeMarketCategory;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTradeMarketCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTradeMarketCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTradeMarketCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byTradeMarketCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByTradeMarketCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTradeMarketCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.ItemClassCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClassCategory(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClassCategory(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.ItemClassCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClassCategory(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byItemClassCategory is null)
        {
            byItemClassCategory = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClassCategory;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClassCategory.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClassCategory.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClassCategory.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byItemClassCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByItemClassCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClassCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.RemovedIfLeavesArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRemovedIfLeavesArea(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRemovedIfLeavesArea(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.RemovedIfLeavesArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRemovedIfLeavesArea(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byRemovedIfLeavesArea is null)
        {
            byRemovedIfLeavesArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.RemovedIfLeavesArea;

                if (!byRemovedIfLeavesArea.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRemovedIfLeavesArea.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRemovedIfLeavesArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byRemovedIfLeavesArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByRemovedIfLeavesArea(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRemovedIfLeavesArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown49(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byUnknown49 is null)
        {
            byUnknown49 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown49;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown49.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown49.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown49.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByUnknown49(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.IdentifyAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIdentifyAchievements(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIdentifyAchievements(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.IdentifyAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIdentifyAchievements(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byIdentifyAchievements is null)
        {
            byIdentifyAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.IdentifyAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byIdentifyAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byIdentifyAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byIdentifyAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byIdentifyAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByIdentifyAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIdentifyAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.AllocateToMapOwner"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAllocateToMapOwner(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAllocateToMapOwner(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.AllocateToMapOwner"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAllocateToMapOwner(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byAllocateToMapOwner is null)
        {
            byAllocateToMapOwner = new();
            foreach (var item in Items)
            {
                var itemKey = item.AllocateToMapOwner;

                if (!byAllocateToMapOwner.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAllocateToMapOwner.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAllocateToMapOwner.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byAllocateToMapOwner"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByAllocateToMapOwner(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAllocateToMapOwner(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.AlwaysAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlwaysAllocate(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlwaysAllocate(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.AlwaysAllocate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlwaysAllocate(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byAlwaysAllocate is null)
        {
            byAlwaysAllocate = new();
            foreach (var item in Items)
            {
                var itemKey = item.AlwaysAllocate;

                if (!byAlwaysAllocate.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAlwaysAllocate.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAlwaysAllocate.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byAlwaysAllocate"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByAlwaysAllocate(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlwaysAllocate(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveVeiledMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanHaveVeiledMods(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanHaveVeiledMods(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveVeiledMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanHaveVeiledMods(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanHaveVeiledMods is null)
        {
            byCanHaveVeiledMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanHaveVeiledMods;

                if (!byCanHaveVeiledMods.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanHaveVeiledMods.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanHaveVeiledMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanHaveVeiledMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanHaveVeiledMods(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanHaveVeiledMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.PickedUpQuest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPickedUpQuest(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPickedUpQuest(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.PickedUpQuest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPickedUpQuest(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byPickedUpQuest is null)
        {
            byPickedUpQuest = new();
            foreach (var item in Items)
            {
                var itemKey = item.PickedUpQuest;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPickedUpQuest.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPickedUpQuest.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPickedUpQuest.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byPickedUpQuest"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByPickedUpQuest(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPickedUpQuest(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.AlwaysShow"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlwaysShow(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlwaysShow(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.AlwaysShow"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlwaysShow(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byAlwaysShow is null)
        {
            byAlwaysShow = new();
            foreach (var item in Items)
            {
                var itemKey = item.AlwaysShow;

                if (!byAlwaysShow.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAlwaysShow.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAlwaysShow.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byAlwaysShow"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByAlwaysShow(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlwaysShow(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanBeCorrupted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanBeCorrupted(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanBeCorrupted(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanBeCorrupted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanBeCorrupted(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanBeCorrupted is null)
        {
            byCanBeCorrupted = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanBeCorrupted;

                if (!byCanBeCorrupted.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanBeCorrupted.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanBeCorrupted.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanBeCorrupted"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanBeCorrupted(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanBeCorrupted(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveIncubators"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanHaveIncubators(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanHaveIncubators(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveIncubators"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanHaveIncubators(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanHaveIncubators is null)
        {
            byCanHaveIncubators = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanHaveIncubators;

                if (!byCanHaveIncubators.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanHaveIncubators.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanHaveIncubators.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanHaveIncubators"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanHaveIncubators(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanHaveIncubators(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveInfluence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanHaveInfluence(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanHaveInfluence(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveInfluence"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanHaveInfluence(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanHaveInfluence is null)
        {
            byCanHaveInfluence = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanHaveInfluence;

                if (!byCanHaveInfluence.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanHaveInfluence.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanHaveInfluence.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanHaveInfluence"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanHaveInfluence(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanHaveInfluence(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanBeDoubleCorrupted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanBeDoubleCorrupted(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanBeDoubleCorrupted(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanBeDoubleCorrupted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanBeDoubleCorrupted(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanBeDoubleCorrupted is null)
        {
            byCanBeDoubleCorrupted = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanBeDoubleCorrupted;

                if (!byCanBeDoubleCorrupted.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanBeDoubleCorrupted.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanBeDoubleCorrupted.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanBeDoubleCorrupted"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanBeDoubleCorrupted(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanBeDoubleCorrupted(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveAspects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanHaveAspects(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanHaveAspects(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanHaveAspects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanHaveAspects(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanHaveAspects is null)
        {
            byCanHaveAspects = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanHaveAspects;

                if (!byCanHaveAspects.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanHaveAspects.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanHaveAspects.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanHaveAspects"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanHaveAspects(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanHaveAspects(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanTransferSkin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanTransferSkin(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanTransferSkin(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanTransferSkin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanTransferSkin(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanTransferSkin is null)
        {
            byCanTransferSkin = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanTransferSkin;

                if (!byCanTransferSkin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanTransferSkin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanTransferSkin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanTransferSkin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanTransferSkin(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanTransferSkin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.ItemStance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemStance(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemStance(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.ItemStance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemStance(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byItemStance is null)
        {
            byItemStance = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemStance;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemStance.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemStance.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemStance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byItemStance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByItemStance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemStance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanScourge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanScourge(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanScourge(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanScourge"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanScourge(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanScourge is null)
        {
            byCanScourge = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanScourge;

                if (!byCanScourge.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanScourge.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanScourge.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanScourge"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanScourge(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanScourge(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanUpgradeRarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanUpgradeRarity(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanUpgradeRarity(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanUpgradeRarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanUpgradeRarity(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanUpgradeRarity is null)
        {
            byCanUpgradeRarity = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanUpgradeRarity;

                if (!byCanUpgradeRarity.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanUpgradeRarity.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanUpgradeRarity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanUpgradeRarity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanUpgradeRarity(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanUpgradeRarity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown129(bool? key, out ItemClassesDat? item)
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown129(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byUnknown129 is null)
        {
            byUnknown129 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown129;

                if (!byUnknown129.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown129.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown129.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byUnknown129"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByUnknown129(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown129(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown130"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown130(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown130(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown130"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown130(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byUnknown130 is null)
        {
            byUnknown130 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown130;

                if (!byUnknown130.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown130.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown130.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byUnknown130"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByUnknown130(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown130(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.MaxInventoryDimensions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxInventoryDimensions(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxInventoryDimensions(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.MaxInventoryDimensions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxInventoryDimensions(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byMaxInventoryDimensions is null)
        {
            byMaxInventoryDimensions = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxInventoryDimensions;
                foreach (var listKey in itemKey)
                {
                    if (!byMaxInventoryDimensions.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMaxInventoryDimensions.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMaxInventoryDimensions.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byMaxInventoryDimensions"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByMaxInventoryDimensions(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxInventoryDimensions(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Flags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlags(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlags(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Flags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlags(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byFlags is null)
        {
            byFlags = new();
            foreach (var item in Items)
            {
                var itemKey = item.Flags;
                foreach (var listKey in itemKey)
                {
                    if (!byFlags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFlags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFlags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byFlags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByFlags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.IsUnmodifiable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsUnmodifiable(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsUnmodifiable(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.IsUnmodifiable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsUnmodifiable(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byIsUnmodifiable is null)
        {
            byIsUnmodifiable = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsUnmodifiable;

                if (!byIsUnmodifiable.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsUnmodifiable.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsUnmodifiable.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byIsUnmodifiable"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByIsUnmodifiable(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsUnmodifiable(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanBeFractured"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanBeFractured(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanBeFractured(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.CanBeFractured"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanBeFractured(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byCanBeFractured is null)
        {
            byCanBeFractured = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanBeFractured;

                if (!byCanBeFractured.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanBeFractured.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanBeFractured.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byCanBeFractured"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByCanBeFractured(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanBeFractured(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.EquipAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEquipAchievements(int? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEquipAchievements(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.EquipAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEquipAchievements(int? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byEquipAchievements is null)
        {
            byEquipAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.EquipAchievements;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEquipAchievements.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEquipAchievements.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEquipAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byEquipAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemClassesDat>> GetManyToManyByEquipAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemClassesDat>>();
        }

        var items = new List<ResultItem<int, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEquipAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown181(bool? key, out ItemClassesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown181(key, out var items))
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
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown181(bool? key, out IReadOnlyList<ItemClassesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        if (byUnknown181 is null)
        {
            byUnknown181 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown181;

                if (!byUnknown181.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown181.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown181.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemClassesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemClassesDat"/> with <see cref="ItemClassesDat.byUnknown181"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemClassesDat>> GetManyToManyByUnknown181(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemClassesDat>>();
        }

        var items = new List<ResultItem<bool, ItemClassesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown181(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemClassesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemClassesDat[] Load()
    {
        const string filePath = "Data/ItemClasses.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemClassesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TradeMarketCategory
            (var trademarketcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemClassCategory
            (var itemclasscategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RemovedIfLeavesArea
            (var removedifleavesareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var tempunknown49Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown49Loading = tempunknown49Loading.AsReadOnly();

            // loading IdentifyAchievements
            (var tempidentifyachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identifyachievementsLoading = tempidentifyachievementsLoading.AsReadOnly();

            // loading AllocateToMapOwner
            (var allocatetomapownerLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AlwaysAllocate
            (var alwaysallocateLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveVeiledMods
            (var canhaveveiledmodsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PickedUpQuest
            (var pickedupquestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AlwaysShow
            (var alwaysshowLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanBeCorrupted
            (var canbecorruptedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveIncubators
            (var canhaveincubatorsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveInfluence
            (var canhaveinfluenceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanBeDoubleCorrupted
            (var canbedoublecorruptedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveAspects
            (var canhaveaspectsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanTransferSkin
            (var cantransferskinLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ItemStance
            (var itemstanceLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading CanScourge
            (var canscourgeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanUpgradeRarity
            (var canupgraderarityLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown130
            (var unknown130Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MaxInventoryDimensions
            (var tempmaxinventorydimensionsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var maxinventorydimensionsLoading = tempmaxinventorydimensionsLoading.AsReadOnly();

            // loading Flags
            (var tempflagsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var flagsLoading = tempflagsLoading.AsReadOnly();

            // loading IsUnmodifiable
            (var isunmodifiableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanBeFractured
            (var canbefracturedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading EquipAchievements
            (var equipachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemClassesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                TradeMarketCategory = trademarketcategoryLoading,
                ItemClassCategory = itemclasscategoryLoading,
                RemovedIfLeavesArea = removedifleavesareaLoading,
                Unknown49 = unknown49Loading,
                IdentifyAchievements = identifyachievementsLoading,
                AllocateToMapOwner = allocatetomapownerLoading,
                AlwaysAllocate = alwaysallocateLoading,
                CanHaveVeiledMods = canhaveveiledmodsLoading,
                PickedUpQuest = pickedupquestLoading,
                Unknown100 = unknown100Loading,
                AlwaysShow = alwaysshowLoading,
                CanBeCorrupted = canbecorruptedLoading,
                CanHaveIncubators = canhaveincubatorsLoading,
                CanHaveInfluence = canhaveinfluenceLoading,
                CanBeDoubleCorrupted = canbedoublecorruptedLoading,
                CanHaveAspects = canhaveaspectsLoading,
                CanTransferSkin = cantransferskinLoading,
                ItemStance = itemstanceLoading,
                CanScourge = canscourgeLoading,
                CanUpgradeRarity = canupgraderarityLoading,
                Unknown129 = unknown129Loading,
                Unknown130 = unknown130Loading,
                MaxInventoryDimensions = maxinventorydimensionsLoading,
                Flags = flagsLoading,
                IsUnmodifiable = isunmodifiableLoading,
                CanBeFractured = canbefracturedLoading,
                EquipAchievements = equipachievementsLoading,
                Unknown181 = unknown181Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
