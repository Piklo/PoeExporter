using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AchievementsDat"/> related data and helper methods.
/// </summary>
public sealed class AchievementsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AchievementsDat> Items { get; }

    private Dictionary<string, List<AchievementsDat>>? byId;
    private Dictionary<string, List<AchievementsDat>>? byDescription;
    private Dictionary<int, List<AchievementsDat>>? bySetId;
    private Dictionary<string, List<AchievementsDat>>? byObjective;
    private Dictionary<int, List<AchievementsDat>>? byUnknown28;
    private Dictionary<bool, List<AchievementsDat>>? byUnknown32;
    private Dictionary<bool, List<AchievementsDat>>? byHideAchievementItems;
    private Dictionary<bool, List<AchievementsDat>>? byUnknown34;
    private Dictionary<int, List<AchievementsDat>>? byMinCompletedItems;
    private Dictionary<bool, List<AchievementsDat>>? byTwoColumnLayout;
    private Dictionary<bool, List<AchievementsDat>>? byShowItemCompletionsAsOne;
    private Dictionary<string, List<AchievementsDat>>? byUnknown41;
    private Dictionary<bool, List<AchievementsDat>>? bySoftcoreOnly;
    private Dictionary<bool, List<AchievementsDat>>? byHardcoreOnly;
    private Dictionary<bool, List<AchievementsDat>>? byUnknown51;
    private Dictionary<string, List<AchievementsDat>>? byUnknown52;

    /// <summary>
    /// Initializes a new instance of the <see cref="AchievementsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AchievementsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AchievementsDat? item)
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
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
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementsDat>>();
        }

        var items = new List<ResultItem<string, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out AchievementsDat? item)
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
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
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementsDat>>();
        }

        var items = new List<ResultItem<string, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.SetId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySetId(int? key, out AchievementsDat? item)
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.SetId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySetId(int? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
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
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.bySetId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementsDat>> GetManyToManyBySetId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementsDat>>();
        }

        var items = new List<ResultItem<int, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySetId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Objective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByObjective(string? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByObjective(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Objective"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByObjective(string? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byObjective is null)
        {
            byObjective = new();
            foreach (var item in Items)
            {
                var itemKey = item.Objective;

                if (!byObjective.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byObjective.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byObjective.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byObjective"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementsDat>> GetManyToManyByObjective(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementsDat>>();
        }

        var items = new List<ResultItem<string, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByObjective(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementsDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementsDat>>();
        }

        var items = new List<ResultItem<int, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByUnknown32(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.HideAchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHideAchievementItems(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHideAchievementItems(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.HideAchievementItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHideAchievementItems(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byHideAchievementItems is null)
        {
            byHideAchievementItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.HideAchievementItems;

                if (!byHideAchievementItems.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHideAchievementItems.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHideAchievementItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byHideAchievementItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByHideAchievementItems(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHideAchievementItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown34"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown34(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown34(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown34"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown34(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byUnknown34 is null)
        {
            byUnknown34 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown34;

                if (!byUnknown34.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown34.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown34.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byUnknown34"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByUnknown34(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown34(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.MinCompletedItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinCompletedItems(int? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinCompletedItems(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.MinCompletedItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinCompletedItems(int? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byMinCompletedItems is null)
        {
            byMinCompletedItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinCompletedItems;

                if (!byMinCompletedItems.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinCompletedItems.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinCompletedItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byMinCompletedItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementsDat>> GetManyToManyByMinCompletedItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementsDat>>();
        }

        var items = new List<ResultItem<int, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinCompletedItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.TwoColumnLayout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoColumnLayout(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoColumnLayout(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.TwoColumnLayout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoColumnLayout(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byTwoColumnLayout is null)
        {
            byTwoColumnLayout = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoColumnLayout;

                if (!byTwoColumnLayout.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTwoColumnLayout.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoColumnLayout.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byTwoColumnLayout"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByTwoColumnLayout(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoColumnLayout(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.ShowItemCompletionsAsOne"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShowItemCompletionsAsOne(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShowItemCompletionsAsOne(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.ShowItemCompletionsAsOne"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShowItemCompletionsAsOne(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byShowItemCompletionsAsOne is null)
        {
            byShowItemCompletionsAsOne = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShowItemCompletionsAsOne;

                if (!byShowItemCompletionsAsOne.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShowItemCompletionsAsOne.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShowItemCompletionsAsOne.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byShowItemCompletionsAsOne"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByShowItemCompletionsAsOne(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShowItemCompletionsAsOne(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown41(string? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown41(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown41(string? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byUnknown41 is null)
        {
            byUnknown41 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown41;

                if (!byUnknown41.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown41.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown41.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byUnknown41"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementsDat>> GetManyToManyByUnknown41(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementsDat>>();
        }

        var items = new List<ResultItem<string, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown41(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.SoftcoreOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoftcoreOnly(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoftcoreOnly(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.SoftcoreOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoftcoreOnly(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (bySoftcoreOnly is null)
        {
            bySoftcoreOnly = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoftcoreOnly;

                if (!bySoftcoreOnly.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySoftcoreOnly.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySoftcoreOnly.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.bySoftcoreOnly"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyBySoftcoreOnly(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoftcoreOnly(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.HardcoreOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHardcoreOnly(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHardcoreOnly(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.HardcoreOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHardcoreOnly(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byHardcoreOnly is null)
        {
            byHardcoreOnly = new();
            foreach (var item in Items)
            {
                var itemKey = item.HardcoreOnly;

                if (!byHardcoreOnly.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHardcoreOnly.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHardcoreOnly.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byHardcoreOnly"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByHardcoreOnly(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHardcoreOnly(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown51(bool? key, out AchievementsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown51(key, out var items))
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown51(bool? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byUnknown51 is null)
        {
            byUnknown51 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown51;

                if (!byUnknown51.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown51.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown51.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byUnknown51"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementsDat>> GetManyToManyByUnknown51(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown51(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(string? key, out AchievementsDat? item)
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
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(string? key, out IReadOnlyList<AchievementsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;

                if (!byUnknown52.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AchievementsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementsDat"/> with <see cref="AchievementsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementsDat>> GetManyToManyByUnknown52(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementsDat>>();
        }

        var items = new List<ResultItem<string, AchievementsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AchievementsDat[] Load()
    {
        const string filePath = "Data/Achievements.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SetId
            (var setidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Objective
            (var objectiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HideAchievementItems
            (var hideachievementitemsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinCompletedItems
            (var mincompleteditemsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TwoColumnLayout
            (var twocolumnlayoutLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ShowItemCompletionsAsOne
            (var showitemcompletionsasoneLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoftcoreOnly
            (var softcoreonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HardcoreOnly
            (var hardcoreonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown51
            (var unknown51Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementsDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                SetId = setidLoading,
                Objective = objectiveLoading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                HideAchievementItems = hideachievementitemsLoading,
                Unknown34 = unknown34Loading,
                MinCompletedItems = mincompleteditemsLoading,
                TwoColumnLayout = twocolumnlayoutLoading,
                ShowItemCompletionsAsOne = showitemcompletionsasoneLoading,
                Unknown41 = unknown41Loading,
                SoftcoreOnly = softcoreonlyLoading,
                HardcoreOnly = hardcoreonlyLoading,
                Unknown51 = unknown51Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
