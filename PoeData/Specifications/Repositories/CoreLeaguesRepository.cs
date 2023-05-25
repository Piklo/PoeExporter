using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CoreLeaguesDat"/> related data and helper methods.
/// </summary>
public sealed class CoreLeaguesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CoreLeaguesDat> Items { get; }

    private Dictionary<string, List<CoreLeaguesDat>>? byId;
    private Dictionary<bool, List<CoreLeaguesDat>>? byUnknown8;
    private Dictionary<bool, List<CoreLeaguesDat>>? byUnknown9;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown10;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown26;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown42;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown58;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown74;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown90;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown106;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown122;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown138;
    private Dictionary<bool, List<CoreLeaguesDat>>? byUnknown142;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown143;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown159;
    private Dictionary<bool, List<CoreLeaguesDat>>? byUnknown175;
    private Dictionary<bool, List<CoreLeaguesDat>>? byUnknown176;
    private Dictionary<int, List<CoreLeaguesDat>>? byUnknown177;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoreLeaguesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CoreLeaguesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CoreLeaguesDat? item)
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
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
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CoreLeaguesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<string, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(bool? key, out CoreLeaguesDat? item)
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(bool? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
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
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CoreLeaguesDat>> GetManyToManyByUnknown8(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<bool, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown9(bool? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown9(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown9(bool? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown9 is null)
        {
            byUnknown9 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown9;

                if (!byUnknown9.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown9.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown9.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown9"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CoreLeaguesDat>> GetManyToManyByUnknown9(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<bool, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown9(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown10(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown10(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown10(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown10 is null)
        {
            byUnknown10 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown10;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown10.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown10.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown10.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown10"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown10(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown10(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown26(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown26(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown26(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown26 is null)
        {
            byUnknown26 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown26;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown26.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown26.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown26.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown26"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown26(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown26(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown42(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown42(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown42(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown42 is null)
        {
            byUnknown42 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown42;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown42.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown42.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown42.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown42"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown42(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown42(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown58"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown58(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown58(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown58"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown58(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown58 is null)
        {
            byUnknown58 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown58;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown58.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown58.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown58.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown58"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown58(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown58(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown74(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown74 is null)
        {
            byUnknown74 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown74;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown74.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown74.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown74.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown74(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown90"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown90(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown90(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown90"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown90(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown90 is null)
        {
            byUnknown90 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown90;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown90.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown90.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown90.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown90"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown90(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown90(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown106(int? key, out CoreLeaguesDat? item)
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown106(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown106 is null)
        {
            byUnknown106 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown106;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown106.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown106.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown106.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown106"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown106(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown106(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown122(int? key, out CoreLeaguesDat? item)
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown122(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown122 is null)
        {
            byUnknown122 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown122;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown122.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown122.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown122.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown122"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown122(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown122(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown138"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown138(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown138(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown138"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown138(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown138 is null)
        {
            byUnknown138 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown138;

                if (!byUnknown138.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown138.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown138.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown138"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown138(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown138(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(bool? key, out CoreLeaguesDat? item)
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(bool? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
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
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CoreLeaguesDat>> GetManyToManyByUnknown142(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<bool, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown143(int? key, out CoreLeaguesDat? item)
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown143(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown143 is null)
        {
            byUnknown143 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown143;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown143.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown143.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown143.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown143"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown143(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown143(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown159"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown159(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown159(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown159"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown159(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown159 is null)
        {
            byUnknown159 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown159;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown159.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown159.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown159.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown159"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown159(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown159(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown175"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown175(bool? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown175(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown175"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown175(bool? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown175 is null)
        {
            byUnknown175 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown175;

                if (!byUnknown175.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown175.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown175.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown175"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CoreLeaguesDat>> GetManyToManyByUnknown175(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<bool, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown175(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown176(bool? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown176(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown176(bool? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown176 is null)
        {
            byUnknown176 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown176;

                if (!byUnknown176.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown176.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown176.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown176"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CoreLeaguesDat>> GetManyToManyByUnknown176(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<bool, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown176(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown177"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown177(int? key, out CoreLeaguesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown177(key, out var items))
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
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.Unknown177"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown177(int? key, out IReadOnlyList<CoreLeaguesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        if (byUnknown177 is null)
        {
            byUnknown177 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown177;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown177.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown177.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown177.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CoreLeaguesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CoreLeaguesDat"/> with <see cref="CoreLeaguesDat.byUnknown177"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CoreLeaguesDat>> GetManyToManyByUnknown177(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CoreLeaguesDat>>();
        }

        var items = new List<ResultItem<int, CoreLeaguesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown177(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CoreLeaguesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CoreLeaguesDat[] Load()
    {
        const string filePath = "Data/CoreLeagues.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CoreLeaguesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown10
            (var unknown10Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown26
            (var tempunknown26Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown26Loading = tempunknown26Loading.AsReadOnly();

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown90
            (var tempunknown90Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown90Loading = tempunknown90Loading.AsReadOnly();

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown138
            (var unknown138Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown159
            (var tempunknown159Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown159Loading = tempunknown159Loading.AsReadOnly();

            // loading Unknown175
            (var unknown175Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown177
            (var tempunknown177Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown177Loading = tempunknown177Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CoreLeaguesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Unknown10 = unknown10Loading,
                Unknown26 = unknown26Loading,
                Unknown42 = unknown42Loading,
                Unknown58 = unknown58Loading,
                Unknown74 = unknown74Loading,
                Unknown90 = unknown90Loading,
                Unknown106 = unknown106Loading,
                Unknown122 = unknown122Loading,
                Unknown138 = unknown138Loading,
                Unknown142 = unknown142Loading,
                Unknown143 = unknown143Loading,
                Unknown159 = unknown159Loading,
                Unknown175 = unknown175Loading,
                Unknown176 = unknown176Loading,
                Unknown177 = unknown177Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
