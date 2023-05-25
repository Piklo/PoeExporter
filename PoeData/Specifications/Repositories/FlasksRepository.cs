using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="FlasksDat"/> related data and helper methods.
/// </summary>
public sealed class FlasksRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<FlasksDat> Items { get; }

    private Dictionary<int, List<FlasksDat>>? byBaseItemTypesKey;
    private Dictionary<string, List<FlasksDat>>? byName;
    private Dictionary<int, List<FlasksDat>>? byGroup;
    private Dictionary<int, List<FlasksDat>>? byLifePerUse;
    private Dictionary<int, List<FlasksDat>>? byManaPerUse;
    private Dictionary<int, List<FlasksDat>>? byRecoveryTime;
    private Dictionary<int, List<FlasksDat>>? byBuffDefinitionsKey;
    private Dictionary<int, List<FlasksDat>>? byBuffStatValues;
    private Dictionary<int, List<FlasksDat>>? byRecoveryTime2;
    private Dictionary<int, List<FlasksDat>>? byBuffStatValues2;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlasksRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal FlasksRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out FlasksDat? item)
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
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
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, FlasksDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, FlasksDat>>();
        }

        var items = new List<ResultItem<string, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.Group"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroup(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroup(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.Group"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroup(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byGroup is null)
        {
            byGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.Group;

                if (!byGroup.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGroup.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGroup.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByGroup(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.LifePerUse"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifePerUse(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifePerUse(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.LifePerUse"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifePerUse(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byLifePerUse is null)
        {
            byLifePerUse = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifePerUse;

                if (!byLifePerUse.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifePerUse.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifePerUse.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byLifePerUse"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByLifePerUse(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifePerUse(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.ManaPerUse"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByManaPerUse(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByManaPerUse(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.ManaPerUse"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByManaPerUse(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byManaPerUse is null)
        {
            byManaPerUse = new();
            foreach (var item in Items)
            {
                var itemKey = item.ManaPerUse;

                if (!byManaPerUse.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byManaPerUse.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byManaPerUse.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byManaPerUse"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByManaPerUse(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByManaPerUse(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.RecoveryTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecoveryTime(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRecoveryTime(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.RecoveryTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecoveryTime(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byRecoveryTime is null)
        {
            byRecoveryTime = new();
            foreach (var item in Items)
            {
                var itemKey = item.RecoveryTime;

                if (!byRecoveryTime.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRecoveryTime.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRecoveryTime.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byRecoveryTime"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByRecoveryTime(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecoveryTime(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey(int? key, out FlasksDat? item)
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BuffDefinitionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
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
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byBuffDefinitionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByBuffDefinitionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BuffStatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffStatValues(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffStatValues(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BuffStatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffStatValues(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byBuffStatValues is null)
        {
            byBuffStatValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffStatValues;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffStatValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffStatValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffStatValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byBuffStatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByBuffStatValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffStatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.RecoveryTime2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRecoveryTime2(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRecoveryTime2(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.RecoveryTime2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRecoveryTime2(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byRecoveryTime2 is null)
        {
            byRecoveryTime2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.RecoveryTime2;

                if (!byRecoveryTime2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRecoveryTime2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRecoveryTime2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byRecoveryTime2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByRecoveryTime2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRecoveryTime2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BuffStatValues2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffStatValues2(int? key, out FlasksDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffStatValues2(key, out var items))
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
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.BuffStatValues2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffStatValues2(int? key, out IReadOnlyList<FlasksDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        if (byBuffStatValues2 is null)
        {
            byBuffStatValues2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffStatValues2;
                foreach (var listKey in itemKey)
                {
                    if (!byBuffStatValues2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBuffStatValues2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBuffStatValues2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FlasksDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FlasksDat"/> with <see cref="FlasksDat.byBuffStatValues2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FlasksDat>> GetManyToManyByBuffStatValues2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FlasksDat>>();
        }

        var items = new List<ResultItem<int, FlasksDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffStatValues2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FlasksDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private FlasksDat[] Load()
    {
        const string filePath = "Data/Flasks.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FlasksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Group
            (var groupLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifePerUse
            (var lifeperuseLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ManaPerUse
            (var manaperuseLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RecoveryTime
            (var recoverytimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffStatValues
            (var tempbuffstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buffstatvaluesLoading = tempbuffstatvaluesLoading.AsReadOnly();

            // loading RecoveryTime2
            (var recoverytime2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffStatValues2
            (var tempbuffstatvalues2Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var buffstatvalues2Loading = tempbuffstatvalues2Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FlasksDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Name = nameLoading,
                Group = groupLoading,
                LifePerUse = lifeperuseLoading,
                ManaPerUse = manaperuseLoading,
                RecoveryTime = recoverytimeLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                BuffStatValues = buffstatvaluesLoading,
                RecoveryTime2 = recoverytime2Loading,
                BuffStatValues2 = buffstatvalues2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
