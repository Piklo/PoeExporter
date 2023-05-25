using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="NearbyMonsterConditionsDat"/> related data and helper methods.
/// </summary>
public sealed class NearbyMonsterConditionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<NearbyMonsterConditionsDat> Items { get; }

    private Dictionary<string, List<NearbyMonsterConditionsDat>>? byId;
    private Dictionary<int, List<NearbyMonsterConditionsDat>>? byMonsterVarietiesKeys;
    private Dictionary<int, List<NearbyMonsterConditionsDat>>? byMonsterAmount;
    private Dictionary<int, List<NearbyMonsterConditionsDat>>? byUnknown28;
    private Dictionary<bool, List<NearbyMonsterConditionsDat>>? byIsNegated;
    private Dictionary<int, List<NearbyMonsterConditionsDat>>? byUnknown33;
    private Dictionary<int, List<NearbyMonsterConditionsDat>>? byUnknown37;
    private Dictionary<bool, List<NearbyMonsterConditionsDat>>? byIsLessThen;
    private Dictionary<int, List<NearbyMonsterConditionsDat>>? byMinimumHealthPercentage;

    /// <summary>
    /// Initializes a new instance of the <see cref="NearbyMonsterConditionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal NearbyMonsterConditionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out NearbyMonsterConditionsDat? item)
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
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
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, NearbyMonsterConditionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<string, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKeys(int? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKeys(int? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byMonsterVarietiesKeys is null)
        {
            byMonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byMonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NearbyMonsterConditionsDat>> GetManyToManyByMonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<int, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.MonsterAmount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterAmount(int? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterAmount(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.MonsterAmount"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterAmount(int? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byMonsterAmount is null)
        {
            byMonsterAmount = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterAmount;

                if (!byMonsterAmount.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterAmount.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterAmount.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byMonsterAmount"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NearbyMonsterConditionsDat>> GetManyToManyByMonsterAmount(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<int, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterAmount(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out NearbyMonsterConditionsDat? item)
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
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
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NearbyMonsterConditionsDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<int, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.IsNegated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsNegated(bool? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsNegated(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.IsNegated"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsNegated(bool? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byIsNegated is null)
        {
            byIsNegated = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsNegated;

                if (!byIsNegated.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsNegated.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsNegated.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byIsNegated"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NearbyMonsterConditionsDat>> GetManyToManyByIsNegated(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<bool, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsNegated(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown33(int? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown33(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown33(int? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byUnknown33 is null)
        {
            byUnknown33 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown33;

                if (!byUnknown33.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown33.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown33.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byUnknown33"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NearbyMonsterConditionsDat>> GetManyToManyByUnknown33(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<int, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown33(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown37(int? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown37(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown37(int? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byUnknown37 is null)
        {
            byUnknown37 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown37;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown37.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown37.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown37.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byUnknown37"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NearbyMonsterConditionsDat>> GetManyToManyByUnknown37(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<int, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown37(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.IsLessThen"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLessThen(bool? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLessThen(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.IsLessThen"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLessThen(bool? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byIsLessThen is null)
        {
            byIsLessThen = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLessThen;

                if (!byIsLessThen.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLessThen.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLessThen.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byIsLessThen"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, NearbyMonsterConditionsDat>> GetManyToManyByIsLessThen(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<bool, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLessThen(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.MinimumHealthPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinimumHealthPercentage(int? key, out NearbyMonsterConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinimumHealthPercentage(key, out var items))
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
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.MinimumHealthPercentage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinimumHealthPercentage(int? key, out IReadOnlyList<NearbyMonsterConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        if (byMinimumHealthPercentage is null)
        {
            byMinimumHealthPercentage = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinimumHealthPercentage;

                if (!byMinimumHealthPercentage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinimumHealthPercentage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinimumHealthPercentage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<NearbyMonsterConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="NearbyMonsterConditionsDat"/> with <see cref="NearbyMonsterConditionsDat.byMinimumHealthPercentage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, NearbyMonsterConditionsDat>> GetManyToManyByMinimumHealthPercentage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, NearbyMonsterConditionsDat>>();
        }

        var items = new List<ResultItem<int, NearbyMonsterConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinimumHealthPercentage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, NearbyMonsterConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private NearbyMonsterConditionsDat[] Load()
    {
        const string filePath = "Data/NearbyMonsterConditions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NearbyMonsterConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKeys
            (var tempmonstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeysLoading = tempmonstervarietieskeysLoading.AsReadOnly();

            // loading MonsterAmount
            (var monsteramountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsNegated
            (var isnegatedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var tempunknown37Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown37Loading = tempunknown37Loading.AsReadOnly();

            // loading IsLessThen
            (var islessthenLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinimumHealthPercentage
            (var minimumhealthpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NearbyMonsterConditionsDat()
            {
                Id = idLoading,
                MonsterVarietiesKeys = monstervarietieskeysLoading,
                MonsterAmount = monsteramountLoading,
                Unknown28 = unknown28Loading,
                IsNegated = isnegatedLoading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
                IsLessThen = islessthenLoading,
                MinimumHealthPercentage = minimumhealthpercentageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
