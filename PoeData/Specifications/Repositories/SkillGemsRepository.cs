using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SkillGemsDat"/> related data and helper methods.
/// </summary>
public sealed class SkillGemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SkillGemsDat> Items { get; }

    private Dictionary<int, List<SkillGemsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<SkillGemsDat>>? byGrantedEffectsKey;
    private Dictionary<int, List<SkillGemsDat>>? byStr;
    private Dictionary<int, List<SkillGemsDat>>? byDex;
    private Dictionary<int, List<SkillGemsDat>>? byInt;
    private Dictionary<int, List<SkillGemsDat>>? byGemTagsKeys;
    private Dictionary<int, List<SkillGemsDat>>? byVaalVariant_BaseItemTypesKey;
    private Dictionary<bool, List<SkillGemsDat>>? byIsVaalVariant;
    private Dictionary<string, List<SkillGemsDat>>? byDescription;
    private Dictionary<int, List<SkillGemsDat>>? byConsumed_ModsKey;
    private Dictionary<int, List<SkillGemsDat>>? byGrantedEffectsKey2;
    private Dictionary<int, List<SkillGemsDat>>? byMinionGlobalSkillLevelStat;
    private Dictionary<string, List<SkillGemsDat>>? bySupportSkillName;
    private Dictionary<bool, List<SkillGemsDat>>? byIsSupport;
    private Dictionary<bool, List<SkillGemsDat>>? byUnknown142;
    private Dictionary<bool, List<SkillGemsDat>>? byUnknown143;
    private Dictionary<bool, List<SkillGemsDat>>? byUnknown144;
    private Dictionary<bool, List<SkillGemsDat>>? byUnknown145;
    private Dictionary<bool, List<SkillGemsDat>>? byUnknown146;
    private Dictionary<int, List<SkillGemsDat>>? byAwakenedVariant;
    private Dictionary<int, List<SkillGemsDat>>? byRegularVariant;
    private Dictionary<int, List<SkillGemsDat>>? byGrantedEffectHardMode;
    private Dictionary<int, List<SkillGemsDat>>? byUnknown179;
    private Dictionary<int, List<SkillGemsDat>>? byUnknown195;
    private Dictionary<int, List<SkillGemsDat>>? byItemExperienceType;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkillGemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SkillGemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out SkillGemsDat? item)
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
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
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsKey(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsKey(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsKey(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byGrantedEffectsKey is null)
        {
            byGrantedEffectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byGrantedEffectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByGrantedEffectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Str"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStr(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStr(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Str"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStr(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byStr is null)
        {
            byStr = new();
            foreach (var item in Items)
            {
                var itemKey = item.Str;

                if (!byStr.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStr.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStr.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byStr"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByStr(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStr(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Dex"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDex(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDex(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Dex"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDex(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byDex is null)
        {
            byDex = new();
            foreach (var item in Items)
            {
                var itemKey = item.Dex;

                if (!byDex.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDex.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDex.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byDex"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByDex(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDex(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Int"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInt(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInt(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Int"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInt(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byInt is null)
        {
            byInt = new();
            foreach (var item in Items)
            {
                var itemKey = item.Int;

                if (!byInt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInt.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byInt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByInt(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GemTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGemTagsKeys(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGemTagsKeys(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GemTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGemTagsKeys(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byGemTagsKeys is null)
        {
            byGemTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.GemTagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byGemTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGemTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGemTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byGemTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByGemTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGemTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.VaalVariant_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVaalVariant_BaseItemTypesKey(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVaalVariant_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.VaalVariant_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVaalVariant_BaseItemTypesKey(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byVaalVariant_BaseItemTypesKey is null)
        {
            byVaalVariant_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.VaalVariant_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byVaalVariant_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byVaalVariant_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byVaalVariant_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byVaalVariant_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByVaalVariant_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVaalVariant_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.IsVaalVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsVaalVariant(bool? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsVaalVariant(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.IsVaalVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsVaalVariant(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byIsVaalVariant is null)
        {
            byIsVaalVariant = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsVaalVariant;

                if (!byIsVaalVariant.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsVaalVariant.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsVaalVariant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byIsVaalVariant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByIsVaalVariant(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsVaalVariant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out SkillGemsDat? item)
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
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
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillGemsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillGemsDat>>();
        }

        var items = new List<ResultItem<string, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Consumed_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConsumed_ModsKey(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConsumed_ModsKey(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Consumed_ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConsumed_ModsKey(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byConsumed_ModsKey is null)
        {
            byConsumed_ModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Consumed_ModsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byConsumed_ModsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byConsumed_ModsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byConsumed_ModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byConsumed_ModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByConsumed_ModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConsumed_ModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GrantedEffectsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsKey2(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsKey2(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GrantedEffectsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsKey2(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byGrantedEffectsKey2 is null)
        {
            byGrantedEffectsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byGrantedEffectsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByGrantedEffectsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.MinionGlobalSkillLevelStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinionGlobalSkillLevelStat(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinionGlobalSkillLevelStat(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.MinionGlobalSkillLevelStat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinionGlobalSkillLevelStat(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byMinionGlobalSkillLevelStat is null)
        {
            byMinionGlobalSkillLevelStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinionGlobalSkillLevelStat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMinionGlobalSkillLevelStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMinionGlobalSkillLevelStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMinionGlobalSkillLevelStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byMinionGlobalSkillLevelStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByMinionGlobalSkillLevelStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinionGlobalSkillLevelStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.SupportSkillName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySupportSkillName(string? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySupportSkillName(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.SupportSkillName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySupportSkillName(string? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (bySupportSkillName is null)
        {
            bySupportSkillName = new();
            foreach (var item in Items)
            {
                var itemKey = item.SupportSkillName;

                if (!bySupportSkillName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySupportSkillName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySupportSkillName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.bySupportSkillName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SkillGemsDat>> GetManyToManyBySupportSkillName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SkillGemsDat>>();
        }

        var items = new List<ResultItem<string, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySupportSkillName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.IsSupport"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsSupport(bool? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsSupport(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.IsSupport"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsSupport(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byIsSupport is null)
        {
            byIsSupport = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsSupport;

                if (!byIsSupport.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsSupport.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsSupport.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byIsSupport"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByIsSupport(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsSupport(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(bool? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown142(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byUnknown142 is null)
        {
            byUnknown142 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown142;

                if (!byUnknown142.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown142.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown142.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByUnknown142(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown143(bool? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown143(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown143(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byUnknown143 is null)
        {
            byUnknown143 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown143;

                if (!byUnknown143.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown143.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown143.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown143"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByUnknown143(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown143(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(bool? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown144(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;

                if (!byUnknown144.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByUnknown144(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown145(bool? key, out SkillGemsDat? item)
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown145(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
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
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown145"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByUnknown145(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown145(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown146(bool? key, out SkillGemsDat? item)
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown146(bool? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byUnknown146 is null)
        {
            byUnknown146 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown146;

                if (!byUnknown146.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown146.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown146.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown146"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SkillGemsDat>> GetManyToManyByUnknown146(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SkillGemsDat>>();
        }

        var items = new List<ResultItem<bool, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown146(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.AwakenedVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAwakenedVariant(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAwakenedVariant(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.AwakenedVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAwakenedVariant(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byAwakenedVariant is null)
        {
            byAwakenedVariant = new();
            foreach (var item in Items)
            {
                var itemKey = item.AwakenedVariant;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAwakenedVariant.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAwakenedVariant.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAwakenedVariant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byAwakenedVariant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByAwakenedVariant(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAwakenedVariant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.RegularVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRegularVariant(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRegularVariant(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.RegularVariant"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRegularVariant(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byRegularVariant is null)
        {
            byRegularVariant = new();
            foreach (var item in Items)
            {
                var itemKey = item.RegularVariant;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRegularVariant.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRegularVariant.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRegularVariant.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byRegularVariant"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByRegularVariant(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRegularVariant(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GrantedEffectHardMode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectHardMode(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectHardMode(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.GrantedEffectHardMode"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectHardMode(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byGrantedEffectHardMode is null)
        {
            byGrantedEffectHardMode = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectHardMode;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectHardMode.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectHardMode.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectHardMode.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byGrantedEffectHardMode"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByGrantedEffectHardMode(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectHardMode(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown179"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown179(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown179(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown179"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown179(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byUnknown179 is null)
        {
            byUnknown179 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown179;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown179.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown179.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown179.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown179"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByUnknown179(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown179(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown195"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown195(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown195(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.Unknown195"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown195(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byUnknown195 is null)
        {
            byUnknown195 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown195;

                if (!byUnknown195.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown195.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown195.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byUnknown195"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByUnknown195(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown195(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.ItemExperienceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemExperienceType(int? key, out SkillGemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemExperienceType(key, out var items))
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
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.ItemExperienceType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemExperienceType(int? key, out IReadOnlyList<SkillGemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        if (byItemExperienceType is null)
        {
            byItemExperienceType = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemExperienceType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemExperienceType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemExperienceType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemExperienceType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SkillGemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SkillGemsDat"/> with <see cref="SkillGemsDat.byItemExperienceType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SkillGemsDat>> GetManyToManyByItemExperienceType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SkillGemsDat>>();
        }

        var items = new List<ResultItem<int, SkillGemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemExperienceType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SkillGemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SkillGemsDat[] Load()
    {
        const string filePath = "Data/SkillGems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillGemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Str
            (var strLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Dex
            (var dexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Int
            (var intLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading GemTagsKeys
            (var tempgemtagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var gemtagskeysLoading = tempgemtagskeysLoading.AsReadOnly();

            // loading VaalVariant_BaseItemTypesKey
            (var vaalvariant_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsVaalVariant
            (var isvaalvariantLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Consumed_ModsKey
            (var consumed_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffectsKey2
            (var grantedeffectskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinionGlobalSkillLevelStat
            (var minionglobalskilllevelstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SupportSkillName
            (var supportskillnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsSupport
            (var issupportLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AwakenedVariant
            (var awakenedvariantLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading RegularVariant
            (var regularvariantLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffectHardMode
            (var grantedeffecthardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown179
            (var unknown179Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown195
            (var unknown195Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemExperienceType
            (var itemexperiencetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillGemsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                GrantedEffectsKey = grantedeffectskeyLoading,
                Str = strLoading,
                Dex = dexLoading,
                Int = intLoading,
                GemTagsKeys = gemtagskeysLoading,
                VaalVariant_BaseItemTypesKey = vaalvariant_baseitemtypeskeyLoading,
                IsVaalVariant = isvaalvariantLoading,
                Description = descriptionLoading,
                Consumed_ModsKey = consumed_modskeyLoading,
                GrantedEffectsKey2 = grantedeffectskey2Loading,
                MinionGlobalSkillLevelStat = minionglobalskilllevelstatLoading,
                SupportSkillName = supportskillnameLoading,
                IsSupport = issupportLoading,
                Unknown142 = unknown142Loading,
                Unknown143 = unknown143Loading,
                Unknown144 = unknown144Loading,
                Unknown145 = unknown145Loading,
                Unknown146 = unknown146Loading,
                AwakenedVariant = awakenedvariantLoading,
                RegularVariant = regularvariantLoading,
                GrantedEffectHardMode = grantedeffecthardmodeLoading,
                Unknown179 = unknown179Loading,
                Unknown195 = unknown195Loading,
                ItemExperienceType = itemexperiencetypeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
