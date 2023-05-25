using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveCraftingModifiersDat"/> related data and helper methods.
/// </summary>
public sealed class DelveCraftingModifiersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveCraftingModifiersDat> Items { get; }

    private Dictionary<int, List<DelveCraftingModifiersDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byAddedModsKeys;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byNegativeWeight_TagsKeys;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byNegativeWeight_Values;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byForcedAddModsKeys;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byForbiddenDelveCraftingTagsKeys;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byAllowedDelveCraftingTagsKeys;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byCanMirrorItem;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byCorruptedEssenceChance;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byCanImproveQuality;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byCanRollEnchant;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byHasLuckyRolls;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? bySellPrice_ModsKeys;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byCanRollWhiteSockets;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byWeight_TagsKeys;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byWeight_Values;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byDelveCraftingModifierDescriptionsKeys;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byBlockedDelveCraftingModifierDescriptionsKeys;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byUnknown201;
    private Dictionary<bool, List<DelveCraftingModifiersDat>>? byUnknown202;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byUnknown203;
    private Dictionary<int, List<DelveCraftingModifiersDat>>? byUnknown219;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveCraftingModifiersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveCraftingModifiersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out DelveCraftingModifiersDat? item)
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
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
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.AddedModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAddedModsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAddedModsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.AddedModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAddedModsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byAddedModsKeys is null)
        {
            byAddedModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AddedModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAddedModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAddedModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAddedModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byAddedModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByAddedModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAddedModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.NegativeWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNegativeWeight_TagsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNegativeWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.NegativeWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNegativeWeight_TagsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byNegativeWeight_TagsKeys is null)
        {
            byNegativeWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.NegativeWeight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byNegativeWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNegativeWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNegativeWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byNegativeWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByNegativeWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNegativeWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.NegativeWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNegativeWeight_Values(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNegativeWeight_Values(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.NegativeWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNegativeWeight_Values(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byNegativeWeight_Values is null)
        {
            byNegativeWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.NegativeWeight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byNegativeWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byNegativeWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byNegativeWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byNegativeWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByNegativeWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNegativeWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.ForcedAddModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByForcedAddModsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByForcedAddModsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.ForcedAddModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByForcedAddModsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byForcedAddModsKeys is null)
        {
            byForcedAddModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ForcedAddModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byForcedAddModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byForcedAddModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byForcedAddModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byForcedAddModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByForcedAddModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByForcedAddModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.ForbiddenDelveCraftingTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByForbiddenDelveCraftingTagsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByForbiddenDelveCraftingTagsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.ForbiddenDelveCraftingTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByForbiddenDelveCraftingTagsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byForbiddenDelveCraftingTagsKeys is null)
        {
            byForbiddenDelveCraftingTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ForbiddenDelveCraftingTagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byForbiddenDelveCraftingTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byForbiddenDelveCraftingTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byForbiddenDelveCraftingTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byForbiddenDelveCraftingTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByForbiddenDelveCraftingTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByForbiddenDelveCraftingTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.AllowedDelveCraftingTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAllowedDelveCraftingTagsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAllowedDelveCraftingTagsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.AllowedDelveCraftingTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAllowedDelveCraftingTagsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byAllowedDelveCraftingTagsKeys is null)
        {
            byAllowedDelveCraftingTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AllowedDelveCraftingTagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAllowedDelveCraftingTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAllowedDelveCraftingTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAllowedDelveCraftingTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byAllowedDelveCraftingTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByAllowedDelveCraftingTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAllowedDelveCraftingTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanMirrorItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanMirrorItem(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanMirrorItem(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanMirrorItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanMirrorItem(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byCanMirrorItem is null)
        {
            byCanMirrorItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanMirrorItem;

                if (!byCanMirrorItem.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanMirrorItem.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanMirrorItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byCanMirrorItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByCanMirrorItem(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanMirrorItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CorruptedEssenceChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCorruptedEssenceChance(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCorruptedEssenceChance(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CorruptedEssenceChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCorruptedEssenceChance(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byCorruptedEssenceChance is null)
        {
            byCorruptedEssenceChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.CorruptedEssenceChance;

                if (!byCorruptedEssenceChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCorruptedEssenceChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCorruptedEssenceChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byCorruptedEssenceChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByCorruptedEssenceChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCorruptedEssenceChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanImproveQuality"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanImproveQuality(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanImproveQuality(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanImproveQuality"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanImproveQuality(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byCanImproveQuality is null)
        {
            byCanImproveQuality = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanImproveQuality;

                if (!byCanImproveQuality.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanImproveQuality.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanImproveQuality.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byCanImproveQuality"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByCanImproveQuality(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanImproveQuality(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanRollEnchant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanRollEnchant(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanRollEnchant(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanRollEnchant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanRollEnchant(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byCanRollEnchant is null)
        {
            byCanRollEnchant = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanRollEnchant;

                if (!byCanRollEnchant.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanRollEnchant.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanRollEnchant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byCanRollEnchant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByCanRollEnchant(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanRollEnchant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.HasLuckyRolls"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasLuckyRolls(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasLuckyRolls(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.HasLuckyRolls"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasLuckyRolls(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byHasLuckyRolls is null)
        {
            byHasLuckyRolls = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasLuckyRolls;

                if (!byHasLuckyRolls.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasLuckyRolls.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasLuckyRolls.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byHasLuckyRolls"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByHasLuckyRolls(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasLuckyRolls(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.SellPrice_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySellPrice_ModsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySellPrice_ModsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.SellPrice_ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySellPrice_ModsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (bySellPrice_ModsKeys is null)
        {
            bySellPrice_ModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.SellPrice_ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySellPrice_ModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySellPrice_ModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySellPrice_ModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.bySellPrice_ModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyBySellPrice_ModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySellPrice_ModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanRollWhiteSockets"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCanRollWhiteSockets(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCanRollWhiteSockets(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.CanRollWhiteSockets"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCanRollWhiteSockets(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byCanRollWhiteSockets is null)
        {
            byCanRollWhiteSockets = new();
            foreach (var item in Items)
            {
                var itemKey = item.CanRollWhiteSockets;

                if (!byCanRollWhiteSockets.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCanRollWhiteSockets.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCanRollWhiteSockets.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byCanRollWhiteSockets"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByCanRollWhiteSockets(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCanRollWhiteSockets(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Weight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeight_TagsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Weight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeight_TagsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byWeight_TagsKeys is null)
        {
            byWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Weight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWeight_Values(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWeight_Values(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Weight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWeight_Values(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byWeight_Values is null)
        {
            byWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Weight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.DelveCraftingModifierDescriptionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDelveCraftingModifierDescriptionsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDelveCraftingModifierDescriptionsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.DelveCraftingModifierDescriptionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDelveCraftingModifierDescriptionsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byDelveCraftingModifierDescriptionsKeys is null)
        {
            byDelveCraftingModifierDescriptionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.DelveCraftingModifierDescriptionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byDelveCraftingModifierDescriptionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byDelveCraftingModifierDescriptionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byDelveCraftingModifierDescriptionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byDelveCraftingModifierDescriptionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByDelveCraftingModifierDescriptionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDelveCraftingModifierDescriptionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.BlockedDelveCraftingModifierDescriptionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBlockedDelveCraftingModifierDescriptionsKeys(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBlockedDelveCraftingModifierDescriptionsKeys(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.BlockedDelveCraftingModifierDescriptionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBlockedDelveCraftingModifierDescriptionsKeys(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byBlockedDelveCraftingModifierDescriptionsKeys is null)
        {
            byBlockedDelveCraftingModifierDescriptionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BlockedDelveCraftingModifierDescriptionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBlockedDelveCraftingModifierDescriptionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBlockedDelveCraftingModifierDescriptionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBlockedDelveCraftingModifierDescriptionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byBlockedDelveCraftingModifierDescriptionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByBlockedDelveCraftingModifierDescriptionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBlockedDelveCraftingModifierDescriptionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown201"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown201(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown201(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown201"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown201(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byUnknown201 is null)
        {
            byUnknown201 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown201;

                if (!byUnknown201.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown201.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown201.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byUnknown201"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByUnknown201(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown201(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown202"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown202(bool? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown202(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown202"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown202(bool? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byUnknown202 is null)
        {
            byUnknown202 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown202;

                if (!byUnknown202.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown202.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown202.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byUnknown202"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveCraftingModifiersDat>> GetManyToManyByUnknown202(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<bool, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown202(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown203"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown203(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown203(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown203"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown203(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byUnknown203 is null)
        {
            byUnknown203 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown203;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown203.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown203.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown203.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byUnknown203"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByUnknown203(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown203(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown219"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown219(int? key, out DelveCraftingModifiersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown219(key, out var items))
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
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.Unknown219"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown219(int? key, out IReadOnlyList<DelveCraftingModifiersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        if (byUnknown219 is null)
        {
            byUnknown219 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown219;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown219.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown219.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown219.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveCraftingModifiersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveCraftingModifiersDat"/> with <see cref="DelveCraftingModifiersDat.byUnknown219"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveCraftingModifiersDat>> GetManyToManyByUnknown219(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveCraftingModifiersDat>>();
        }

        var items = new List<ResultItem<int, DelveCraftingModifiersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown219(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveCraftingModifiersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveCraftingModifiersDat[] Load()
    {
        const string filePath = "Data/DelveCraftingModifiers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveCraftingModifiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AddedModsKeys
            (var tempaddedmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var addedmodskeysLoading = tempaddedmodskeysLoading.AsReadOnly();

            // loading NegativeWeight_TagsKeys
            (var tempnegativeweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var negativeweight_tagskeysLoading = tempnegativeweight_tagskeysLoading.AsReadOnly();

            // loading NegativeWeight_Values
            (var tempnegativeweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var negativeweight_valuesLoading = tempnegativeweight_valuesLoading.AsReadOnly();

            // loading ForcedAddModsKeys
            (var tempforcedaddmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var forcedaddmodskeysLoading = tempforcedaddmodskeysLoading.AsReadOnly();

            // loading ForbiddenDelveCraftingTagsKeys
            (var tempforbiddendelvecraftingtagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var forbiddendelvecraftingtagskeysLoading = tempforbiddendelvecraftingtagskeysLoading.AsReadOnly();

            // loading AllowedDelveCraftingTagsKeys
            (var tempalloweddelvecraftingtagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var alloweddelvecraftingtagskeysLoading = tempalloweddelvecraftingtagskeysLoading.AsReadOnly();

            // loading CanMirrorItem
            (var canmirroritemLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CorruptedEssenceChance
            (var corruptedessencechanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CanImproveQuality
            (var canimprovequalityLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanRollEnchant
            (var canrollenchantLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasLuckyRolls
            (var hasluckyrollsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SellPrice_ModsKeys
            (var tempsellprice_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var sellprice_modskeysLoading = tempsellprice_modskeysLoading.AsReadOnly();

            // loading CanRollWhiteSockets
            (var canrollwhitesocketsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Weight_TagsKeys
            (var tempweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weight_tagskeysLoading = tempweight_tagskeysLoading.AsReadOnly();

            // loading Weight_Values
            (var tempweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var weight_valuesLoading = tempweight_valuesLoading.AsReadOnly();

            // loading DelveCraftingModifierDescriptionsKeys
            (var tempdelvecraftingmodifierdescriptionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var delvecraftingmodifierdescriptionskeysLoading = tempdelvecraftingmodifierdescriptionskeysLoading.AsReadOnly();

            // loading BlockedDelveCraftingModifierDescriptionsKeys
            (var tempblockeddelvecraftingmodifierdescriptionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var blockeddelvecraftingmodifierdescriptionskeysLoading = tempblockeddelvecraftingmodifierdescriptionskeysLoading.AsReadOnly();

            // loading Unknown201
            (var unknown201Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown202
            (var unknown202Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown203
            (var tempunknown203Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown203Loading = tempunknown203Loading.AsReadOnly();

            // loading Unknown219
            (var tempunknown219Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown219Loading = tempunknown219Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveCraftingModifiersDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                AddedModsKeys = addedmodskeysLoading,
                NegativeWeight_TagsKeys = negativeweight_tagskeysLoading,
                NegativeWeight_Values = negativeweight_valuesLoading,
                ForcedAddModsKeys = forcedaddmodskeysLoading,
                ForbiddenDelveCraftingTagsKeys = forbiddendelvecraftingtagskeysLoading,
                AllowedDelveCraftingTagsKeys = alloweddelvecraftingtagskeysLoading,
                CanMirrorItem = canmirroritemLoading,
                CorruptedEssenceChance = corruptedessencechanceLoading,
                CanImproveQuality = canimprovequalityLoading,
                CanRollEnchant = canrollenchantLoading,
                HasLuckyRolls = hasluckyrollsLoading,
                SellPrice_ModsKeys = sellprice_modskeysLoading,
                CanRollWhiteSockets = canrollwhitesocketsLoading,
                Weight_TagsKeys = weight_tagskeysLoading,
                Weight_Values = weight_valuesLoading,
                DelveCraftingModifierDescriptionsKeys = delvecraftingmodifierdescriptionskeysLoading,
                BlockedDelveCraftingModifierDescriptionsKeys = blockeddelvecraftingmodifierdescriptionskeysLoading,
                Unknown201 = unknown201Loading,
                Unknown202 = unknown202Loading,
                Unknown203 = unknown203Loading,
                Unknown219 = unknown219Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
