using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapSeriesDat"/> related data and helper methods.
/// </summary>
public sealed class MapSeriesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapSeriesDat> Items { get; }

    private Dictionary<string, List<MapSeriesDat>>? byId;
    private Dictionary<string, List<MapSeriesDat>>? byName;
    private Dictionary<string, List<MapSeriesDat>>? byBaseIcon_DDSFile;
    private Dictionary<string, List<MapSeriesDat>>? byInfected_DDSFile;
    private Dictionary<string, List<MapSeriesDat>>? byShaper_DDSFile;
    private Dictionary<string, List<MapSeriesDat>>? byElder_DDSFile;
    private Dictionary<string, List<MapSeriesDat>>? byDrawn_DDSFile;
    private Dictionary<string, List<MapSeriesDat>>? byDelirious_DDSFile;
    private Dictionary<string, List<MapSeriesDat>>? byUberBlight_DDSFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapSeriesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapSeriesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MapSeriesDat? item)
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
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
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out MapSeriesDat? item)
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
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
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.BaseIcon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseIcon_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseIcon_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.BaseIcon_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseIcon_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byBaseIcon_DDSFile is null)
        {
            byBaseIcon_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseIcon_DDSFile;

                if (!byBaseIcon_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseIcon_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseIcon_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byBaseIcon_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByBaseIcon_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseIcon_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Infected_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfected_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfected_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Infected_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfected_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byInfected_DDSFile is null)
        {
            byInfected_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Infected_DDSFile;

                if (!byInfected_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInfected_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInfected_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byInfected_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByInfected_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfected_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Shaper_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShaper_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShaper_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Shaper_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShaper_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byShaper_DDSFile is null)
        {
            byShaper_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Shaper_DDSFile;

                if (!byShaper_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShaper_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShaper_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byShaper_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByShaper_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShaper_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Elder_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByElder_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByElder_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Elder_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByElder_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byElder_DDSFile is null)
        {
            byElder_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Elder_DDSFile;

                if (!byElder_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byElder_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byElder_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byElder_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByElder_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByElder_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Drawn_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDrawn_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDrawn_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Drawn_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDrawn_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byDrawn_DDSFile is null)
        {
            byDrawn_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Drawn_DDSFile;

                if (!byDrawn_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDrawn_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDrawn_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byDrawn_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByDrawn_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDrawn_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Delirious_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDelirious_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDelirious_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.Delirious_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDelirious_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byDelirious_DDSFile is null)
        {
            byDelirious_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Delirious_DDSFile;

                if (!byDelirious_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDelirious_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDelirious_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byDelirious_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByDelirious_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDelirious_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.UberBlight_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUberBlight_DDSFile(string? key, out MapSeriesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUberBlight_DDSFile(key, out var items))
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
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.UberBlight_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUberBlight_DDSFile(string? key, out IReadOnlyList<MapSeriesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        if (byUberBlight_DDSFile is null)
        {
            byUberBlight_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.UberBlight_DDSFile;

                if (!byUberBlight_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUberBlight_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUberBlight_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapSeriesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapSeriesDat"/> with <see cref="MapSeriesDat.byUberBlight_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapSeriesDat>> GetManyToManyByUberBlight_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapSeriesDat>>();
        }

        var items = new List<ResultItem<string, MapSeriesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUberBlight_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapSeriesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapSeriesDat[] Load()
    {
        const string filePath = "Data/MapSeries.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapSeriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseIcon_DDSFile
            (var baseicon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Infected_DDSFile
            (var infected_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Shaper_DDSFile
            (var shaper_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Elder_DDSFile
            (var elder_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Drawn_DDSFile
            (var drawn_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Delirious_DDSFile
            (var delirious_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UberBlight_DDSFile
            (var uberblight_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapSeriesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                BaseIcon_DDSFile = baseicon_ddsfileLoading,
                Infected_DDSFile = infected_ddsfileLoading,
                Shaper_DDSFile = shaper_ddsfileLoading,
                Elder_DDSFile = elder_ddsfileLoading,
                Drawn_DDSFile = drawn_ddsfileLoading,
                Delirious_DDSFile = delirious_ddsfileLoading,
                UberBlight_DDSFile = uberblight_ddsfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
