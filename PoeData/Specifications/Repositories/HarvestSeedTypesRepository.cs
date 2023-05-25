using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HarvestSeedTypesDat"/> related data and helper methods.
/// </summary>
public sealed class HarvestSeedTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HarvestSeedTypesDat> Items { get; }

    private Dictionary<int, List<HarvestSeedTypesDat>>? byHarvestObjectsKey;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byUnknown16;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byGrowthCycles;
    private Dictionary<string, List<HarvestSeedTypesDat>>? byAOFiles;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byUnknown52;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byUnknown68;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byTier;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byRequiredNearbySeed_Tier;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byRequiredNearbySeed_Amount;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byWildLifeforceConsumedPercentage;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byVividLifeforceConsumedPercentage;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byPrimalLifeforceConsumedPercentage;
    private Dictionary<string, List<HarvestSeedTypesDat>>? byText;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byHarvestCraftOptionsKeys;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byUnknown120;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byUnknown124;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byAchievementItemsKeys;
    private Dictionary<int, List<HarvestSeedTypesDat>>? byOutcomeType;

    /// <summary>
    /// Initializes a new instance of the <see cref="HarvestSeedTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HarvestSeedTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.HarvestObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHarvestObjectsKey(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHarvestObjectsKey(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.HarvestObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHarvestObjectsKey(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byHarvestObjectsKey is null)
        {
            byHarvestObjectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HarvestObjectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHarvestObjectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHarvestObjectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHarvestObjectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byHarvestObjectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByHarvestObjectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHarvestObjectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.GrowthCycles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrowthCycles(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrowthCycles(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.GrowthCycles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrowthCycles(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byGrowthCycles is null)
        {
            byGrowthCycles = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrowthCycles;

                if (!byGrowthCycles.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGrowthCycles.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGrowthCycles.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byGrowthCycles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByGrowthCycles(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrowthCycles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFiles(string? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFiles(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFiles(string? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byAOFiles is null)
        {
            byAOFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byAOFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAOFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAOFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byAOFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestSeedTypesDat>> GetManyToManyByAOFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<string, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out HarvestSeedTypesDat? item)
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown52.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown52.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out HarvestSeedTypesDat? item)
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out HarvestSeedTypesDat? item)
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
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
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.RequiredNearbySeed_Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredNearbySeed_Tier(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredNearbySeed_Tier(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.RequiredNearbySeed_Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredNearbySeed_Tier(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byRequiredNearbySeed_Tier is null)
        {
            byRequiredNearbySeed_Tier = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredNearbySeed_Tier;

                if (!byRequiredNearbySeed_Tier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRequiredNearbySeed_Tier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRequiredNearbySeed_Tier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byRequiredNearbySeed_Tier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByRequiredNearbySeed_Tier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredNearbySeed_Tier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.RequiredNearbySeed_Amount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredNearbySeed_Amount(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredNearbySeed_Amount(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.RequiredNearbySeed_Amount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredNearbySeed_Amount(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byRequiredNearbySeed_Amount is null)
        {
            byRequiredNearbySeed_Amount = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredNearbySeed_Amount;

                if (!byRequiredNearbySeed_Amount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRequiredNearbySeed_Amount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRequiredNearbySeed_Amount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byRequiredNearbySeed_Amount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByRequiredNearbySeed_Amount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredNearbySeed_Amount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.WildLifeforceConsumedPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWildLifeforceConsumedPercentage(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWildLifeforceConsumedPercentage(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.WildLifeforceConsumedPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWildLifeforceConsumedPercentage(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byWildLifeforceConsumedPercentage is null)
        {
            byWildLifeforceConsumedPercentage = new();
            foreach (var item in Items)
            {
                var itemKey = item.WildLifeforceConsumedPercentage;

                if (!byWildLifeforceConsumedPercentage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWildLifeforceConsumedPercentage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWildLifeforceConsumedPercentage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byWildLifeforceConsumedPercentage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByWildLifeforceConsumedPercentage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWildLifeforceConsumedPercentage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.VividLifeforceConsumedPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVividLifeforceConsumedPercentage(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVividLifeforceConsumedPercentage(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.VividLifeforceConsumedPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVividLifeforceConsumedPercentage(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byVividLifeforceConsumedPercentage is null)
        {
            byVividLifeforceConsumedPercentage = new();
            foreach (var item in Items)
            {
                var itemKey = item.VividLifeforceConsumedPercentage;

                if (!byVividLifeforceConsumedPercentage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVividLifeforceConsumedPercentage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVividLifeforceConsumedPercentage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byVividLifeforceConsumedPercentage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByVividLifeforceConsumedPercentage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVividLifeforceConsumedPercentage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.PrimalLifeforceConsumedPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPrimalLifeforceConsumedPercentage(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPrimalLifeforceConsumedPercentage(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.PrimalLifeforceConsumedPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPrimalLifeforceConsumedPercentage(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byPrimalLifeforceConsumedPercentage is null)
        {
            byPrimalLifeforceConsumedPercentage = new();
            foreach (var item in Items)
            {
                var itemKey = item.PrimalLifeforceConsumedPercentage;

                if (!byPrimalLifeforceConsumedPercentage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPrimalLifeforceConsumedPercentage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPrimalLifeforceConsumedPercentage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byPrimalLifeforceConsumedPercentage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByPrimalLifeforceConsumedPercentage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPrimalLifeforceConsumedPercentage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByText(string? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByText(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Text"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByText(string? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byText is null)
        {
            byText = new();
            foreach (var item in Items)
            {
                var itemKey = item.Text;

                if (!byText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HarvestSeedTypesDat>> GetManyToManyByText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<string, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.HarvestCraftOptionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHarvestCraftOptionsKeys(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHarvestCraftOptionsKeys(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.HarvestCraftOptionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHarvestCraftOptionsKeys(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byHarvestCraftOptionsKeys is null)
        {
            byHarvestCraftOptionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.HarvestCraftOptionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byHarvestCraftOptionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byHarvestCraftOptionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byHarvestCraftOptionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byHarvestCraftOptionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByHarvestCraftOptionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHarvestCraftOptionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out HarvestSeedTypesDat? item)
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;

                if (!byUnknown120.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown120.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown124(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown124.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown124.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byAchievementItemsKeys is null)
        {
            byAchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.OutcomeType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOutcomeType(int? key, out HarvestSeedTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOutcomeType(key, out var items))
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
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.OutcomeType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOutcomeType(int? key, out IReadOnlyList<HarvestSeedTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        if (byOutcomeType is null)
        {
            byOutcomeType = new();
            foreach (var item in Items)
            {
                var itemKey = item.OutcomeType;

                if (!byOutcomeType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOutcomeType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOutcomeType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HarvestSeedTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HarvestSeedTypesDat"/> with <see cref="HarvestSeedTypesDat.byOutcomeType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HarvestSeedTypesDat>> GetManyToManyByOutcomeType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HarvestSeedTypesDat>>();
        }

        var items = new List<ResultItem<int, HarvestSeedTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOutcomeType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HarvestSeedTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HarvestSeedTypesDat[] Load()
    {
        const string filePath = "Data/HarvestSeedTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestSeedTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HarvestObjectsKey
            (var harvestobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrowthCycles
            (var growthcyclesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RequiredNearbySeed_Tier
            (var requirednearbyseed_tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RequiredNearbySeed_Amount
            (var requirednearbyseed_amountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WildLifeforceConsumedPercentage
            (var wildlifeforceconsumedpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VividLifeforceConsumedPercentage
            (var vividlifeforceconsumedpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PrimalLifeforceConsumedPercentage
            (var primallifeforceconsumedpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HarvestCraftOptionsKeys
            (var tempharvestcraftoptionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var harvestcraftoptionskeysLoading = tempharvestcraftoptionskeysLoading.AsReadOnly();

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var tempunknown124Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown124Loading = tempunknown124Loading.AsReadOnly();

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading OutcomeType
            (var outcometypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestSeedTypesDat()
            {
                HarvestObjectsKey = harvestobjectskeyLoading,
                Unknown16 = unknown16Loading,
                GrowthCycles = growthcyclesLoading,
                AOFiles = aofilesLoading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Tier = tierLoading,
                RequiredNearbySeed_Tier = requirednearbyseed_tierLoading,
                RequiredNearbySeed_Amount = requirednearbyseed_amountLoading,
                WildLifeforceConsumedPercentage = wildlifeforceconsumedpercentageLoading,
                VividLifeforceConsumedPercentage = vividlifeforceconsumedpercentageLoading,
                PrimalLifeforceConsumedPercentage = primallifeforceconsumedpercentageLoading,
                Text = textLoading,
                HarvestCraftOptionsKeys = harvestcraftoptionskeysLoading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                AchievementItemsKeys = achievementitemskeysLoading,
                OutcomeType = outcometypeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
