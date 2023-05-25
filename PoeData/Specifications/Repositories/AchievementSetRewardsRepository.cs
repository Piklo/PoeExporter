using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AchievementSetRewardsDat"/> related data and helper methods.
/// </summary>
public sealed class AchievementSetRewardsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AchievementSetRewardsDat> Items { get; }

    private Dictionary<int, List<AchievementSetRewardsDat>>? bySetId;
    private Dictionary<int, List<AchievementSetRewardsDat>>? byAchievementsRequired;
    private Dictionary<int, List<AchievementSetRewardsDat>>? byRewards;
    private Dictionary<int, List<AchievementSetRewardsDat>>? byTotemPieceEveryNAchievements;
    private Dictionary<string, List<AchievementSetRewardsDat>>? byMessage;
    private Dictionary<string, List<AchievementSetRewardsDat>>? byNotificationIcon;
    private Dictionary<string, List<AchievementSetRewardsDat>>? byHideoutName;
    private Dictionary<string, List<AchievementSetRewardsDat>>? byId;

    /// <summary>
    /// Initializes a new instance of the <see cref="AchievementSetRewardsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AchievementSetRewardsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.SetId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySetId(int? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySetId(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.SetId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySetId(int? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (bySetId is null)
        {
            bySetId = new();
            foreach (var item in Items)
            {
                var itemKey = item.SetId;

                if (!bySetId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySetId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySetId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.bySetId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementSetRewardsDat>> GetManyToManyBySetId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<int, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySetId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.AchievementsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementsRequired(int? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementsRequired(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.AchievementsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementsRequired(int? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (byAchievementsRequired is null)
        {
            byAchievementsRequired = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementsRequired;

                if (!byAchievementsRequired.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAchievementsRequired.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementsRequired.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byAchievementsRequired"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementSetRewardsDat>> GetManyToManyByAchievementsRequired(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<int, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementsRequired(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.Rewards"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewards(int? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewards(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.Rewards"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewards(int? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (byRewards is null)
        {
            byRewards = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rewards;
                foreach (var listKey in itemKey)
                {
                    if (!byRewards.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byRewards.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byRewards.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byRewards"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementSetRewardsDat>> GetManyToManyByRewards(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<int, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewards(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.TotemPieceEveryNAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTotemPieceEveryNAchievements(int? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTotemPieceEveryNAchievements(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.TotemPieceEveryNAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTotemPieceEveryNAchievements(int? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (byTotemPieceEveryNAchievements is null)
        {
            byTotemPieceEveryNAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.TotemPieceEveryNAchievements;

                if (!byTotemPieceEveryNAchievements.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTotemPieceEveryNAchievements.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTotemPieceEveryNAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byTotemPieceEveryNAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementSetRewardsDat>> GetManyToManyByTotemPieceEveryNAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<int, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTotemPieceEveryNAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.Message"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMessage(string? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMessage(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.Message"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMessage(string? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (byMessage is null)
        {
            byMessage = new();
            foreach (var item in Items)
            {
                var itemKey = item.Message;

                if (!byMessage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMessage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMessage.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byMessage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementSetRewardsDat>> GetManyToManyByMessage(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<string, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMessage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.NotificationIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotificationIcon(string? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotificationIcon(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.NotificationIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotificationIcon(string? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (byNotificationIcon is null)
        {
            byNotificationIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.NotificationIcon;

                if (!byNotificationIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNotificationIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNotificationIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byNotificationIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementSetRewardsDat>> GetManyToManyByNotificationIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<string, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotificationIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.HideoutName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHideoutName(string? key, out AchievementSetRewardsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHideoutName(key, out var items))
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.HideoutName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHideoutName(string? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        if (byHideoutName is null)
        {
            byHideoutName = new();
            foreach (var item in Items)
            {
                var itemKey = item.HideoutName;

                if (!byHideoutName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHideoutName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHideoutName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byHideoutName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementSetRewardsDat>> GetManyToManyByHideoutName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<string, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHideoutName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AchievementSetRewardsDat? item)
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
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AchievementSetRewardsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementSetRewardsDat>();
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
            items = Array.Empty<AchievementSetRewardsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementSetRewardsDat"/> with <see cref="AchievementSetRewardsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementSetRewardsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementSetRewardsDat>>();
        }

        var items = new List<ResultItem<string, AchievementSetRewardsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementSetRewardsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AchievementSetRewardsDat[] Load()
    {
        const string filePath = "Data/AchievementSetRewards.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementSetRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SetId
            (var setidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementsRequired
            (var achievementsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Rewards
            (var temprewardsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var rewardsLoading = temprewardsLoading.AsReadOnly();

            // loading TotemPieceEveryNAchievements
            (var totempieceeverynachievementsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Message
            (var messageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotificationIcon
            (var notificationiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HideoutName
            (var hideoutnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementSetRewardsDat()
            {
                SetId = setidLoading,
                AchievementsRequired = achievementsrequiredLoading,
                Rewards = rewardsLoading,
                TotemPieceEveryNAchievements = totempieceeverynachievementsLoading,
                Message = messageLoading,
                NotificationIcon = notificationiconLoading,
                HideoutName = hideoutnameLoading,
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
