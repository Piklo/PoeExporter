using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="CustomLeagueModsDat"/> related data and helper methods.
/// </summary>
public sealed class CustomLeagueModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<CustomLeagueModsDat> Items { get; }

    private Dictionary<string, List<CustomLeagueModsDat>>? byId;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown8;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown24;
    private Dictionary<bool, List<CustomLeagueModsDat>>? byUnknown40;
    private Dictionary<bool, List<CustomLeagueModsDat>>? byUnknown41;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown42;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown46;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown62;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown66;
    private Dictionary<int, List<CustomLeagueModsDat>>? byUnknown82;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomLeagueModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal CustomLeagueModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out CustomLeagueModsDat? item)
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
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
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, CustomLeagueModsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<string, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out CustomLeagueModsDat? item)
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown8.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown8.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out CustomLeagueModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown24.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown24.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(bool? key, out CustomLeagueModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(bool? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CustomLeagueModsDat>> GetManyToManyByUnknown40(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<bool, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown41(bool? key, out CustomLeagueModsDat? item)
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown41(bool? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
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

        if (!byUnknown41.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown41"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, CustomLeagueModsDat>> GetManyToManyByUnknown41(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<bool, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown41(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown42(int? key, out CustomLeagueModsDat? item)
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown42(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown42 is null)
        {
            byUnknown42 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown42;

                if (!byUnknown42.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown42.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown42.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown42"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown42(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown42(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown46(int? key, out CustomLeagueModsDat? item)
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown46(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown46 is null)
        {
            byUnknown46 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown46;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown46.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown46.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown46.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown46"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown46(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown46(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(int? key, out CustomLeagueModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown62(key, out var items))
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown62 is null)
        {
            byUnknown62 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown62;

                if (!byUnknown62.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown62.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown62.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown62(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(int? key, out CustomLeagueModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown66(key, out var items))
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown66.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown66(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown82(int? key, out CustomLeagueModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown82(key, out var items))
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
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown82(int? key, out IReadOnlyList<CustomLeagueModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        if (byUnknown82 is null)
        {
            byUnknown82 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown82;

                if (!byUnknown82.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown82.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown82.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<CustomLeagueModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="CustomLeagueModsDat"/> with <see cref="CustomLeagueModsDat.byUnknown82"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, CustomLeagueModsDat>> GetManyToManyByUnknown82(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, CustomLeagueModsDat>>();
        }

        var items = new List<ResultItem<int, CustomLeagueModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown82(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, CustomLeagueModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private CustomLeagueModsDat[] Load()
    {
        const string filePath = "Data/CustomLeagueMods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CustomLeagueModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CustomLeagueModsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown42 = unknown42Loading,
                Unknown46 = unknown46Loading,
                Unknown62 = unknown62Loading,
                Unknown66 = unknown66Loading,
                Unknown82 = unknown82Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
