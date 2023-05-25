using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="PlayerConditionsDat"/> related data and helper methods.
/// </summary>
public sealed class PlayerConditionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<PlayerConditionsDat> Items { get; }

    private Dictionary<string, List<PlayerConditionsDat>>? byId;
    private Dictionary<int, List<PlayerConditionsDat>>? byBuffDefinitionsKeys;
    private Dictionary<bool, List<PlayerConditionsDat>>? byUnknown24;
    private Dictionary<int, List<PlayerConditionsDat>>? byBuffStacks;
    private Dictionary<int, List<PlayerConditionsDat>>? byCharactersKey;
    private Dictionary<int, List<PlayerConditionsDat>>? byStatsKeys;
    private Dictionary<bool, List<PlayerConditionsDat>>? byUnknown61;
    private Dictionary<int, List<PlayerConditionsDat>>? byStatValue;
    private Dictionary<int, List<PlayerConditionsDat>>? byUnknown66;
    private Dictionary<bool, List<PlayerConditionsDat>>? byUnknown82;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerConditionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal PlayerConditionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out PlayerConditionsDat? item)
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
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
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, PlayerConditionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<string, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.BuffDefinitionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKeys(int? key, out PlayerConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDefinitionsKeys(key, out var items))
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.BuffDefinitionsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKeys(int? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        if (byBuffDefinitionsKeys is null)
        {
            byBuffDefinitionsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDefinitionsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffDefinitionsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffDefinitionsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffDefinitionsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byBuffDefinitionsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PlayerConditionsDat>> GetManyToManyByBuffDefinitionsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<int, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out PlayerConditionsDat? item)
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
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
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PlayerConditionsDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<bool, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.BuffStacks"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffStacks(int? key, out PlayerConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffStacks(key, out var items))
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.BuffStacks"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffStacks(int? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        if (byBuffStacks is null)
        {
            byBuffStacks = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffStacks;

                if (!byBuffStacks.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBuffStacks.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffStacks.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byBuffStacks"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PlayerConditionsDat>> GetManyToManyByBuffStacks(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<int, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffStacks(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCharactersKey(int? key, out PlayerConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCharactersKey(key, out var items))
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.CharactersKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCharactersKey(int? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        if (byCharactersKey is null)
        {
            byCharactersKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.CharactersKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCharactersKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCharactersKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCharactersKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byCharactersKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PlayerConditionsDat>> GetManyToManyByCharactersKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<int, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCharactersKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out PlayerConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys(key, out var items))
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        if (byStatsKeys is null)
        {
            byStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PlayerConditionsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<int, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(bool? key, out PlayerConditionsDat? item)
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(bool? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
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
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PlayerConditionsDat>> GetManyToManyByUnknown61(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<bool, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.StatValue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValue(int? key, out PlayerConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValue(key, out var items))
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.StatValue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValue(int? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        if (byStatValue is null)
        {
            byStatValue = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValue;

                if (!byStatValue.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStatValue.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStatValue.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byStatValue"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PlayerConditionsDat>> GetManyToManyByStatValue(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<int, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValue(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(int? key, out PlayerConditionsDat? item)
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(int? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown66.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown66.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, PlayerConditionsDat>> GetManyToManyByUnknown66(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<int, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown82(bool? key, out PlayerConditionsDat? item)
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
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.Unknown82"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown82(bool? key, out IReadOnlyList<PlayerConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<PlayerConditionsDat>();
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
            items = Array.Empty<PlayerConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="PlayerConditionsDat"/> with <see cref="PlayerConditionsDat.byUnknown82"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, PlayerConditionsDat>> GetManyToManyByUnknown82(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, PlayerConditionsDat>>();
        }

        var items = new List<ResultItem<bool, PlayerConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown82(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, PlayerConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private PlayerConditionsDat[] Load()
    {
        const string filePath = "Data/PlayerConditions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PlayerConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDefinitionsKeys
            (var tempbuffdefinitionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var buffdefinitionskeysLoading = tempbuffdefinitionskeysLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BuffStacks
            (var buffstacksLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CharactersKey
            (var characterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown66
            (var tempunknown66Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown66Loading = tempunknown66Loading.AsReadOnly();

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PlayerConditionsDat()
            {
                Id = idLoading,
                BuffDefinitionsKeys = buffdefinitionskeysLoading,
                Unknown24 = unknown24Loading,
                BuffStacks = buffstacksLoading,
                CharactersKey = characterskeyLoading,
                StatsKeys = statskeysLoading,
                Unknown61 = unknown61Loading,
                StatValue = statvalueLoading,
                Unknown66 = unknown66Loading,
                Unknown82 = unknown82Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
