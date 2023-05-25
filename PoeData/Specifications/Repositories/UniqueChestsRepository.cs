using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="UniqueChestsDat"/> related data and helper methods.
/// </summary>
public sealed class UniqueChestsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<UniqueChestsDat> Items { get; }

    private Dictionary<string, List<UniqueChestsDat>>? byId;
    private Dictionary<int, List<UniqueChestsDat>>? byWordsKey;
    private Dictionary<int, List<UniqueChestsDat>>? byFlavourTextKey;
    private Dictionary<int, List<UniqueChestsDat>>? byMinLevel;
    private Dictionary<int, List<UniqueChestsDat>>? byModsKeys;
    private Dictionary<int, List<UniqueChestsDat>>? bySpawnWeight;
    private Dictionary<int, List<UniqueChestsDat>>? byUnknown64;
    private Dictionary<string, List<UniqueChestsDat>>? byAOFile;
    private Dictionary<bool, List<UniqueChestsDat>>? byUnknown88;
    private Dictionary<int, List<UniqueChestsDat>>? byUnknown89;
    private Dictionary<int, List<UniqueChestsDat>>? byAppearanceChestsKey;
    private Dictionary<int, List<UniqueChestsDat>>? byChestsKey;
    private Dictionary<int, List<UniqueChestsDat>>? byUnknown137;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueChestsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal UniqueChestsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out UniqueChestsDat? item)
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
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
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueChestsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<string, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWordsKey(int? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWordsKey(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.WordsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWordsKey(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byWordsKey is null)
        {
            byWordsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.WordsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byWordsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byWordsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byWordsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byWordsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByWordsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWordsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourTextKey(int? key, out UniqueChestsDat? item)
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.FlavourTextKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourTextKey(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
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
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byFlavourTextKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByFlavourTextKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourTextKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out UniqueChestsDat? item)
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
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
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys(int? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byModsKeys is null)
        {
            byModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out UniqueChestsDat? item)
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
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
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out UniqueChestsDat? item)
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
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
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;

                if (!byAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, UniqueChestsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<string, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(bool? key, out UniqueChestsDat? item)
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(bool? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
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
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, UniqueChestsDat>> GetManyToManyByUnknown88(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<bool, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown89(int? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown89(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown89(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byUnknown89 is null)
        {
            byUnknown89 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown89;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown89.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown89.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown89.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byUnknown89"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByUnknown89(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown89(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.AppearanceChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAppearanceChestsKey(int? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAppearanceChestsKey(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.AppearanceChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAppearanceChestsKey(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byAppearanceChestsKey is null)
        {
            byAppearanceChestsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AppearanceChestsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAppearanceChestsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAppearanceChestsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAppearanceChestsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byAppearanceChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByAppearanceChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAppearanceChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestsKey(int? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestsKey(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.ChestsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestsKey(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byChestsKey is null)
        {
            byChestsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byChestsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byChestsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byChestsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byChestsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByChestsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown137(int? key, out UniqueChestsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown137(key, out var items))
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
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.Unknown137"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown137(int? key, out IReadOnlyList<UniqueChestsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        if (byUnknown137 is null)
        {
            byUnknown137 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown137;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown137.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown137.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown137.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<UniqueChestsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="UniqueChestsDat"/> with <see cref="UniqueChestsDat.byUnknown137"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, UniqueChestsDat>> GetManyToManyByUnknown137(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, UniqueChestsDat>>();
        }

        var items = new List<ResultItem<int, UniqueChestsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown137(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, UniqueChestsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private UniqueChestsDat[] Load()
    {
        const string filePath = "Data/UniqueChests.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WordsKey
            (var wordskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown89
            (var tempunknown89Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown89Loading = tempunknown89Loading.AsReadOnly();

            // loading AppearanceChestsKey
            (var appearancechestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown137
            (var tempunknown137Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown137Loading = tempunknown137Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueChestsDat()
            {
                Id = idLoading,
                WordsKey = wordskeyLoading,
                FlavourTextKey = flavourtextkeyLoading,
                MinLevel = minlevelLoading,
                ModsKeys = modskeysLoading,
                SpawnWeight = spawnweightLoading,
                Unknown64 = unknown64Loading,
                AOFile = aofileLoading,
                Unknown88 = unknown88Loading,
                Unknown89 = unknown89Loading,
                AppearanceChestsKey = appearancechestskeyLoading,
                ChestsKey = chestskeyLoading,
                Unknown137 = unknown137Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
