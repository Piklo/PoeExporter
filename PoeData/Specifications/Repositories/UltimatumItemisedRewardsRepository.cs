using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UltimatumItemisedRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class UltimatumItemisedRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UltimatumItemisedRewardsDat> Items { get; }

    private Dictionary<string, List<UltimatumItemisedRewardsDat>>? byId;
    private Dictionary<int, List<UltimatumItemisedRewardsDat>>? byHASH16;
    private Dictionary<string, List<UltimatumItemisedRewardsDat>>? byRewardText;
    private Dictionary<int, List<UltimatumItemisedRewardsDat>>? byItemVisualIdentityKey;
    private Dictionary<int, List<UltimatumItemisedRewardsDat>>? byRewardType;
    private Dictionary<int, List<UltimatumItemisedRewardsDat>>? bySacrificeItem;
    private Dictionary<int, List<UltimatumItemisedRewardsDat>>? bySacrificeAmount;
    private Dictionary<string, List<UltimatumItemisedRewardsDat>>? bySacrificeText;
    private Dictionary<bool, List<UltimatumItemisedRewardsDat>>? byUnknown68;
    private Dictionary<int, List<UltimatumItemisedRewardsDat>>? byTrialMods;

    /// <summary>
    /// Initializes a new instance of the <see cref="UltimatumItemisedRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UltimatumItemisedRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UltimatumItemisedRewardsDat? item)
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
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
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumItemisedRewardsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<string, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH16(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (byHASH16 is null)
        {
            byHASH16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH16;

                if (!byHASH16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumItemisedRewardsDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<int, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.RewardText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardText(string? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardText(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.RewardText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardText(string? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (byRewardText is null)
        {
            byRewardText = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardText;

                if (!byRewardText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byRewardText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumItemisedRewardsDat>> GetManyToManyByRewardText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<string, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey(int? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey(int? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (byItemVisualIdentityKey is null)
        {
            byItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumItemisedRewardsDat>> GetManyToManyByItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<int, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.RewardType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardType(int? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardType(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.RewardType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardType(int? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (byRewardType is null)
        {
            byRewardType = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardType;

                if (!byRewardType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byRewardType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumItemisedRewardsDat>> GetManyToManyByRewardType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<int, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.SacrificeItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySacrificeItem(int? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySacrificeItem(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.SacrificeItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySacrificeItem(int? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (bySacrificeItem is null)
        {
            bySacrificeItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.SacrificeItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySacrificeItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySacrificeItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySacrificeItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.bySacrificeItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumItemisedRewardsDat>> GetManyToManyBySacrificeItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<int, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySacrificeItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.SacrificeAmount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySacrificeAmount(int? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySacrificeAmount(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.SacrificeAmount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySacrificeAmount(int? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (bySacrificeAmount is null)
        {
            bySacrificeAmount = new();
            foreach (var item in Items)
            {
                var itemKey = item.SacrificeAmount;

                if (!bySacrificeAmount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySacrificeAmount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySacrificeAmount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.bySacrificeAmount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumItemisedRewardsDat>> GetManyToManyBySacrificeAmount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<int, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySacrificeAmount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.SacrificeText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySacrificeText(string? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySacrificeText(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.SacrificeText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySacrificeText(string? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (bySacrificeText is null)
        {
            bySacrificeText = new();
            foreach (var item in Items)
            {
                var itemKey = item.SacrificeText;

                if (!bySacrificeText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySacrificeText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySacrificeText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.bySacrificeText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UltimatumItemisedRewardsDat>> GetManyToManyBySacrificeText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<string, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySacrificeText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(bool? key, out UltimatumItemisedRewardsDat? item)
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(bool? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
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
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UltimatumItemisedRewardsDat>> GetManyToManyByUnknown68(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<bool, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.TrialMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTrialMods(int? key, out UltimatumItemisedRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTrialMods(key, out var items))
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
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.TrialMods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTrialMods(int? key, out IReadOnlyList<UltimatumItemisedRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        if (byTrialMods is null)
        {
            byTrialMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.TrialMods;
                foreach (var listKey in itemKey)
                {
                    if (!byTrialMods.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTrialMods.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTrialMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UltimatumItemisedRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UltimatumItemisedRewardsDat"/> with <see cref="UltimatumItemisedRewardsDat.byTrialMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UltimatumItemisedRewardsDat>> GetManyToManyByTrialMods(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UltimatumItemisedRewardsDat>>();
        }

        var items = new List<ResultItem<int, UltimatumItemisedRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTrialMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UltimatumItemisedRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UltimatumItemisedRewardsDat[] Load()
    {
        const string filePath = "Data/UltimatumItemisedRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumItemisedRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardText
            (var rewardtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading RewardType
            (var rewardtypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SacrificeItem
            (var sacrificeitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SacrificeAmount
            (var sacrificeamountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SacrificeText
            (var sacrificetextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TrialMods
            (var temptrialmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var trialmodsLoading = temptrialmodsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumItemisedRewardsDat()
            {
                Id = idLoading,
                HASH16 = hash16Loading,
                RewardText = rewardtextLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                RewardType = rewardtypeLoading,
                SacrificeItem = sacrificeitemLoading,
                SacrificeAmount = sacrificeamountLoading,
                SacrificeText = sacrificetextLoading,
                Unknown68 = unknown68Loading,
                TrialMods = trialmodsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
