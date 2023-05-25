using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="QuestRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class QuestRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<QuestRewardsDat> Items { get; }

    private Dictionary<int, List<QuestRewardsDat>>? byRewardOffer;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown16;
    private Dictionary<int, List<QuestRewardsDat>>? byCharacters;
    private Dictionary<int, List<QuestRewardsDat>>? byReward;
    private Dictionary<int, List<QuestRewardsDat>>? byRewardLevel;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown56;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown72;
    private Dictionary<string, List<QuestRewardsDat>>? byUnknown76;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown84;
    private Dictionary<int, List<QuestRewardsDat>>? byRewardStack;
    private Dictionary<bool, List<QuestRewardsDat>>? byUnknown104;
    private Dictionary<bool, List<QuestRewardsDat>>? byUnknown105;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown106;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown122;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown126;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown130;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown146;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown150;
    private Dictionary<int, List<QuestRewardsDat>>? byUnknown166;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal QuestRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.RewardOffer"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardOffer(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardOffer(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.RewardOffer"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardOffer(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byRewardOffer is null)
        {
            byRewardOffer = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardOffer;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRewardOffer.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRewardOffer.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardOffer.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byRewardOffer"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByRewardOffer(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardOffer(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out QuestRewardsDat? item)
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Characters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharacters(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharacters(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Characters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharacters(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byCharacters is null)
        {
            byCharacters = new();
            foreach (var item in Items)
            {
                var itemKey = item.Characters;
                foreach (var listKey in itemKey)
                {
                    if (!byCharacters.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCharacters.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCharacters.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byCharacters"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByCharacters(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharacters(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Reward"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByReward(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByReward(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Reward"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByReward(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byReward is null)
        {
            byReward = new();
            foreach (var item in Items)
            {
                var itemKey = item.Reward;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byReward.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byReward.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byReward.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byReward"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByReward(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByReward(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.RewardLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardLevel(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardLevel(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.RewardLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardLevel(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byRewardLevel is null)
        {
            byRewardLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardLevel;

                if (!byRewardLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byRewardLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByRewardLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown56.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(string? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown76(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(string? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;

                if (!byUnknown76.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown76.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown76.TryGetValue(key, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, QuestRewardsDat>> GetManyToManyByUnknown76(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<string, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out QuestRewardsDat? item)
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
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
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.RewardStack"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardStack(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardStack(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.RewardStack"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardStack(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byRewardStack is null)
        {
            byRewardStack = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardStack;

                if (!byRewardStack.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardStack.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardStack.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byRewardStack"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByRewardStack(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardStack(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(bool? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(bool? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, QuestRewardsDat>> GetManyToManyByUnknown104(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<bool, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(bool? key, out QuestRewardsDat? item)
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(bool? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;

                if (!byUnknown105.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown105.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown105.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, QuestRewardsDat>> GetManyToManyByUnknown105(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<bool, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown106(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown106(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown106(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown106 is null)
        {
            byUnknown106 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown106;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown106.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown106.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown106.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown106"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown106(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown106(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown122(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown122(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown122(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown122 is null)
        {
            byUnknown122 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown122;

                if (!byUnknown122.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown122.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown122.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown122"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown122(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown122(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown126(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown126(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown126(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown126 is null)
        {
            byUnknown126 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown126;

                if (!byUnknown126.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown126.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown126.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown126"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown126(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown126(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown130"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown130(int? key, out QuestRewardsDat? item)
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown130"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown130(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown130 is null)
        {
            byUnknown130 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown130;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown130.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown130.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown130.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown130"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown130(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown130(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown146(int? key, out QuestRewardsDat? item)
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown146(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
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
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown146"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown146(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown146(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown150"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown150(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown150(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown150"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown150(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown150 is null)
        {
            byUnknown150 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown150;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown150.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown150.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown150.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown150"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown150(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown150(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown166"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown166(int? key, out QuestRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown166(key, out var items))
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
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.Unknown166"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown166(int? key, out IReadOnlyList<QuestRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        if (byUnknown166 is null)
        {
            byUnknown166 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown166;

                if (!byUnknown166.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown166.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown166.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<QuestRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="QuestRewardsDat"/> with <see cref="QuestRewardsDat.byUnknown166"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, QuestRewardsDat>> GetManyToManyByUnknown166(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, QuestRewardsDat>>();
        }

        var items = new List<ResultItem<int, QuestRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown166(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, QuestRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private QuestRewardsDat[] Load()
    {
        const string filePath = "Data/QuestRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading RewardOffer
            (var rewardofferLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Characters
            (var tempcharactersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var charactersLoading = tempcharactersLoading.AsReadOnly();

            // loading Reward
            (var rewardLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RewardLevel
            (var rewardlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading RewardStack
            (var rewardstackLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var tempunknown106Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown106Loading = tempunknown106Loading.AsReadOnly();

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown130
            (var tempunknown130Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown130Loading = tempunknown130Loading.AsReadOnly();

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown150
            (var unknown150Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown166
            (var unknown166Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestRewardsDat()
            {
                RewardOffer = rewardofferLoading,
                Unknown16 = unknown16Loading,
                Characters = charactersLoading,
                Reward = rewardLoading,
                RewardLevel = rewardlevelLoading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown84 = unknown84Loading,
                RewardStack = rewardstackLoading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
                Unknown122 = unknown122Loading,
                Unknown126 = unknown126Loading,
                Unknown130 = unknown130Loading,
                Unknown146 = unknown146Loading,
                Unknown150 = unknown150Loading,
                Unknown166 = unknown166Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
