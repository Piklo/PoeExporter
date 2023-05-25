using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BaseItemTypesDat"/> related data and helper methods.
/// </summary>
public sealed class BaseItemTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BaseItemTypesDat> Items { get; }

    private Dictionary<string, List<BaseItemTypesDat>>? byId;
    private Dictionary<int, List<BaseItemTypesDat>>? byItemClassesKey;
    private Dictionary<int, List<BaseItemTypesDat>>? byWidth;
    private Dictionary<int, List<BaseItemTypesDat>>? byHeight;
    private Dictionary<string, List<BaseItemTypesDat>>? byName;
    private Dictionary<string, List<BaseItemTypesDat>>? byInheritsFrom;
    private Dictionary<int, List<BaseItemTypesDat>>? byDropLevel;
    private Dictionary<int, List<BaseItemTypesDat>>? byFlavourTextKey;
    private Dictionary<int, List<BaseItemTypesDat>>? byImplicit_ModsKeys;
    private Dictionary<int, List<BaseItemTypesDat>>? bySizeOnGround;
    private Dictionary<int, List<BaseItemTypesDat>>? bySoundEffect;
    private Dictionary<int, List<BaseItemTypesDat>>? byTagsKeys;
    private Dictionary<int, List<BaseItemTypesDat>>? byModDomain;
    private Dictionary<int, List<BaseItemTypesDat>>? bySiteVisibility;
    private Dictionary<int, List<BaseItemTypesDat>>? byItemVisualIdentity;
    private Dictionary<int, List<BaseItemTypesDat>>? byHASH32;
    private Dictionary<int, List<BaseItemTypesDat>>? byVendorRecipe_AchievementItems;
    private Dictionary<string, List<BaseItemTypesDat>>? byInflection;
    private Dictionary<int, List<BaseItemTypesDat>>? byEquip_AchievementItemsKey;
    private Dictionary<bool, List<BaseItemTypesDat>>? byIsCorrupted;
    private Dictionary<int, List<BaseItemTypesDat>>? byIdentify_AchievementItems;
    private Dictionary<int, List<BaseItemTypesDat>>? byIdentifyMagic_AchievementItems;
    private Dictionary<int, List<BaseItemTypesDat>>? byFragmentBaseItemTypesKey;
    private Dictionary<bool, List<BaseItemTypesDat>>? byUnknown229;
    private Dictionary<int, List<BaseItemTypesDat>>? byUnknown230;
    private Dictionary<int, List<BaseItemTypesDat>>? byUnknown246;
    private Dictionary<bool, List<BaseItemTypesDat>>? byUnknown262;
    private Dictionary<int, List<BaseItemTypesDat>>? byTradeMarketCategory;
    private Dictionary<bool, List<BaseItemTypesDat>>? byUnknown279;
    private Dictionary<int, List<BaseItemTypesDat>>? byAchievement;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseItemTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BaseItemTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BaseItemTypesDat? item)
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
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
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BaseItemTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<string, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClassesKey(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClassesKey(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClassesKey(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byItemClassesKey is null)
        {
            byItemClassesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClassesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClassesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClassesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClassesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byItemClassesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByItemClassesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClassesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWidth(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWidth(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWidth(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byWidth is null)
        {
            byWidth = new();
            foreach (var item in Items)
            {
                var itemKey = item.Width;

                if (!byWidth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWidth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWidth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byWidth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByWidth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWidth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeight(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeight(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeight(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byHeight is null)
        {
            byHeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Height;

                if (!byHeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byHeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByHeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out BaseItemTypesDat? item)
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
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
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BaseItemTypesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<string, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInheritsFrom(string? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInheritsFrom(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.InheritsFrom"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInheritsFrom(string? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byInheritsFrom is null)
        {
            byInheritsFrom = new();
            foreach (var item in Items)
            {
                var itemKey = item.InheritsFrom;

                if (!byInheritsFrom.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInheritsFrom.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInheritsFrom.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byInheritsFrom"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BaseItemTypesDat>> GetManyToManyByInheritsFrom(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<string, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInheritsFrom(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.DropLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDropLevel(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDropLevel(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.DropLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDropLevel(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byDropLevel is null)
        {
            byDropLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.DropLevel;

                if (!byDropLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDropLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDropLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byDropLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByDropLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDropLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourTextKey(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourTextKey(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourTextKey(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byFlavourTextKey is null)
        {
            byFlavourTextKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourTextKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFlavourTextKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFlavourTextKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourTextKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byFlavourTextKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByFlavourTextKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourTextKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Implicit_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImplicit_ModsKeys(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImplicit_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Implicit_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImplicit_ModsKeys(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byImplicit_ModsKeys is null)
        {
            byImplicit_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Implicit_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byImplicit_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byImplicit_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byImplicit_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byImplicit_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByImplicit_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImplicit_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.SizeOnGround"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySizeOnGround(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySizeOnGround(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.SizeOnGround"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySizeOnGround(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (bySizeOnGround is null)
        {
            bySizeOnGround = new();
            foreach (var item in Items)
            {
                var itemKey = item.SizeOnGround;

                if (!bySizeOnGround.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySizeOnGround.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySizeOnGround.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.bySizeOnGround"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyBySizeOnGround(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySizeOnGround(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoundEffect(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoundEffect(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.SoundEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoundEffect(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (bySoundEffect is null)
        {
            bySoundEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoundEffect;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySoundEffect.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySoundEffect.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySoundEffect.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.bySoundEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyBySoundEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoundEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKeys(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKeys(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKeys(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byTagsKeys is null)
        {
            byTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.ModDomain"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModDomain(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModDomain(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.ModDomain"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModDomain(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byModDomain is null)
        {
            byModDomain = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModDomain;

                if (!byModDomain.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byModDomain.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byModDomain.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byModDomain"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByModDomain(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModDomain(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.SiteVisibility"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySiteVisibility(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySiteVisibility(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.SiteVisibility"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySiteVisibility(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (bySiteVisibility is null)
        {
            bySiteVisibility = new();
            foreach (var item in Items)
            {
                var itemKey = item.SiteVisibility;

                if (!bySiteVisibility.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySiteVisibility.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySiteVisibility.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.bySiteVisibility"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyBySiteVisibility(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySiteVisibility(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.ItemVisualIdentity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentity(int? key, out BaseItemTypesDat? item)
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.ItemVisualIdentity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentity(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
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
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byItemVisualIdentity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByItemVisualIdentity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH32(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH32(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH32(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byHASH32 is null)
        {
            byHASH32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH32;

                if (!byHASH32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byHASH32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByHASH32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.VendorRecipe_AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVendorRecipe_AchievementItems(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVendorRecipe_AchievementItems(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.VendorRecipe_AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVendorRecipe_AchievementItems(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byVendorRecipe_AchievementItems is null)
        {
            byVendorRecipe_AchievementItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.VendorRecipe_AchievementItems;
                foreach (var listKey in itemKey)
                {
                    if (!byVendorRecipe_AchievementItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byVendorRecipe_AchievementItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byVendorRecipe_AchievementItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byVendorRecipe_AchievementItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByVendorRecipe_AchievementItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVendorRecipe_AchievementItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Inflection"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInflection(string? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInflection(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Inflection"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInflection(string? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byInflection is null)
        {
            byInflection = new();
            foreach (var item in Items)
            {
                var itemKey = item.Inflection;

                if (!byInflection.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInflection.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInflection.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byInflection"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BaseItemTypesDat>> GetManyToManyByInflection(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<string, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInflection(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Equip_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEquip_AchievementItemsKey(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEquip_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Equip_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEquip_AchievementItemsKey(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byEquip_AchievementItemsKey is null)
        {
            byEquip_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Equip_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEquip_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEquip_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEquip_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byEquip_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByEquip_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEquip_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.IsCorrupted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsCorrupted(bool? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsCorrupted(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.IsCorrupted"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsCorrupted(bool? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byIsCorrupted is null)
        {
            byIsCorrupted = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsCorrupted;

                if (!byIsCorrupted.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsCorrupted.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsCorrupted.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byIsCorrupted"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BaseItemTypesDat>> GetManyToManyByIsCorrupted(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<bool, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsCorrupted(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Identify_AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIdentify_AchievementItems(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIdentify_AchievementItems(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Identify_AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIdentify_AchievementItems(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byIdentify_AchievementItems is null)
        {
            byIdentify_AchievementItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.Identify_AchievementItems;
                foreach (var listKey in itemKey)
                {
                    if (!byIdentify_AchievementItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byIdentify_AchievementItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byIdentify_AchievementItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byIdentify_AchievementItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByIdentify_AchievementItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIdentify_AchievementItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.IdentifyMagic_AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIdentifyMagic_AchievementItems(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIdentifyMagic_AchievementItems(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.IdentifyMagic_AchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIdentifyMagic_AchievementItems(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byIdentifyMagic_AchievementItems is null)
        {
            byIdentifyMagic_AchievementItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.IdentifyMagic_AchievementItems;
                foreach (var listKey in itemKey)
                {
                    if (!byIdentifyMagic_AchievementItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byIdentifyMagic_AchievementItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byIdentifyMagic_AchievementItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byIdentifyMagic_AchievementItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByIdentifyMagic_AchievementItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIdentifyMagic_AchievementItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.FragmentBaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFragmentBaseItemTypesKey(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFragmentBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.FragmentBaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFragmentBaseItemTypesKey(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byFragmentBaseItemTypesKey is null)
        {
            byFragmentBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FragmentBaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFragmentBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFragmentBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFragmentBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byFragmentBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByFragmentBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFragmentBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown229"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown229(bool? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown229(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown229"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown229(bool? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byUnknown229 is null)
        {
            byUnknown229 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown229;

                if (!byUnknown229.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown229.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown229.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byUnknown229"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BaseItemTypesDat>> GetManyToManyByUnknown229(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<bool, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown229(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown230(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown230(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown230"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown230(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byUnknown230 is null)
        {
            byUnknown230 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown230;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown230.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown230.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown230.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byUnknown230"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByUnknown230(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown230(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown246"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown246(int? key, out BaseItemTypesDat? item)
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown246"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown246(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byUnknown246 is null)
        {
            byUnknown246 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown246;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown246.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown246.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown246.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byUnknown246"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByUnknown246(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown246(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown262"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown262(bool? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown262(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown262"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown262(bool? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byUnknown262 is null)
        {
            byUnknown262 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown262;

                if (!byUnknown262.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown262.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown262.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byUnknown262"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BaseItemTypesDat>> GetManyToManyByUnknown262(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<bool, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown262(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.TradeMarketCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTradeMarketCategory(int? key, out BaseItemTypesDat? item)
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.TradeMarketCategory"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTradeMarketCategory(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
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
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byTradeMarketCategory"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByTradeMarketCategory(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTradeMarketCategory(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown279"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown279(bool? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown279(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Unknown279"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown279(bool? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byUnknown279 is null)
        {
            byUnknown279 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown279;

                if (!byUnknown279.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown279.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown279.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byUnknown279"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BaseItemTypesDat>> GetManyToManyByUnknown279(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<bool, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown279(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Achievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievement(int? key, out BaseItemTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievement(key, out var items))
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
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.Achievement"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievement(int? key, out IReadOnlyList<BaseItemTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        if (byAchievement is null)
        {
            byAchievement = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievement;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievement.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievement.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievement.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BaseItemTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BaseItemTypesDat"/> with <see cref="BaseItemTypesDat.byAchievement"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BaseItemTypesDat>> GetManyToManyByAchievement(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BaseItemTypesDat>>();
        }

        var items = new List<ResultItem<int, BaseItemTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievement(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BaseItemTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BaseItemTypesDat[] Load()
    {
        const string filePath = "Data/BaseItemTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BaseItemTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemClassesKey
            (var itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DropLevel
            (var droplevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Implicit_ModsKeys
            (var tempimplicit_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var implicit_modskeysLoading = tempimplicit_modskeysLoading.AsReadOnly();

            // loading SizeOnGround
            (var sizeongroundLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading ModDomain
            (var moddomainLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SiteVisibility
            (var sitevisibilityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemVisualIdentity
            (var itemvisualidentityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VendorRecipe_AchievementItems
            (var tempvendorrecipe_achievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var vendorrecipe_achievementitemsLoading = tempvendorrecipe_achievementitemsLoading.AsReadOnly();

            // loading Inflection
            (var inflectionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Equip_AchievementItemsKey
            (var equip_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsCorrupted
            (var iscorruptedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Identify_AchievementItems
            (var tempidentify_achievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identify_achievementitemsLoading = tempidentify_achievementitemsLoading.AsReadOnly();

            // loading IdentifyMagic_AchievementItems
            (var tempidentifymagic_achievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identifymagic_achievementitemsLoading = tempidentifymagic_achievementitemsLoading.AsReadOnly();

            // loading FragmentBaseItemTypesKey
            (var fragmentbaseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown229
            (var unknown229Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown246
            (var unknown246Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown262
            (var unknown262Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TradeMarketCategory
            (var trademarketcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown279
            (var unknown279Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievement
            (var tempachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementLoading = tempachievementLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BaseItemTypesDat()
            {
                Id = idLoading,
                ItemClassesKey = itemclasseskeyLoading,
                Width = widthLoading,
                Height = heightLoading,
                Name = nameLoading,
                InheritsFrom = inheritsfromLoading,
                DropLevel = droplevelLoading,
                FlavourTextKey = flavourtextkeyLoading,
                Implicit_ModsKeys = implicit_modskeysLoading,
                SizeOnGround = sizeongroundLoading,
                SoundEffect = soundeffectLoading,
                TagsKeys = tagskeysLoading,
                ModDomain = moddomainLoading,
                SiteVisibility = sitevisibilityLoading,
                ItemVisualIdentity = itemvisualidentityLoading,
                HASH32 = hash32Loading,
                VendorRecipe_AchievementItems = vendorrecipe_achievementitemsLoading,
                Inflection = inflectionLoading,
                Equip_AchievementItemsKey = equip_achievementitemskeyLoading,
                IsCorrupted = iscorruptedLoading,
                Identify_AchievementItems = identify_achievementitemsLoading,
                IdentifyMagic_AchievementItems = identifymagic_achievementitemsLoading,
                FragmentBaseItemTypesKey = fragmentbaseitemtypeskeyLoading,
                Unknown229 = unknown229Loading,
                Unknown230 = unknown230Loading,
                Unknown246 = unknown246Loading,
                Unknown262 = unknown262Loading,
                TradeMarketCategory = trademarketcategoryLoading,
                Unknown279 = unknown279Loading,
                Achievement = achievementLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
