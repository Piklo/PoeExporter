using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="TableMonsterSpawnersDat"/> related data and helper methods.
/// </summary>
public sealed class TableMonsterSpawnersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<TableMonsterSpawnersDat> Items { get; }

    private Dictionary<string, List<TableMonsterSpawnersDat>>? byMetadata;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byAreaLevel;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? bySpawnsMonsters;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown28;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown32;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown36;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown40;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown44;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown48;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown52;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown56;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown60;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown61;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown62;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown63;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown64;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown65;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown66;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown70;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown74;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown78;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown82;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown98;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown99;
    private Dictionary<string, List<TableMonsterSpawnersDat>>? byScript1;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown108;
    private Dictionary<bool, List<TableMonsterSpawnersDat>>? byUnknown109;
    private Dictionary<string, List<TableMonsterSpawnersDat>>? byScript2;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown118;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown134;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown138;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown142;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown146;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown150;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown154;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown158;
    private Dictionary<int, List<TableMonsterSpawnersDat>>? byUnknown162;

    /// <summary>
    /// Initializes a new instance of the <see cref="TableMonsterSpawnersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal TableMonsterSpawnersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Metadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMetadata(string? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMetadata(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Metadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMetadata(string? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byMetadata is null)
        {
            byMetadata = new();
            foreach (var item in Items)
            {
                var itemKey = item.Metadata;

                if (!byMetadata.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMetadata.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMetadata.TryGetValue(key, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byMetadata"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, TableMonsterSpawnersDat>> GetManyToManyByMetadata(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<string, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMetadata(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.SpawnsMonsters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnsMonsters(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnsMonsters(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.SpawnsMonsters"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnsMonsters(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (bySpawnsMonsters is null)
        {
            bySpawnsMonsters = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnsMonsters;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnsMonsters.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnsMonsters.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnsMonsters.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.bySpawnsMonsters"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyBySpawnsMonsters(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnsMonsters(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown60(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown61(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown62(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown63(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown63(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown63"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown63(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown63(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown64(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown65(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown66(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown70(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown74(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown74"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown74(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown74"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown74(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown74(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown78(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown78(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown78"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown78(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown78(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown82(int? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown82(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown82 is null)
        {
            byUnknown82 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown82;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown82.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown82.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown82.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown82"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown82(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown82(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown98(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown98"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown98(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown98"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown98(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown98(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown99(bool? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown99(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown99(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown99 is null)
        {
            byUnknown99 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown99;

                if (!byUnknown99.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown99.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown99.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown99"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown99(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown99(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Script1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript1(string? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript1(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Script1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript1(string? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byScript1 is null)
        {
            byScript1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script1;

                if (!byScript1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript1.TryGetValue(key, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byScript1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, TableMonsterSpawnersDat>> GetManyToManyByScript1(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<string, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown108(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(bool? key, out TableMonsterSpawnersDat? item)
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(bool? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
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
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, TableMonsterSpawnersDat>> GetManyToManyByUnknown109(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<bool, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Script2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScript2(string? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScript2(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Script2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScript2(string? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byScript2 is null)
        {
            byScript2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Script2;

                if (!byScript2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScript2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScript2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byScript2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, TableMonsterSpawnersDat>> GetManyToManyByScript2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<string, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScript2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown118"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown118(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown118(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown118"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown118(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown118 is null)
        {
            byUnknown118 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown118;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown118.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown118.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown118.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown118"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown118(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown118(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown134"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown134(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown134(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown134"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown134(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown134 is null)
        {
            byUnknown134 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown134;

                if (!byUnknown134.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown134.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown134.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown134"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown134(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown134(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown138"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown138(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown138(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown138"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown138(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown138 is null)
        {
            byUnknown138 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown138;

                if (!byUnknown138.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown138.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown138.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown138"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown138(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown138(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown142(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown142 is null)
        {
            byUnknown142 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown142;

                if (!byUnknown142.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown142.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown142.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown142(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown146(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown146(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown146(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown146 is null)
        {
            byUnknown146 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown146;

                if (!byUnknown146.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown146.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown146.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown146"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown146(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown146(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown150"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown150(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown150(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown150"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown150(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown150 is null)
        {
            byUnknown150 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown150;

                if (!byUnknown150.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown150.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown150.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown150"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown150(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown150(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown154"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown154(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown154(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown154"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown154(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown154 is null)
        {
            byUnknown154 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown154;

                if (!byUnknown154.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown154.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown154.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown154"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown154(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown154(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown158"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown158(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown158(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown158"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown158(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown158 is null)
        {
            byUnknown158 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown158;

                if (!byUnknown158.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown158.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown158.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown158"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown158(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown158(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown162"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown162(int? key, out TableMonsterSpawnersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown162(key, out var items))
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
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.Unknown162"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown162(int? key, out IReadOnlyList<TableMonsterSpawnersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        if (byUnknown162 is null)
        {
            byUnknown162 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown162;

                if (!byUnknown162.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown162.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown162.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<TableMonsterSpawnersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="TableMonsterSpawnersDat"/> with <see cref="TableMonsterSpawnersDat.byUnknown162"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, TableMonsterSpawnersDat>> GetManyToManyByUnknown162(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, TableMonsterSpawnersDat>>();
        }

        var items = new List<ResultItem<int, TableMonsterSpawnersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown162(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, TableMonsterSpawnersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private TableMonsterSpawnersDat[] Load()
    {
        const string filePath = "Data/TableMonsterSpawners.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TableMonsterSpawnersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Metadata
            (var metadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnsMonsters
            (var tempspawnsmonstersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnsmonstersLoading = tempspawnsmonstersLoading.AsReadOnly();

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
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Script1
            (var script1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Script2
            (var script2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown118
            (var tempunknown118Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown118Loading = tempunknown118Loading.AsReadOnly();

            // loading Unknown134
            (var unknown134Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown138
            (var unknown138Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown150
            (var unknown150Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown154
            (var unknown154Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown158
            (var unknown158Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown162
            (var unknown162Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TableMonsterSpawnersDat()
            {
                Metadata = metadataLoading,
                AreaLevel = arealevelLoading,
                SpawnsMonsters = spawnsmonstersLoading,
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
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown82 = unknown82Loading,
                Unknown98 = unknown98Loading,
                Unknown99 = unknown99Loading,
                Script1 = script1Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Script2 = script2Loading,
                Unknown118 = unknown118Loading,
                Unknown134 = unknown134Loading,
                Unknown138 = unknown138Loading,
                Unknown142 = unknown142Loading,
                Unknown146 = unknown146Loading,
                Unknown150 = unknown150Loading,
                Unknown154 = unknown154Loading,
                Unknown158 = unknown158Loading,
                Unknown162 = unknown162Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
