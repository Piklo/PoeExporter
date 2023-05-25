using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveMonsterSpawnersDat"/> related data and helper methods.
/// </summary>
public sealed class DelveMonsterSpawnersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveMonsterSpawnersDat> Items { get; }

    private Dictionary<string, List<DelveMonsterSpawnersDat>>? byBaseMetadata;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown8;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown12;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown28;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown32;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown36;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown40;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown44;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown48;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown52;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown56;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown60;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown61;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown62;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown63;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown64;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown65;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown66;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown67;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown68;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown69;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown70;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown74;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown78;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown82;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown86;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown102;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown103;
    private Dictionary<int, List<DelveMonsterSpawnersDat>>? byUnknown104;
    private Dictionary<string, List<DelveMonsterSpawnersDat>>? byScript;
    private Dictionary<bool, List<DelveMonsterSpawnersDat>>? byUnknown116;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveMonsterSpawnersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveMonsterSpawnersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.BaseMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseMetadata(string? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseMetadata(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.BaseMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseMetadata(string? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byBaseMetadata is null)
        {
            byBaseMetadata = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseMetadata;

                if (!byBaseMetadata.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseMetadata.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseMetadata.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byBaseMetadata"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveMonsterSpawnersDat>> GetManyToManyByBaseMetadata(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<string, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseMetadata(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
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
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown12.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown12.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown28(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown28 is null)
        {
            byUnknown28 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown28;

                if (!byUnknown28.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown28.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown28.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
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
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
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
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
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
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;

                if (!byUnknown52.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;

                if (!byUnknown60.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown60(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown61(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown61 is null)
        {
            byUnknown61 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown61;

                if (!byUnknown61.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown61.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown61.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown61(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(bool? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
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
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown62(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown63(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown63(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown63(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown63 is null)
        {
            byUnknown63 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown63;

                if (!byUnknown63.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown63.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown63.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown63"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown63(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown63(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(bool? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown64(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown65(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown65 is null)
        {
            byUnknown65 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown65;

                if (!byUnknown65.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown65.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown65.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown65(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(bool? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown66(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown67(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown67(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown67(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown67 is null)
        {
            byUnknown67 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown67;

                if (!byUnknown67.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown67.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown67.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown67"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown67(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown67(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown68(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown69(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown69(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown69(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown69 is null)
        {
            byUnknown69 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown69;

                if (!byUnknown69.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown69.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown69.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown69"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown69(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown69(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown70(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown70 is null)
        {
            byUnknown70 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown70;

                if (!byUnknown70.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown70.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown70.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown70(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(int? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown74 is null)
        {
            byUnknown74 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown74;

                if (!byUnknown74.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown74.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown74.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown74(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown78(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown78(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown78(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown78 is null)
        {
            byUnknown78 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown78;

                if (!byUnknown78.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown78.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown78.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown78"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown78(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown78(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown82(int? key, out DelveMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown82(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
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
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown82"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown82(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown82(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown86"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown86(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown86(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown86"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown86(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown86 is null)
        {
            byUnknown86 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown86;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown86.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown86.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown86.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown86"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown86(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown86(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown102(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown102(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown102(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown102 is null)
        {
            byUnknown102 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown102;

                if (!byUnknown102.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown102.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown102.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown102"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown102(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown102(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown103(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown103(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown103(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown103 is null)
        {
            byUnknown103 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown103;

                if (!byUnknown103.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown103.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown103.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown103"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown103(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown103(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveMonsterSpawnersDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Script"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript(string? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Script"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript(string? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byScript is null)
        {
            byScript = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script;

                if (!byScript.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript.TryGetValue(key, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byScript"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DelveMonsterSpawnersDat>> GetManyToManyByScript(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<string, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(bool? key, out DelveMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(bool? key, out IReadOnlyList<DelveMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;

                if (!byUnknown116.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown116.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveMonsterSpawnersDat"/> with <see cref="DelveMonsterSpawnersDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DelveMonsterSpawnersDat>> GetManyToManyByUnknown116(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DelveMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, DelveMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DelveMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveMonsterSpawnersDat[] Load()
    {
        const string filePath = "Data/DelveMonsterSpawners.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveMonsterSpawnersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseMetadata
            (var basemetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown63
            (var unknown63Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown86
            (var unknown86Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveMonsterSpawnersDat()
            {
                BaseMetadata = basemetadataLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown62 = unknown62Loading,
                Unknown63 = unknown63Loading,
                Unknown64 = unknown64Loading,
                Unknown65 = unknown65Loading,
                Unknown66 = unknown66Loading,
                Unknown67 = unknown67Loading,
                Unknown68 = unknown68Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown82 = unknown82Loading,
                Unknown86 = unknown86Loading,
                Unknown102 = unknown102Loading,
                Unknown103 = unknown103Loading,
                Unknown104 = unknown104Loading,
                Script = scriptLoading,
                Unknown116 = unknown116Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
