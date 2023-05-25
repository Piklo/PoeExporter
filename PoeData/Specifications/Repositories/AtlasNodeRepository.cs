using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AtlasNodeDat"/> related data and helper methods.
/// </summary>
public sealed class AtlasNodeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AtlasNodeDat> Items { get; }

    private Dictionary<int, List<AtlasNodeDat>>? byWorldAreasKey;
    private Dictionary<int, List<AtlasNodeDat>>? byItemVisualIdentityKey;
    private Dictionary<bool, List<AtlasNodeDat>>? byUnknown32;
    private Dictionary<int, List<AtlasNodeDat>>? byMapsKey;
    private Dictionary<int, List<AtlasNodeDat>>? byFlavourTextKey;
    private Dictionary<int, List<AtlasNodeDat>>? byAtlasNodeKeys;
    private Dictionary<int, List<AtlasNodeDat>>? byTier0;
    private Dictionary<int, List<AtlasNodeDat>>? byTier1;
    private Dictionary<int, List<AtlasNodeDat>>? byTier2;
    private Dictionary<int, List<AtlasNodeDat>>? byTier3;
    private Dictionary<int, List<AtlasNodeDat>>? byTier4;
    private Dictionary<float, List<AtlasNodeDat>>? byUnknown101;
    private Dictionary<float, List<AtlasNodeDat>>? byUnknown105;
    private Dictionary<float, List<AtlasNodeDat>>? byUnknown109;
    private Dictionary<float, List<AtlasNodeDat>>? byUnknown113;
    private Dictionary<float, List<AtlasNodeDat>>? byUnknown117;
    private Dictionary<string, List<AtlasNodeDat>>? byDDSFile;
    private Dictionary<bool, List<AtlasNodeDat>>? byUnknown129;
    private Dictionary<bool, List<AtlasNodeDat>>? byNotOnAtlas;

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasNodeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AtlasNodeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWorldAreasKey(int? key, out AtlasNodeDat? item)
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWorldAreasKey(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
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
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byWorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByWorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.ItemVisualIdentityKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byItemVisualIdentityKey is null)
        {
            byItemVisualIdentityKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byItemVisualIdentityKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByItemVisualIdentityKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(bool? key, out AtlasNodeDat? item)
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(bool? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
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
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AtlasNodeDat>> GetManyToManyByUnknown32(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<bool, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapsKey(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapsKey(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapsKey(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byMapsKey is null)
        {
            byMapsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byMapsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByMapsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourTextKey(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourTextKey(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourTextKey(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byFlavourTextKey is null)
        {
            byFlavourTextKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourTextKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFlavourTextKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFlavourTextKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourTextKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byFlavourTextKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByFlavourTextKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourTextKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.AtlasNodeKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAtlasNodeKeys(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAtlasNodeKeys(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.AtlasNodeKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAtlasNodeKeys(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byAtlasNodeKeys is null)
        {
            byAtlasNodeKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AtlasNodeKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAtlasNodeKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAtlasNodeKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAtlasNodeKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byAtlasNodeKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByAtlasNodeKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAtlasNodeKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier0(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier0(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier0(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byTier0 is null)
        {
            byTier0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier0;

                if (!byTier0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byTier0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByTier0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier1(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier1(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier1(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byTier1 is null)
        {
            byTier1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier1;

                if (!byTier1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byTier1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByTier1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier2(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier2(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier2(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byTier2 is null)
        {
            byTier2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier2;

                if (!byTier2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byTier2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByTier2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier3(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier3(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier3(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byTier3 is null)
        {
            byTier3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier3;

                if (!byTier3.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier3.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byTier3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByTier3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier4(int? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier4(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Tier4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier4(int? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byTier4 is null)
        {
            byTier4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier4;

                if (!byTier4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byTier4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AtlasNodeDat>> GetManyToManyByTier4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<int, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown101(float? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown101(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown101(float? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byUnknown101 is null)
        {
            byUnknown101 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown101;

                if (!byUnknown101.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown101.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown101.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown101"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AtlasNodeDat>> GetManyToManyByUnknown101(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<float, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown101(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(float? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown105(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(float? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;

                if (!byUnknown105.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown105.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown105.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AtlasNodeDat>> GetManyToManyByUnknown105(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<float, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(float? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown109(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(float? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;

                if (!byUnknown109.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown109.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AtlasNodeDat>> GetManyToManyByUnknown109(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<float, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown113(float? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown113(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown113(float? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byUnknown113 is null)
        {
            byUnknown113 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown113;

                if (!byUnknown113.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown113.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown113.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown113"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AtlasNodeDat>> GetManyToManyByUnknown113(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<float, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown113(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown117(float? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown117(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown117(float? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byUnknown117 is null)
        {
            byUnknown117 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown117;

                if (!byUnknown117.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown117.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown117.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown117"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AtlasNodeDat>> GetManyToManyByUnknown117(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<float, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown117(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDDSFile(string? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDDSFile(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDDSFile(string? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byDDSFile is null)
        {
            byDDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DDSFile;

                if (!byDDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byDDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AtlasNodeDat>> GetManyToManyByDDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<string, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown129(bool? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown129(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.Unknown129"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown129(bool? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byUnknown129 is null)
        {
            byUnknown129 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown129;

                if (!byUnknown129.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown129.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown129.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byUnknown129"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AtlasNodeDat>> GetManyToManyByUnknown129(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<bool, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown129(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.NotOnAtlas"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNotOnAtlas(bool? key, out AtlasNodeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNotOnAtlas(key, out var items))
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
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.NotOnAtlas"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNotOnAtlas(bool? key, out IReadOnlyList<AtlasNodeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        if (byNotOnAtlas is null)
        {
            byNotOnAtlas = new();
            foreach (var item in Items)
            {
                var itemKey = item.NotOnAtlas;

                if (!byNotOnAtlas.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byNotOnAtlas.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byNotOnAtlas.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AtlasNodeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AtlasNodeDat"/> with <see cref="AtlasNodeDat.byNotOnAtlas"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AtlasNodeDat>> GetManyToManyByNotOnAtlas(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AtlasNodeDat>>();
        }

        var items = new List<ResultItem<bool, AtlasNodeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNotOnAtlas(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AtlasNodeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AtlasNodeDat[] Load()
    {
        const string filePath = "Data/AtlasNode.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasNodeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MapsKey
            (var mapskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AtlasNodeKeys
            (var tempatlasnodekeysLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var atlasnodekeysLoading = tempatlasnodekeysLoading.AsReadOnly();

            // loading Tier0
            (var tier0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier1
            (var tier1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier2
            (var tier2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier3
            (var tier3Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier4
            (var tier4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NotOnAtlas
            (var notonatlasLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasNodeDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                Unknown32 = unknown32Loading,
                MapsKey = mapskeyLoading,
                FlavourTextKey = flavourtextkeyLoading,
                AtlasNodeKeys = atlasnodekeysLoading,
                Tier0 = tier0Loading,
                Tier1 = tier1Loading,
                Tier2 = tier2Loading,
                Tier3 = tier3Loading,
                Tier4 = tier4Loading,
                Unknown101 = unknown101Loading,
                Unknown105 = unknown105Loading,
                Unknown109 = unknown109Loading,
                Unknown113 = unknown113Loading,
                Unknown117 = unknown117Loading,
                DDSFile = ddsfileLoading,
                Unknown129 = unknown129Loading,
                NotOnAtlas = notonatlasLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
