using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CurrencyItemsDat"/> related data and helper methods.
/// </summary>
public sealed class CurrencyItemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CurrencyItemsDat> Items { get; }

    private Dictionary<int, List<CurrencyItemsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<CurrencyItemsDat>>? byStacks;
    private Dictionary<int, List<CurrencyItemsDat>>? byCurrencyUseType;
    private Dictionary<string, List<CurrencyItemsDat>>? byAction;
    private Dictionary<string, List<CurrencyItemsDat>>? byDirections;
    private Dictionary<int, List<CurrencyItemsDat>>? byFullStack_BaseItemTypesKey;
    private Dictionary<string, List<CurrencyItemsDat>>? byDescription;
    private Dictionary<int, List<CurrencyItemsDat>>? byUsage_AchievementItemsKeys;
    private Dictionary<bool, List<CurrencyItemsDat>>? byUnknown80;
    private Dictionary<string, List<CurrencyItemsDat>>? byCosmeticTypeName;
    private Dictionary<int, List<CurrencyItemsDat>>? byPossession_AchievementItemsKey;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown105;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown121;
    private Dictionary<int, List<CurrencyItemsDat>>? byCurrencyTab_StackSize;
    private Dictionary<string, List<CurrencyItemsDat>>? byXBoxDirections;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown149;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown153;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown169;
    private Dictionary<int, List<CurrencyItemsDat>>? byModifyMapsAchievements;
    private Dictionary<int, List<CurrencyItemsDat>>? byModifyContractsAchievements;
    private Dictionary<int, List<CurrencyItemsDat>>? byCombineAchievements;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown233;
    private Dictionary<int, List<CurrencyItemsDat>>? byUnknown237;
    private Dictionary<int, List<CurrencyItemsDat>>? byShopTag;
    private Dictionary<bool, List<CurrencyItemsDat>>? byIsHardmode;
    private Dictionary<string, List<CurrencyItemsDat>>? byDescriptionHardmode;
    private Dictionary<bool, List<CurrencyItemsDat>>? byIsGold;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyItemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CurrencyItemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out CurrencyItemsDat? item)
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
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
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Stacks"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStacks(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStacks(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Stacks"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStacks(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byStacks is null)
        {
            byStacks = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stacks;

                if (!byStacks.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStacks.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStacks.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byStacks"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByStacks(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStacks(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CurrencyUseType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencyUseType(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencyUseType(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CurrencyUseType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencyUseType(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byCurrencyUseType is null)
        {
            byCurrencyUseType = new();
            foreach (var item in Items)
            {
                var itemKey = item.CurrencyUseType;

                if (!byCurrencyUseType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCurrencyUseType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrencyUseType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byCurrencyUseType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByCurrencyUseType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencyUseType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Action"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAction(string? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAction(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Action"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAction(string? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byAction is null)
        {
            byAction = new();
            foreach (var item in Items)
            {
                var itemKey = item.Action;

                if (!byAction.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAction.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAction.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byAction"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CurrencyItemsDat>> GetManyToManyByAction(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<string, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAction(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Directions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDirections(string? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDirections(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Directions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDirections(string? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byDirections is null)
        {
            byDirections = new();
            foreach (var item in Items)
            {
                var itemKey = item.Directions;

                if (!byDirections.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDirections.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDirections.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byDirections"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CurrencyItemsDat>> GetManyToManyByDirections(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<string, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDirections(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.FullStack_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFullStack_BaseItemTypesKey(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFullStack_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.FullStack_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFullStack_BaseItemTypesKey(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byFullStack_BaseItemTypesKey is null)
        {
            byFullStack_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FullStack_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFullStack_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFullStack_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFullStack_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byFullStack_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByFullStack_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFullStack_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out CurrencyItemsDat? item)
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
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
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CurrencyItemsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<string, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Usage_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUsage_AchievementItemsKeys(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUsage_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Usage_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUsage_AchievementItemsKeys(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUsage_AchievementItemsKeys is null)
        {
            byUsage_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Usage_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byUsage_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUsage_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUsage_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUsage_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUsage_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUsage_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(bool? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(bool? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;

                if (!byUnknown80.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CurrencyItemsDat>> GetManyToManyByUnknown80(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<bool, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CosmeticTypeName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCosmeticTypeName(string? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCosmeticTypeName(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CosmeticTypeName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCosmeticTypeName(string? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byCosmeticTypeName is null)
        {
            byCosmeticTypeName = new();
            foreach (var item in Items)
            {
                var itemKey = item.CosmeticTypeName;

                if (!byCosmeticTypeName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCosmeticTypeName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCosmeticTypeName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byCosmeticTypeName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CurrencyItemsDat>> GetManyToManyByCosmeticTypeName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<string, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCosmeticTypeName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Possession_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPossession_AchievementItemsKey(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPossession_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Possession_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPossession_AchievementItemsKey(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byPossession_AchievementItemsKey is null)
        {
            byPossession_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Possession_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPossession_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPossession_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPossession_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byPossession_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByPossession_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPossession_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown105(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown105.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown105.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown105.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown105(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown121"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown121(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown121(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown121"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown121(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown121 is null)
        {
            byUnknown121 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown121;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown121.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown121.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown121.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown121"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown121(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown121(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CurrencyTab_StackSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrencyTab_StackSize(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrencyTab_StackSize(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CurrencyTab_StackSize"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrencyTab_StackSize(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byCurrencyTab_StackSize is null)
        {
            byCurrencyTab_StackSize = new();
            foreach (var item in Items)
            {
                var itemKey = item.CurrencyTab_StackSize;

                if (!byCurrencyTab_StackSize.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCurrencyTab_StackSize.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrencyTab_StackSize.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byCurrencyTab_StackSize"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByCurrencyTab_StackSize(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrencyTab_StackSize(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.XBoxDirections"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByXBoxDirections(string? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByXBoxDirections(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.XBoxDirections"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByXBoxDirections(string? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byXBoxDirections is null)
        {
            byXBoxDirections = new();
            foreach (var item in Items)
            {
                var itemKey = item.XBoxDirections;

                if (!byXBoxDirections.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byXBoxDirections.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byXBoxDirections.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byXBoxDirections"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CurrencyItemsDat>> GetManyToManyByXBoxDirections(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<string, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByXBoxDirections(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown149(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown149(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown149(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown149 is null)
        {
            byUnknown149 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown149;

                if (!byUnknown149.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown149.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown149.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown149"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown149(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown149(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown153"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown153(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown153(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown153"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown153(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown153 is null)
        {
            byUnknown153 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown153;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown153.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown153.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown153.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown153"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown153(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown153(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown169(int? key, out CurrencyItemsDat? item)
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown169(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
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
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown169"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown169(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown169(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.ModifyMapsAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModifyMapsAchievements(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModifyMapsAchievements(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.ModifyMapsAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModifyMapsAchievements(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byModifyMapsAchievements is null)
        {
            byModifyMapsAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModifyMapsAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byModifyMapsAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModifyMapsAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModifyMapsAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byModifyMapsAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByModifyMapsAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModifyMapsAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.ModifyContractsAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModifyContractsAchievements(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModifyContractsAchievements(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.ModifyContractsAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModifyContractsAchievements(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byModifyContractsAchievements is null)
        {
            byModifyContractsAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModifyContractsAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byModifyContractsAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModifyContractsAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModifyContractsAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byModifyContractsAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByModifyContractsAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModifyContractsAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CombineAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCombineAchievements(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCombineAchievements(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.CombineAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCombineAchievements(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byCombineAchievements is null)
        {
            byCombineAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.CombineAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byCombineAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCombineAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCombineAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byCombineAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByCombineAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCombineAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown233"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown233(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown233(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown233"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown233(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown233 is null)
        {
            byUnknown233 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown233;

                if (!byUnknown233.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown233.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown233.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown233"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown233(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown233(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown237"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown237(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown237(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.Unknown237"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown237(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byUnknown237 is null)
        {
            byUnknown237 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown237;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown237.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown237.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown237.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byUnknown237"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByUnknown237(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown237(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.ShopTag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShopTag(int? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShopTag(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.ShopTag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShopTag(int? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byShopTag is null)
        {
            byShopTag = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShopTag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShopTag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShopTag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShopTag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byShopTag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CurrencyItemsDat>> GetManyToManyByShopTag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<int, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShopTag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.IsHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsHardmode(bool? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsHardmode(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.IsHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsHardmode(bool? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byIsHardmode is null)
        {
            byIsHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsHardmode;

                if (!byIsHardmode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsHardmode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsHardmode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byIsHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CurrencyItemsDat>> GetManyToManyByIsHardmode(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<bool, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.DescriptionHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescriptionHardmode(string? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescriptionHardmode(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.DescriptionHardmode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescriptionHardmode(string? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byDescriptionHardmode is null)
        {
            byDescriptionHardmode = new();
            foreach (var item in Items)
            {
                var itemKey = item.DescriptionHardmode;

                if (!byDescriptionHardmode.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescriptionHardmode.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescriptionHardmode.TryGetValue(key, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byDescriptionHardmode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CurrencyItemsDat>> GetManyToManyByDescriptionHardmode(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<string, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescriptionHardmode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.IsGold"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsGold(bool? key, out CurrencyItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsGold(key, out var items))
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
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.IsGold"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsGold(bool? key, out IReadOnlyList<CurrencyItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        if (byIsGold is null)
        {
            byIsGold = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsGold;

                if (!byIsGold.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsGold.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsGold.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CurrencyItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CurrencyItemsDat"/> with <see cref="CurrencyItemsDat.byIsGold"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CurrencyItemsDat>> GetManyToManyByIsGold(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CurrencyItemsDat>>();
        }

        var items = new List<ResultItem<bool, CurrencyItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsGold(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CurrencyItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CurrencyItemsDat[] Load()
    {
        const string filePath = "Data/CurrencyItems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CurrencyItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stacks
            (var stacksLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CurrencyUseType
            (var currencyusetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Action
            (var actionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Directions
            (var directionsLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FullStack_BaseItemTypesKey
            (var fullstack_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Usage_AchievementItemsKeys
            (var tempusage_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var usage_achievementitemskeysLoading = tempusage_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CosmeticTypeName
            (var cosmetictypenameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Possession_AchievementItemsKey
            (var possession_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown105
            (var tempunknown105Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown105Loading = tempunknown105Loading.AsReadOnly();

            // loading Unknown121
            (var tempunknown121Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown121Loading = tempunknown121Loading.AsReadOnly();

            // loading CurrencyTab_StackSize
            (var currencytab_stacksizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading XBoxDirections
            (var xboxdirectionsLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown153
            (var unknown153Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModifyMapsAchievements
            (var tempmodifymapsachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modifymapsachievementsLoading = tempmodifymapsachievementsLoading.AsReadOnly();

            // loading ModifyContractsAchievements
            (var tempmodifycontractsachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modifycontractsachievementsLoading = tempmodifycontractsachievementsLoading.AsReadOnly();

            // loading CombineAchievements
            (var tempcombineachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var combineachievementsLoading = tempcombineachievementsLoading.AsReadOnly();

            // loading Unknown233
            (var unknown233Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown237
            (var tempunknown237Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown237Loading = tempunknown237Loading.AsReadOnly();

            // loading ShopTag
            (var shoptagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsHardmode
            (var ishardmodeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DescriptionHardmode
            (var descriptionhardmodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsGold
            (var isgoldLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CurrencyItemsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Stacks = stacksLoading,
                CurrencyUseType = currencyusetypeLoading,
                Action = actionLoading,
                Directions = directionsLoading,
                FullStack_BaseItemTypesKey = fullstack_baseitemtypeskeyLoading,
                Description = descriptionLoading,
                Usage_AchievementItemsKeys = usage_achievementitemskeysLoading,
                Unknown80 = unknown80Loading,
                CosmeticTypeName = cosmetictypenameLoading,
                Possession_AchievementItemsKey = possession_achievementitemskeyLoading,
                Unknown105 = unknown105Loading,
                Unknown121 = unknown121Loading,
                CurrencyTab_StackSize = currencytab_stacksizeLoading,
                XBoxDirections = xboxdirectionsLoading,
                Unknown149 = unknown149Loading,
                Unknown153 = unknown153Loading,
                Unknown169 = unknown169Loading,
                ModifyMapsAchievements = modifymapsachievementsLoading,
                ModifyContractsAchievements = modifycontractsachievementsLoading,
                CombineAchievements = combineachievementsLoading,
                Unknown233 = unknown233Loading,
                Unknown237 = unknown237Loading,
                ShopTag = shoptagLoading,
                IsHardmode = ishardmodeLoading,
                DescriptionHardmode = descriptionhardmodeLoading,
                IsGold = isgoldLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
