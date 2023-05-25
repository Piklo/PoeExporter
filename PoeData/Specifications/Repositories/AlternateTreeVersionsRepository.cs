using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AlternateTreeVersionsDat"/> related data and helper methods.
/// </summary>
public sealed class AlternateTreeVersionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AlternateTreeVersionsDat> Items { get; }

    private Dictionary<string, List<AlternateTreeVersionsDat>>? byId;
    private Dictionary<bool, List<AlternateTreeVersionsDat>>? byUnknown8;
    private Dictionary<bool, List<AlternateTreeVersionsDat>>? byUnknown9;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown10;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown14;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown18;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown22;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown26;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown30;
    private Dictionary<int, List<AlternateTreeVersionsDat>>? byUnknown34;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlternateTreeVersionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AlternateTreeVersionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AlternateTreeVersionsDat? item)
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
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
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternateTreeVersionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<string, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(bool? key, out AlternateTreeVersionsDat? item)
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(bool? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
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
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AlternateTreeVersionsDat>> GetManyToManyByUnknown8(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<bool, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown9(bool? key, out AlternateTreeVersionsDat? item)
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown9"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown9(bool? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
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
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown9"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AlternateTreeVersionsDat>> GetManyToManyByUnknown9(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<bool, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown9(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown10(int? key, out AlternateTreeVersionsDat? item)
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown10"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown10(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        if (byUnknown10 is null)
        {
            byUnknown10 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown10;

                if (!byUnknown10.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown10.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown10.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown10"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown10(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown10(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown14"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown14(int? key, out AlternateTreeVersionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown14(key, out var items))
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown14"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown14(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        if (byUnknown14 is null)
        {
            byUnknown14 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown14;

                if (!byUnknown14.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown14.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown14.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown14"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown14(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown14(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown18"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown18(int? key, out AlternateTreeVersionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown18(key, out var items))
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown18"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown18(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        if (byUnknown18 is null)
        {
            byUnknown18 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown18;

                if (!byUnknown18.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown18.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown18.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown18"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown18(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown18(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown22"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown22(int? key, out AlternateTreeVersionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown22(key, out var items))
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown22"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown22(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        if (byUnknown22 is null)
        {
            byUnknown22 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown22;

                if (!byUnknown22.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown22.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown22.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown22"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown22(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown22(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown26(int? key, out AlternateTreeVersionsDat? item)
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown26(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        if (byUnknown26 is null)
        {
            byUnknown26 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown26;

                if (!byUnknown26.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown26.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown26.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown26"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown26(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown26(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown30"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown30(int? key, out AlternateTreeVersionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown30(key, out var items))
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown30"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown30(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        if (byUnknown30 is null)
        {
            byUnknown30 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown30;

                if (!byUnknown30.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown30.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown30.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown30"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown30(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown30(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown34"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown34(int? key, out AlternateTreeVersionsDat? item)
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
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.Unknown34"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown34(int? key, out IReadOnlyList<AlternateTreeVersionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternateTreeVersionsDat>();
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
            items = Array.Empty<AlternateTreeVersionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternateTreeVersionsDat"/> with <see cref="AlternateTreeVersionsDat.byUnknown34"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternateTreeVersionsDat>> GetManyToManyByUnknown34(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternateTreeVersionsDat>>();
        }

        var items = new List<ResultItem<int, AlternateTreeVersionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown34(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternateTreeVersionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AlternateTreeVersionsDat[] Load()
    {
        const string filePath = "Data/AlternateTreeVersions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternateTreeVersionsDat[tableRows];
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
            (var unknown10Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown14
            (var unknown14Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown18
            (var unknown18Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown22
            (var unknown22Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown30
            (var unknown30Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternateTreeVersionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Unknown10 = unknown10Loading,
                Unknown14 = unknown14Loading,
                Unknown18 = unknown18Loading,
                Unknown22 = unknown22Loading,
                Unknown26 = unknown26Loading,
                Unknown30 = unknown30Loading,
                Unknown34 = unknown34Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
