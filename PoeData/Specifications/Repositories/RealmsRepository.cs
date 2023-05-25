using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RealmsDat"/> related data and helper methods.
/// </summary>
public sealed class RealmsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RealmsDat> Items { get; }

    private Dictionary<string, List<RealmsDat>>? byId;
    private Dictionary<string, List<RealmsDat>>? byName;
    private Dictionary<string, List<RealmsDat>>? byServer;
    private Dictionary<bool, List<RealmsDat>>? byIsEnabled;
    private Dictionary<string, List<RealmsDat>>? byServer2;
    private Dictionary<string, List<RealmsDat>>? byShortName;
    private Dictionary<int, List<RealmsDat>>? byUnknown57;
    private Dictionary<int, List<RealmsDat>>? byUnknown73;
    private Dictionary<int, List<RealmsDat>>? byUnknown81;
    private Dictionary<bool, List<RealmsDat>>? byIsGammaRealm;
    private Dictionary<string, List<RealmsDat>>? bySpeedtestUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="RealmsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RealmsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out RealmsDat? item)
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
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
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RealmsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RealmsDat>>();
        }

        var items = new List<ResultItem<string, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out RealmsDat? item)
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
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
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RealmsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RealmsDat>>();
        }

        var items = new List<ResultItem<string, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Server"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByServer(string? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByServer(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Server"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByServer(string? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byServer is null)
        {
            byServer = new();
            foreach (var item in Items)
            {
                var itemKey = item.Server;
                foreach (var listKey in itemKey)
                {
                    if (!byServer.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byServer.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byServer.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byServer"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RealmsDat>> GetManyToManyByServer(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RealmsDat>>();
        }

        var items = new List<ResultItem<string, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByServer(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsEnabled(bool? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsEnabled(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsEnabled(bool? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byIsEnabled is null)
        {
            byIsEnabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsEnabled;

                if (!byIsEnabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsEnabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsEnabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byIsEnabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, RealmsDat>> GetManyToManyByIsEnabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, RealmsDat>>();
        }

        var items = new List<ResultItem<bool, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsEnabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Server2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByServer2(string? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByServer2(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Server2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByServer2(string? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byServer2 is null)
        {
            byServer2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Server2;
                foreach (var listKey in itemKey)
                {
                    if (!byServer2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byServer2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byServer2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byServer2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RealmsDat>> GetManyToManyByServer2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RealmsDat>>();
        }

        var items = new List<ResultItem<string, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByServer2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.ShortName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShortName(string? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShortName(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.ShortName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShortName(string? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byShortName is null)
        {
            byShortName = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShortName;

                if (!byShortName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShortName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShortName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byShortName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RealmsDat>> GetManyToManyByShortName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RealmsDat>>();
        }

        var items = new List<ResultItem<string, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShortName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown57(int? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown57(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown57(int? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byUnknown57 is null)
        {
            byUnknown57 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown57;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown57.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown57.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown57.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byUnknown57"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RealmsDat>> GetManyToManyByUnknown57(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RealmsDat>>();
        }

        var items = new List<ResultItem<int, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown57(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(int? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown73(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(int? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byUnknown73 is null)
        {
            byUnknown73 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown73;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown73.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown73.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown73.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RealmsDat>> GetManyToManyByUnknown73(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RealmsDat>>();
        }

        var items = new List<ResultItem<int, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Unknown81"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown81(int? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown81(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.Unknown81"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown81(int? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byUnknown81 is null)
        {
            byUnknown81 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown81;

                if (!byUnknown81.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown81.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown81.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byUnknown81"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RealmsDat>> GetManyToManyByUnknown81(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RealmsDat>>();
        }

        var items = new List<ResultItem<int, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown81(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.IsGammaRealm"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsGammaRealm(bool? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsGammaRealm(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.IsGammaRealm"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsGammaRealm(bool? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (byIsGammaRealm is null)
        {
            byIsGammaRealm = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsGammaRealm;

                if (!byIsGammaRealm.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsGammaRealm.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsGammaRealm.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.byIsGammaRealm"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, RealmsDat>> GetManyToManyByIsGammaRealm(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, RealmsDat>>();
        }

        var items = new List<ResultItem<bool, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsGammaRealm(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.SpeedtestUrl"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpeedtestUrl(string? key, out RealmsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpeedtestUrl(key, out var items))
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
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.SpeedtestUrl"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpeedtestUrl(string? key, out IReadOnlyList<RealmsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        if (bySpeedtestUrl is null)
        {
            bySpeedtestUrl = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpeedtestUrl;
                foreach (var listKey in itemKey)
                {
                    if (!bySpeedtestUrl.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpeedtestUrl.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpeedtestUrl.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RealmsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RealmsDat"/> with <see cref="RealmsDat.bySpeedtestUrl"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RealmsDat>> GetManyToManyBySpeedtestUrl(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RealmsDat>>();
        }

        var items = new List<ResultItem<string, RealmsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpeedtestUrl(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RealmsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RealmsDat[] Load()
    {
        const string filePath = "Data/Realms.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RealmsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Server
            (var tempserverLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var serverLoading = tempserverLoading.AsReadOnly();

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Server2
            (var tempserver2Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var server2Loading = tempserver2Loading.AsReadOnly();

            // loading ShortName
            (var shortnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown57
            (var tempunknown57Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown57Loading = tempunknown57Loading.AsReadOnly();

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsGammaRealm
            (var isgammarealmLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SpeedtestUrl
            (var tempspeedtesturlLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var speedtesturlLoading = tempspeedtesturlLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RealmsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Server = serverLoading,
                IsEnabled = isenabledLoading,
                Server2 = server2Loading,
                ShortName = shortnameLoading,
                Unknown57 = unknown57Loading,
                Unknown73 = unknown73Loading,
                Unknown81 = unknown81Loading,
                IsGammaRealm = isgammarealmLoading,
                SpeedtestUrl = speedtesturlLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
