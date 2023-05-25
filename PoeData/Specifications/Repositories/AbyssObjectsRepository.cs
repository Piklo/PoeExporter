using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AbyssObjectsDat"/> related data and helper methods.
/// </summary>
public sealed class AbyssObjectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AbyssObjectsDat> Items { get; }

    private Dictionary<string, List<AbyssObjectsDat>>? byId;
    private Dictionary<int, List<AbyssObjectsDat>>? byMinLevel;
    private Dictionary<int, List<AbyssObjectsDat>>? byMaxLevel;
    private Dictionary<int, List<AbyssObjectsDat>>? bySpawnWeight;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown20;
    private Dictionary<string, List<AbyssObjectsDat>>? byMetadataFile;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown32;
    private Dictionary<int, List<AbyssObjectsDat>>? byDaemonSpawners;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown52;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown56;
    private Dictionary<int, List<AbyssObjectsDat>>? byAbyssalDepths;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown76;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown80;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown84;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown88;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown92;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown96;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown100;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown104;
    private Dictionary<int, List<AbyssObjectsDat>>? byUnknown108;
    private Dictionary<bool, List<AbyssObjectsDat>>? byUnknown112;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbyssObjectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AbyssObjectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AbyssObjectsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<string, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;

                if (!bySpawnWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.MetadataFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMetadataFile(string? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMetadataFile(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.MetadataFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMetadataFile(string? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byMetadataFile is null)
        {
            byMetadataFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.MetadataFile;

                if (!byMetadataFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMetadataFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMetadataFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byMetadataFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AbyssObjectsDat>> GetManyToManyByMetadataFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<string, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMetadataFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.DaemonSpawners"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDaemonSpawners(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDaemonSpawners(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.DaemonSpawners"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDaemonSpawners(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byDaemonSpawners is null)
        {
            byDaemonSpawners = new();
            foreach (var item in Items)
            {
                var itemKey = item.DaemonSpawners;
                foreach (var listKey in itemKey)
                {
                    if (!byDaemonSpawners.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byDaemonSpawners.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byDaemonSpawners.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byDaemonSpawners"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByDaemonSpawners(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDaemonSpawners(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.AbyssalDepths"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAbyssalDepths(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAbyssalDepths(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.AbyssalDepths"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAbyssalDepths(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byAbyssalDepths is null)
        {
            byAbyssalDepths = new();
            foreach (var item in Items)
            {
                var itemKey = item.AbyssalDepths;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAbyssalDepths.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAbyssalDepths.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAbyssalDepths.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byAbyssalDepths"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByAbyssalDepths(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAbyssalDepths(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown76(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;

                if (!byUnknown76.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown76.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;

                if (!byUnknown80.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;

                if (!byUnknown92.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;

                if (!byUnknown96.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out AbyssObjectsDat? item)
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
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
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AbyssObjectsDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<int, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(bool? key, out AbyssObjectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown112(key, out var items))
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
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(bool? key, out IReadOnlyList<AbyssObjectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;

                if (!byUnknown112.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AbyssObjectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AbyssObjectsDat"/> with <see cref="AbyssObjectsDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AbyssObjectsDat>> GetManyToManyByUnknown112(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AbyssObjectsDat>>();
        }

        var items = new List<ResultItem<bool, AbyssObjectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AbyssObjectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AbyssObjectsDat[] Load()
    {
        const string filePath = "Data/AbyssObjects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AbyssObjectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MetadataFile
            (var metadatafileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DaemonSpawners
            (var tempdaemonspawnersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var daemonspawnersLoading = tempdaemonspawnersLoading.AsReadOnly();

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AbyssalDepths
            (var abyssaldepthsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AbyssObjectsDat()
            {
                Id = idLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                SpawnWeight = spawnweightLoading,
                Unknown20 = unknown20Loading,
                MetadataFile = metadatafileLoading,
                Unknown32 = unknown32Loading,
                DaemonSpawners = daemonspawnersLoading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                AbyssalDepths = abyssaldepthsLoading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                Unknown112 = unknown112Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
