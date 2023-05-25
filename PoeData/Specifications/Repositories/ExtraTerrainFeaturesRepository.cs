using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExtraTerrainFeaturesDat"/> related data and helper methods.
/// </summary>
public sealed class ExtraTerrainFeaturesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExtraTerrainFeaturesDat> Items { get; }

    private Dictionary<string, List<ExtraTerrainFeaturesDat>>? byId;
    private Dictionary<string, List<ExtraTerrainFeaturesDat>>? byArmFiles;
    private Dictionary<string, List<ExtraTerrainFeaturesDat>>? byTdtFiles;
    private Dictionary<bool, List<ExtraTerrainFeaturesDat>>? byUnknown40;
    private Dictionary<string, List<ExtraTerrainFeaturesDat>>? byUnknown41;
    private Dictionary<int, List<ExtraTerrainFeaturesDat>>? byUnknown57;
    private Dictionary<int, List<ExtraTerrainFeaturesDat>>? byUnknown73;
    private Dictionary<int, List<ExtraTerrainFeaturesDat>>? byWorldAreasKey;
    private Dictionary<bool, List<ExtraTerrainFeaturesDat>>? byUnknown97;
    private Dictionary<int, List<ExtraTerrainFeaturesDat>>? byUnknown98;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtraTerrainFeaturesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExtraTerrainFeaturesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ExtraTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
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
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExtraTerrainFeaturesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<string, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.ArmFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArmFiles(string? key, out ExtraTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArmFiles(key, out var items))
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.ArmFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArmFiles(string? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        if (byArmFiles is null)
        {
            byArmFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArmFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byArmFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byArmFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byArmFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byArmFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExtraTerrainFeaturesDat>> GetManyToManyByArmFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<string, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArmFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.TdtFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTdtFiles(string? key, out ExtraTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTdtFiles(key, out var items))
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.TdtFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTdtFiles(string? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        if (byTdtFiles is null)
        {
            byTdtFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.TdtFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byTdtFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTdtFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTdtFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byTdtFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExtraTerrainFeaturesDat>> GetManyToManyByTdtFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<string, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTdtFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(bool? key, out ExtraTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(bool? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
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
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ExtraTerrainFeaturesDat>> GetManyToManyByUnknown40(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<bool, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown41(string? key, out ExtraTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown41(string? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        if (byUnknown41 is null)
        {
            byUnknown41 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown41;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown41.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown41.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown41.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byUnknown41"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExtraTerrainFeaturesDat>> GetManyToManyByUnknown41(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<string, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown41(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown57(int? key, out ExtraTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown57(int? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
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
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byUnknown57"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExtraTerrainFeaturesDat>> GetManyToManyByUnknown57(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown57(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(int? key, out ExtraTerrainFeaturesDat? item)
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(int? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
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
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExtraTerrainFeaturesDat>> GetManyToManyByUnknown73(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out ExtraTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        if (byWorldAreasKey is null)
        {
            byWorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExtraTerrainFeaturesDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown97(bool? key, out ExtraTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown97(key, out var items))
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown97(bool? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        if (byUnknown97 is null)
        {
            byUnknown97 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown97;

                if (!byUnknown97.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown97.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown97.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byUnknown97"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ExtraTerrainFeaturesDat>> GetManyToManyByUnknown97(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<bool, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown97(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown98(int? key, out ExtraTerrainFeaturesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown98(key, out var items))
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
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown98(int? key, out IReadOnlyList<ExtraTerrainFeaturesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        if (byUnknown98 is null)
        {
            byUnknown98 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown98;

                if (!byUnknown98.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown98.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown98.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExtraTerrainFeaturesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExtraTerrainFeaturesDat"/> with <see cref="ExtraTerrainFeaturesDat.byUnknown98"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExtraTerrainFeaturesDat>> GetManyToManyByUnknown98(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExtraTerrainFeaturesDat>>();
        }

        var items = new List<ResultItem<int, ExtraTerrainFeaturesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown98(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExtraTerrainFeaturesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExtraTerrainFeaturesDat[] Load()
    {
        const string filePath = "Data/ExtraTerrainFeatures.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExtraTerrainFeaturesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ArmFiles
            (var temparmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var armfilesLoading = temparmfilesLoading.AsReadOnly();

            // loading TdtFiles
            (var temptdtfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var tdtfilesLoading = temptdtfilesLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var tempunknown41Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown41Loading = tempunknown41Loading.AsReadOnly();

            // loading Unknown57
            (var tempunknown57Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown57Loading = tempunknown57Loading.AsReadOnly();

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExtraTerrainFeaturesDat()
            {
                Id = idLoading,
                ArmFiles = armfilesLoading,
                TdtFiles = tdtfilesLoading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown57 = unknown57Loading,
                Unknown73 = unknown73Loading,
                WorldAreasKey = worldareaskeyLoading,
                Unknown97 = unknown97Loading,
                Unknown98 = unknown98Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
