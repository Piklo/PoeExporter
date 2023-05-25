using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthsDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthsDat> Items { get; }

    private Dictionary<int, List<LabyrinthsDat>>? byTier;
    private Dictionary<string, List<LabyrinthsDat>>? byName;
    private Dictionary<int, List<LabyrinthsDat>>? byOfferingItem;
    private Dictionary<int, List<LabyrinthsDat>>? byQuestFlag;
    private Dictionary<int, List<LabyrinthsDat>>? byRequiredTrials;
    private Dictionary<int, List<LabyrinthsDat>>? byAreaLevel;
    private Dictionary<int, List<LabyrinthsDat>>? byUnknown64;
    private Dictionary<int, List<LabyrinthsDat>>? byJewelReward;
    private Dictionary<int, List<LabyrinthsDat>>? byUnknown84;
    private Dictionary<int, List<LabyrinthsDat>>? byUnknown100;
    private Dictionary<int, List<LabyrinthsDat>>? byMinLevel;
    private Dictionary<int, List<LabyrinthsDat>>? byUnknown120;
    private Dictionary<int, List<LabyrinthsDat>>? byUnknown124;
    private Dictionary<int, List<LabyrinthsDat>>? byCraftingFontDescription;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out LabyrinthsDat? item)
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
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
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out LabyrinthsDat? item)
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
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
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LabyrinthsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<string, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.OfferingItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOfferingItem(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOfferingItem(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.OfferingItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOfferingItem(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byOfferingItem is null)
        {
            byOfferingItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.OfferingItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byOfferingItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byOfferingItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byOfferingItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byOfferingItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByOfferingItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOfferingItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuestFlag(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuestFlag(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.QuestFlag"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuestFlag(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byQuestFlag is null)
        {
            byQuestFlag = new();
            foreach (var item in Items)
            {
                var itemKey = item.QuestFlag;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuestFlag.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuestFlag.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuestFlag.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byQuestFlag"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByQuestFlag(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuestFlag(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.RequiredTrials"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRequiredTrials(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRequiredTrials(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.RequiredTrials"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRequiredTrials(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byRequiredTrials is null)
        {
            byRequiredTrials = new();
            foreach (var item in Items)
            {
                var itemKey = item.RequiredTrials;
                foreach (var listKey in itemKey)
                {
                    if (!byRequiredTrials.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRequiredTrials.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRequiredTrials.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byRequiredTrials"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByRequiredTrials(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRequiredTrials(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.JewelReward"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByJewelReward(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByJewelReward(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.JewelReward"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByJewelReward(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byJewelReward is null)
        {
            byJewelReward = new();
            foreach (var item in Items)
            {
                var itemKey = item.JewelReward;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byJewelReward.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byJewelReward.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byJewelReward.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byJewelReward"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByJewelReward(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByJewelReward(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out LabyrinthsDat? item)
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown84.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown84.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out LabyrinthsDat? item)
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown100.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown100.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(int? key, out LabyrinthsDat? item)
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
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
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByUnknown120(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out LabyrinthsDat? item)
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;

                if (!byUnknown124.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown124.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.CraftingFontDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCraftingFontDescription(int? key, out LabyrinthsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCraftingFontDescription(key, out var items))
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
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.CraftingFontDescription"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCraftingFontDescription(int? key, out IReadOnlyList<LabyrinthsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        if (byCraftingFontDescription is null)
        {
            byCraftingFontDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.CraftingFontDescription;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCraftingFontDescription.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCraftingFontDescription.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCraftingFontDescription.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthsDat"/> with <see cref="LabyrinthsDat.byCraftingFontDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthsDat>> GetManyToManyByCraftingFontDescription(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthsDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCraftingFontDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthsDat[] Load()
    {
        const string filePath = "Data/Labyrinths.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OfferingItem
            (var offeringitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RequiredTrials
            (var temprequiredtrialsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var requiredtrialsLoading = temprequiredtrialsLoading.AsReadOnly();

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading JewelReward
            (var jewelrewardLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading Unknown100
            (var tempunknown100Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown100Loading = tempunknown100Loading.AsReadOnly();

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CraftingFontDescription
            (var craftingfontdescriptionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthsDat()
            {
                Tier = tierLoading,
                Name = nameLoading,
                OfferingItem = offeringitemLoading,
                QuestFlag = questflagLoading,
                RequiredTrials = requiredtrialsLoading,
                AreaLevel = arealevelLoading,
                Unknown64 = unknown64Loading,
                JewelReward = jewelrewardLoading,
                Unknown84 = unknown84Loading,
                Unknown100 = unknown100Loading,
                MinLevel = minlevelLoading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                CraftingFontDescription = craftingfontdescriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
