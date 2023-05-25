using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapPinsDat"/> related data and helper methods.
/// </summary>
public sealed class MapPinsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapPinsDat> Items { get; }

    private Dictionary<string, List<MapPinsDat>>? byId;
    private Dictionary<int, List<MapPinsDat>>? byPositionX;
    private Dictionary<int, List<MapPinsDat>>? byPositionY;
    private Dictionary<int, List<MapPinsDat>>? byWaypoint_WorldAreasKey;
    private Dictionary<int, List<MapPinsDat>>? byWorldAreasKeys;
    private Dictionary<string, List<MapPinsDat>>? byName;
    private Dictionary<string, List<MapPinsDat>>? byFlavourText;
    private Dictionary<int, List<MapPinsDat>>? byUnknown64;
    private Dictionary<int, List<MapPinsDat>>? byAct;
    private Dictionary<string, List<MapPinsDat>>? byUnknown84;
    private Dictionary<int, List<MapPinsDat>>? byUnknown92;
    private Dictionary<int, List<MapPinsDat>>? byUnknown108;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapPinsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapPinsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MapPinsDat? item)
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
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
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapPinsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapPinsDat>>();
        }

        var items = new List<ResultItem<string, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.PositionX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPositionX(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPositionX(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.PositionX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPositionX(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byPositionX is null)
        {
            byPositionX = new();
            foreach (var item in Items)
            {
                var itemKey = item.PositionX;

                if (!byPositionX.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPositionX.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPositionX.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byPositionX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByPositionX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPositionX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.PositionY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPositionY(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPositionY(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.PositionY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPositionY(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byPositionY is null)
        {
            byPositionY = new();
            foreach (var item in Items)
            {
                var itemKey = item.PositionY;

                if (!byPositionY.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPositionY.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPositionY.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byPositionY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByPositionY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPositionY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Waypoint_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWaypoint_WorldAreasKey(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWaypoint_WorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Waypoint_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWaypoint_WorldAreasKey(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byWaypoint_WorldAreasKey is null)
        {
            byWaypoint_WorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Waypoint_WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWaypoint_WorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWaypoint_WorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWaypoint_WorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byWaypoint_WorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByWaypoint_WorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWaypoint_WorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKeys(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKeys(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byWorldAreasKeys is null)
        {
            byWorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byWorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByWorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MapPinsDat? item)
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
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
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapPinsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapPinsDat>>();
        }

        var items = new List<ResultItem<string, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourText(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byFlavourText is null)
        {
            byFlavourText = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourText;

                if (!byFlavourText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFlavourText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapPinsDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapPinsDat>>();
        }

        var items = new List<ResultItem<string, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown64.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown64.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Act"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAct(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAct(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Act"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAct(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byAct is null)
        {
            byAct = new();
            foreach (var item in Items)
            {
                var itemKey = item.Act;

                if (!byAct.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAct.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAct.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byAct"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByAct(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAct(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(string? key, out MapPinsDat? item)
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(string? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;

                if (!byUnknown84.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown84.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown84.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapPinsDat>> GetManyToManyByUnknown84(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapPinsDat>>();
        }

        var items = new List<ResultItem<string, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown92(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown92.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown92.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out MapPinsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown108(key, out var items))
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
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<MapPinsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;

                if (!byUnknown108.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapPinsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapPinsDat"/> with <see cref="MapPinsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapPinsDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapPinsDat>>();
        }

        var items = new List<ResultItem<int, MapPinsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapPinsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapPinsDat[] Load()
    {
        const string filePath = "Data/MapPins.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapPinsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PositionX
            (var positionxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PositionY
            (var positionyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Waypoint_WorldAreasKey
            (var waypoint_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading Act
            (var actLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var tempunknown92Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown92Loading = tempunknown92Loading.AsReadOnly();

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapPinsDat()
            {
                Id = idLoading,
                PositionX = positionxLoading,
                PositionY = positionyLoading,
                Waypoint_WorldAreasKey = waypoint_worldareaskeyLoading,
                WorldAreasKeys = worldareaskeysLoading,
                Name = nameLoading,
                FlavourText = flavourtextLoading,
                Unknown64 = unknown64Loading,
                Act = actLoading,
                Unknown84 = unknown84Loading,
                Unknown92 = unknown92Loading,
                Unknown108 = unknown108Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
