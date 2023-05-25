using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapStashSpecialTypeEntriesDat"/> related data and helper methods.
/// </summary>
public sealed class MapStashSpecialTypeEntriesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapStashSpecialTypeEntriesDat> Items { get; }

    private Dictionary<string, List<MapStashSpecialTypeEntriesDat>>? byId;
    private Dictionary<int, List<MapStashSpecialTypeEntriesDat>>? byUnknown8;
    private Dictionary<int, List<MapStashSpecialTypeEntriesDat>>? byMapItem;
    private Dictionary<string, List<MapStashSpecialTypeEntriesDat>>? byName;
    private Dictionary<int, List<MapStashSpecialTypeEntriesDat>>? byUnknown36;
    private Dictionary<string, List<MapStashSpecialTypeEntriesDat>>? byIcon;
    private Dictionary<string, List<MapStashSpecialTypeEntriesDat>>? byIcon2;
    private Dictionary<bool, List<MapStashSpecialTypeEntriesDat>>? byIsShaperGuardian;
    private Dictionary<bool, List<MapStashSpecialTypeEntriesDat>>? byIsElderGuardian;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapStashSpecialTypeEntriesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapStashSpecialTypeEntriesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MapStashSpecialTypeEntriesDat? item)
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
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
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapStashSpecialTypeEntriesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<string, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out MapStashSpecialTypeEntriesDat? item)
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
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
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapStashSpecialTypeEntriesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<int, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.MapItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapItem(int? key, out MapStashSpecialTypeEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapItem(key, out var items))
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.MapItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapItem(int? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        if (byMapItem is null)
        {
            byMapItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byMapItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapStashSpecialTypeEntriesDat>> GetManyToManyByMapItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<int, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MapStashSpecialTypeEntriesDat? item)
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
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
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapStashSpecialTypeEntriesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<string, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out MapStashSpecialTypeEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapStashSpecialTypeEntriesDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<int, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon(string? key, out MapStashSpecialTypeEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon(key, out var items))
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Icon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon(string? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        if (byIcon is null)
        {
            byIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon;

                if (!byIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapStashSpecialTypeEntriesDat>> GetManyToManyByIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<string, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Icon2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIcon2(string? key, out MapStashSpecialTypeEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIcon2(key, out var items))
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.Icon2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIcon2(string? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        if (byIcon2 is null)
        {
            byIcon2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Icon2;

                if (!byIcon2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIcon2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIcon2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byIcon2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapStashSpecialTypeEntriesDat>> GetManyToManyByIcon2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<string, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIcon2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.IsShaperGuardian"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsShaperGuardian(bool? key, out MapStashSpecialTypeEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsShaperGuardian(key, out var items))
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.IsShaperGuardian"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsShaperGuardian(bool? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        if (byIsShaperGuardian is null)
        {
            byIsShaperGuardian = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsShaperGuardian;

                if (!byIsShaperGuardian.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsShaperGuardian.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsShaperGuardian.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byIsShaperGuardian"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapStashSpecialTypeEntriesDat>> GetManyToManyByIsShaperGuardian(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<bool, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsShaperGuardian(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.IsElderGuardian"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsElderGuardian(bool? key, out MapStashSpecialTypeEntriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsElderGuardian(key, out var items))
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
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.IsElderGuardian"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsElderGuardian(bool? key, out IReadOnlyList<MapStashSpecialTypeEntriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        if (byIsElderGuardian is null)
        {
            byIsElderGuardian = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsElderGuardian;

                if (!byIsElderGuardian.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsElderGuardian.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsElderGuardian.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapStashSpecialTypeEntriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapStashSpecialTypeEntriesDat"/> with <see cref="MapStashSpecialTypeEntriesDat.byIsElderGuardian"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapStashSpecialTypeEntriesDat>> GetManyToManyByIsElderGuardian(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapStashSpecialTypeEntriesDat>>();
        }

        var items = new List<ResultItem<bool, MapStashSpecialTypeEntriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsElderGuardian(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapStashSpecialTypeEntriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapStashSpecialTypeEntriesDat[] Load()
    {
        const string filePath = "Data/MapStashSpecialTypeEntries.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapStashSpecialTypeEntriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MapItem
            (var mapitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon2
            (var icon2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsShaperGuardian
            (var isshaperguardianLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsElderGuardian
            (var iselderguardianLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapStashSpecialTypeEntriesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                MapItem = mapitemLoading,
                Name = nameLoading,
                Unknown36 = unknown36Loading,
                Icon = iconLoading,
                Icon2 = icon2Loading,
                IsShaperGuardian = isshaperguardianLoading,
                IsElderGuardian = iselderguardianLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
