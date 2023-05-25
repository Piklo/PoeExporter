using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AchievementItemsDat"/> related data and helper methods.
/// </summary>
public sealed class AchievementItemsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AchievementItemsDat> Items { get; }

    private Dictionary<string, List<AchievementItemsDat>>? byId;
    private Dictionary<int, List<AchievementItemsDat>>? byUnknown8;
    private Dictionary<int, List<AchievementItemsDat>>? byUnknown12;
    private Dictionary<string, List<AchievementItemsDat>>? byName;
    private Dictionary<int, List<AchievementItemsDat>>? byCompletionsRequired;
    private Dictionary<int, List<AchievementItemsDat>>? byAchievementsKey;
    private Dictionary<bool, List<AchievementItemsDat>>? byUnknown44;
    private Dictionary<bool, List<AchievementItemsDat>>? byUnknown45;
    private Dictionary<bool, List<AchievementItemsDat>>? byUnknown46;
    private Dictionary<bool, List<AchievementItemsDat>>? byUnknown47;

    /// <summary>
    /// Initializes a new instance of the <see cref="AchievementItemsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AchievementItemsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AchievementItemsDat? item)
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
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
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementItemsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<string, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementItemsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<int, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementItemsDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<int, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out AchievementItemsDat? item)
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
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
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AchievementItemsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<string, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.CompletionsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCompletionsRequired(int? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCompletionsRequired(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.CompletionsRequired"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCompletionsRequired(int? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byCompletionsRequired is null)
        {
            byCompletionsRequired = new();
            foreach (var item in Items)
            {
                var itemKey = item.CompletionsRequired;

                if (!byCompletionsRequired.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCompletionsRequired.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCompletionsRequired.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byCompletionsRequired"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementItemsDat>> GetManyToManyByCompletionsRequired(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<int, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCompletionsRequired(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.AchievementsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementsKey(int? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementsKey(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.AchievementsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementsKey(int? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byAchievementsKey is null)
        {
            byAchievementsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byAchievementsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AchievementItemsDat>> GetManyToManyByAchievementsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<int, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(bool? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(bool? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementItemsDat>> GetManyToManyByUnknown44(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(bool? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown45(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(bool? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byUnknown45 is null)
        {
            byUnknown45 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown45;

                if (!byUnknown45.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown45.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown45.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementItemsDat>> GetManyToManyByUnknown45(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown46(bool? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown46(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown46(bool? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byUnknown46 is null)
        {
            byUnknown46 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown46;

                if (!byUnknown46.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown46.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown46.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byUnknown46"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementItemsDat>> GetManyToManyByUnknown46(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown46(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown47(bool? key, out AchievementItemsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown47(key, out var items))
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
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown47(bool? key, out IReadOnlyList<AchievementItemsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        if (byUnknown47 is null)
        {
            byUnknown47 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown47;

                if (!byUnknown47.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown47.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown47.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AchievementItemsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AchievementItemsDat"/> with <see cref="AchievementItemsDat.byUnknown47"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AchievementItemsDat>> GetManyToManyByUnknown47(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AchievementItemsDat>>();
        }

        var items = new List<ResultItem<bool, AchievementItemsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown47(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AchievementItemsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AchievementItemsDat[] Load()
    {
        const string filePath = "Data/AchievementItems.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CompletionsRequired
            (var completionsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementsKey
            (var achievementskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementItemsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Name = nameLoading,
                CompletionsRequired = completionsrequiredLoading,
                AchievementsKey = achievementskeyLoading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown47 = unknown47Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
