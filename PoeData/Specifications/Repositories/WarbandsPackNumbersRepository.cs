using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WarbandsPackNumbersDat"/> related data and helper methods.
/// </summary>
public sealed class WarbandsPackNumbersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WarbandsPackNumbersDat> Items { get; }

    private Dictionary<string, List<WarbandsPackNumbersDat>>? byId;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? bySpawnChance;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byMinLevel;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byMaxLevel;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byTier4Number;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byUnknown24;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byTier3Number;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byUnknown32;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byTier2Number;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byUnknown40;
    private Dictionary<int, List<WarbandsPackNumbersDat>>? byTier1Number;

    /// <summary>
    /// Initializes a new instance of the <see cref="WarbandsPackNumbersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WarbandsPackNumbersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out WarbandsPackNumbersDat? item)
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
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
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackNumbersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.SpawnChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnChance(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnChance(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.SpawnChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnChance(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (bySpawnChance is null)
        {
            bySpawnChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnChance;

                if (!bySpawnChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.bySpawnChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyBySpawnChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier4Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier4Number(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier4Number(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier4Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier4Number(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byTier4Number is null)
        {
            byTier4Number = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier4Number;

                if (!byTier4Number.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier4Number.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier4Number.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byTier4Number"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByTier4Number(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier4Number(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out WarbandsPackNumbersDat? item)
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier3Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier3Number(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier3Number(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier3Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier3Number(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byTier3Number is null)
        {
            byTier3Number = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier3Number;

                if (!byTier3Number.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier3Number.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier3Number.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byTier3Number"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByTier3Number(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier3Number(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out WarbandsPackNumbersDat? item)
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
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
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier2Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier2Number(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier2Number(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier2Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier2Number(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byTier2Number is null)
        {
            byTier2Number = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier2Number;

                if (!byTier2Number.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier2Number.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier2Number.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byTier2Number"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByTier2Number(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier2Number(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out WarbandsPackNumbersDat? item)
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
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
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier1Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier1Number(int? key, out WarbandsPackNumbersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier1Number(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.Tier1Number"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier1Number(int? key, out IReadOnlyList<WarbandsPackNumbersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        if (byTier1Number is null)
        {
            byTier1Number = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier1Number;

                if (!byTier1Number.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier1Number.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier1Number.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackNumbersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackNumbersDat"/> with <see cref="WarbandsPackNumbersDat.byTier1Number"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackNumbersDat>> GetManyToManyByTier1Number(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackNumbersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackNumbersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier1Number(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackNumbersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WarbandsPackNumbersDat[] Load()
    {
        const string filePath = "Data/WarbandsPackNumbers.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WarbandsPackNumbersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnChance
            (var spawnchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier4Number
            (var tier4numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier3Number
            (var tier3numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier2Number
            (var tier2numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier1Number
            (var tier1numberLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WarbandsPackNumbersDat()
            {
                Id = idLoading,
                SpawnChance = spawnchanceLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Tier4Number = tier4numberLoading,
                Unknown24 = unknown24Loading,
                Tier3Number = tier3numberLoading,
                Unknown32 = unknown32Loading,
                Tier2Number = tier2numberLoading,
                Unknown40 = unknown40Loading,
                Tier1Number = tier1numberLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
