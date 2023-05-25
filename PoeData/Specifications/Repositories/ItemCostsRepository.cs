using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemCostsDat"/> related data and helper methods.
/// </summary>
public sealed class ItemCostsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemCostsDat> Items { get; }

    private Dictionary<string, List<ItemCostsDat>>? byId;
    private Dictionary<int, List<ItemCostsDat>>? byCost1Currencies;
    private Dictionary<int, List<ItemCostsDat>>? byCost1Values;
    private Dictionary<int, List<ItemCostsDat>>? byCost2Currencies;
    private Dictionary<int, List<ItemCostsDat>>? byCost2Values;
    private Dictionary<int, List<ItemCostsDat>>? byCost3Currencies;
    private Dictionary<int, List<ItemCostsDat>>? byCost3Values;
    private Dictionary<int, List<ItemCostsDat>>? byCost4Currencies;
    private Dictionary<int, List<ItemCostsDat>>? byCost4Values;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemCostsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemCostsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ItemCostsDat? item)
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
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
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemCostsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemCostsDat>>();
        }

        var items = new List<ResultItem<string, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost1Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost1Currencies(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost1Currencies(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost1Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost1Currencies(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost1Currencies is null)
        {
            byCost1Currencies = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost1Currencies;
                foreach (var listKey in itemKey)
                {
                    if (!byCost1Currencies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost1Currencies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost1Currencies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost1Currencies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost1Currencies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost1Currencies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost1Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost1Values(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost1Values(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost1Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost1Values(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost1Values is null)
        {
            byCost1Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost1Values;
                foreach (var listKey in itemKey)
                {
                    if (!byCost1Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost1Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost1Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost1Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost1Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost1Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost2Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost2Currencies(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost2Currencies(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost2Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost2Currencies(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost2Currencies is null)
        {
            byCost2Currencies = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost2Currencies;
                foreach (var listKey in itemKey)
                {
                    if (!byCost2Currencies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost2Currencies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost2Currencies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost2Currencies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost2Currencies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost2Currencies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost2Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost2Values(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost2Values(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost2Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost2Values(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost2Values is null)
        {
            byCost2Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost2Values;
                foreach (var listKey in itemKey)
                {
                    if (!byCost2Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost2Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost2Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost2Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost2Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost2Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost3Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost3Currencies(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost3Currencies(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost3Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost3Currencies(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost3Currencies is null)
        {
            byCost3Currencies = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost3Currencies;
                foreach (var listKey in itemKey)
                {
                    if (!byCost3Currencies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost3Currencies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost3Currencies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost3Currencies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost3Currencies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost3Currencies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost3Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost3Values(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost3Values(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost3Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost3Values(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost3Values is null)
        {
            byCost3Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost3Values;
                foreach (var listKey in itemKey)
                {
                    if (!byCost3Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost3Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost3Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost3Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost3Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost3Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost4Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost4Currencies(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost4Currencies(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost4Currencies"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost4Currencies(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost4Currencies is null)
        {
            byCost4Currencies = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost4Currencies;
                foreach (var listKey in itemKey)
                {
                    if (!byCost4Currencies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost4Currencies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost4Currencies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost4Currencies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost4Currencies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost4Currencies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost4Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCost4Values(int? key, out ItemCostsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCost4Values(key, out var items))
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
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.Cost4Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCost4Values(int? key, out IReadOnlyList<ItemCostsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        if (byCost4Values is null)
        {
            byCost4Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cost4Values;
                foreach (var listKey in itemKey)
                {
                    if (!byCost4Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCost4Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCost4Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemCostsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemCostsDat"/> with <see cref="ItemCostsDat.byCost4Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemCostsDat>> GetManyToManyByCost4Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemCostsDat>>();
        }

        var items = new List<ResultItem<int, ItemCostsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCost4Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemCostsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemCostsDat[] Load()
    {
        const string filePath = "Data/ItemCosts.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemCostsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Cost1Currencies
            (var tempcost1currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost1currenciesLoading = tempcost1currenciesLoading.AsReadOnly();

            // loading Cost1Values
            (var tempcost1valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost1valuesLoading = tempcost1valuesLoading.AsReadOnly();

            // loading Cost2Currencies
            (var tempcost2currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost2currenciesLoading = tempcost2currenciesLoading.AsReadOnly();

            // loading Cost2Values
            (var tempcost2valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost2valuesLoading = tempcost2valuesLoading.AsReadOnly();

            // loading Cost3Currencies
            (var tempcost3currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost3currenciesLoading = tempcost3currenciesLoading.AsReadOnly();

            // loading Cost3Values
            (var tempcost3valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost3valuesLoading = tempcost3valuesLoading.AsReadOnly();

            // loading Cost4Currencies
            (var tempcost4currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost4currenciesLoading = tempcost4currenciesLoading.AsReadOnly();

            // loading Cost4Values
            (var tempcost4valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost4valuesLoading = tempcost4valuesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemCostsDat()
            {
                Id = idLoading,
                Cost1Currencies = cost1currenciesLoading,
                Cost1Values = cost1valuesLoading,
                Cost2Currencies = cost2currenciesLoading,
                Cost2Values = cost2valuesLoading,
                Cost3Currencies = cost3currenciesLoading,
                Cost3Values = cost3valuesLoading,
                Cost4Currencies = cost4currenciesLoading,
                Cost4Values = cost4valuesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
