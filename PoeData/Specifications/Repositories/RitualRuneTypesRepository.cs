using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RitualRuneTypesDat"/> related data and helper methods.
/// </summary>
public sealed class RitualRuneTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RitualRuneTypesDat> Items { get; }

    private Dictionary<string, List<RitualRuneTypesDat>>? byId;
    private Dictionary<int, List<RitualRuneTypesDat>>? byMiscAnimatedKey1;
    private Dictionary<int, List<RitualRuneTypesDat>>? bySpawnWeight;
    private Dictionary<int, List<RitualRuneTypesDat>>? byLevelMin;
    private Dictionary<int, List<RitualRuneTypesDat>>? byLevelMax;
    private Dictionary<int, List<RitualRuneTypesDat>>? byBuffDefinitionsKey;
    private Dictionary<int, List<RitualRuneTypesDat>>? byUnknown52;
    private Dictionary<int, List<RitualRuneTypesDat>>? bySpawnPatterns;
    private Dictionary<int, List<RitualRuneTypesDat>>? byModsKey;
    private Dictionary<int, List<RitualRuneTypesDat>>? byUnknown100;
    private Dictionary<int, List<RitualRuneTypesDat>>? byUnknown116;
    private Dictionary<int, List<RitualRuneTypesDat>>? byMiscAnimatedKey2;
    private Dictionary<int, List<RitualRuneTypesDat>>? byEnvironmentsKey;
    private Dictionary<int, List<RitualRuneTypesDat>>? byUnknown164;
    private Dictionary<int, List<RitualRuneTypesDat>>? byAchievements;
    private Dictionary<string, List<RitualRuneTypesDat>>? byType;
    private Dictionary<string, List<RitualRuneTypesDat>>? byDescription;
    private Dictionary<int, List<RitualRuneTypesDat>>? byDaemonSpawningDataKey;
    private Dictionary<bool, List<RitualRuneTypesDat>>? byUnknown216;

    /// <summary>
    /// Initializes a new instance of the <see cref="RitualRuneTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RitualRuneTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out RitualRuneTypesDat? item)
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
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
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RitualRuneTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<string, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.MiscAnimatedKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey1(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey1(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.MiscAnimatedKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey1(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byMiscAnimatedKey1 is null)
        {
            byMiscAnimatedKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byMiscAnimatedKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByMiscAnimatedKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out RitualRuneTypesDat? item)
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
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
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.LevelMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevelMin(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevelMin(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.LevelMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevelMin(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byLevelMin is null)
        {
            byLevelMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.LevelMin;

                if (!byLevelMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevelMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevelMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byLevelMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByLevelMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevelMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.LevelMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevelMax(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevelMax(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.LevelMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevelMax(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byLevelMax is null)
        {
            byLevelMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.LevelMax;

                if (!byLevelMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevelMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevelMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byLevelMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByLevelMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevelMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDefinitionsKey(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byBuffDefinitionsKey is null)
        {
            byBuffDefinitionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDefinitionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffDefinitionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffDefinitionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDefinitionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byBuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByBuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out RitualRuneTypesDat? item)
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown52.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown52.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.SpawnPatterns"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnPatterns(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnPatterns(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.SpawnPatterns"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnPatterns(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (bySpawnPatterns is null)
        {
            bySpawnPatterns = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnPatterns;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnPatterns.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnPatterns.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnPatterns.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.bySpawnPatterns"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyBySpawnPatterns(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnPatterns(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byModsKey is null)
        {
            byModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out RitualRuneTypesDat? item)
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown100.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown100.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(int? key, out RitualRuneTypesDat? item)
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown116.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown116.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByUnknown116(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.MiscAnimatedKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey2(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey2(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.MiscAnimatedKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey2(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byMiscAnimatedKey2 is null)
        {
            byMiscAnimatedKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byMiscAnimatedKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByMiscAnimatedKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.EnvironmentsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnvironmentsKey(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnvironmentsKey(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.EnvironmentsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnvironmentsKey(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byEnvironmentsKey is null)
        {
            byEnvironmentsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnvironmentsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnvironmentsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnvironmentsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnvironmentsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byEnvironmentsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByEnvironmentsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnvironmentsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown164(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown164(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown164(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byUnknown164 is null)
        {
            byUnknown164 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown164;

                if (!byUnknown164.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown164.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown164.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byUnknown164"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByUnknown164(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown164(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievements(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievements(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Achievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievements(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byAchievements is null)
        {
            byAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.Achievements;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByType(string? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByType(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Type"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByType(string? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byType is null)
        {
            byType = new();
            foreach (var item in Items)
            {
                var itemKey = item.Type;

                if (!byType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byType.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RitualRuneTypesDat>> GetManyToManyByType(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<string, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, RitualRuneTypesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<string, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.DaemonSpawningDataKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDaemonSpawningDataKey(int? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDaemonSpawningDataKey(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.DaemonSpawningDataKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDaemonSpawningDataKey(int? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byDaemonSpawningDataKey is null)
        {
            byDaemonSpawningDataKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.DaemonSpawningDataKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDaemonSpawningDataKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDaemonSpawningDataKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDaemonSpawningDataKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byDaemonSpawningDataKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualRuneTypesDat>> GetManyToManyByDaemonSpawningDataKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<int, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDaemonSpawningDataKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown216(bool? key, out RitualRuneTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown216(key, out var items))
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
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown216(bool? key, out IReadOnlyList<RitualRuneTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        if (byUnknown216 is null)
        {
            byUnknown216 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown216;

                if (!byUnknown216.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown216.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown216.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualRuneTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualRuneTypesDat"/> with <see cref="RitualRuneTypesDat.byUnknown216"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, RitualRuneTypesDat>> GetManyToManyByUnknown216(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, RitualRuneTypesDat>>();
        }

        var items = new List<ResultItem<bool, RitualRuneTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown216(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, RitualRuneTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RitualRuneTypesDat[] Load()
    {
        const string filePath = "Data/RitualRuneTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RitualRuneTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LevelMin
            (var levelminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LevelMax
            (var levelmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading SpawnPatterns
            (var tempspawnpatternsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnpatternsLoading = tempspawnpatternsLoading.AsReadOnly();

            // loading ModsKey
            (var tempmodskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeyLoading = tempmodskeyLoading.AsReadOnly();

            // loading Unknown100
            (var tempunknown100Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown100Loading = tempunknown100Loading.AsReadOnly();

            // loading Unknown116
            (var tempunknown116Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown116Loading = tempunknown116Loading.AsReadOnly();

            // loading MiscAnimatedKey2
            (var miscanimatedkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading EnvironmentsKey
            (var environmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DaemonSpawningDataKey
            (var daemonspawningdatakeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RitualRuneTypesDat()
            {
                Id = idLoading,
                MiscAnimatedKey1 = miscanimatedkey1Loading,
                SpawnWeight = spawnweightLoading,
                LevelMin = levelminLoading,
                LevelMax = levelmaxLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown52 = unknown52Loading,
                SpawnPatterns = spawnpatternsLoading,
                ModsKey = modskeyLoading,
                Unknown100 = unknown100Loading,
                Unknown116 = unknown116Loading,
                MiscAnimatedKey2 = miscanimatedkey2Loading,
                EnvironmentsKey = environmentskeyLoading,
                Unknown164 = unknown164Loading,
                Achievements = achievementsLoading,
                Type = typeLoading,
                Description = descriptionLoading,
                DaemonSpawningDataKey = daemonspawningdatakeyLoading,
                Unknown216 = unknown216Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
