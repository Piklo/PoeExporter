﻿using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="RitualBalancePerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class RitualBalancePerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<RitualBalancePerLevelDat> Items { get; }

    private Dictionary<int, List<RitualBalancePerLevelDat>>? byMinLevel;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown4;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown8;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown12;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown16;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown20;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown24;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown28;
    private Dictionary<float, List<RitualBalancePerLevelDat>>? byUnknown32;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown36;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown40;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown44;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown48;
    private Dictionary<int, List<RitualBalancePerLevelDat>>? byUnknown52;

    /// <summary>
    /// Initializes a new instance of the <see cref="RitualBalancePerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal RitualBalancePerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out RitualBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown4(key, out var items))
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;

                if (!byUnknown4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out RitualBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(float? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(float? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, RitualBalancePerLevelDat>> GetManyToManyByUnknown32(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out RitualBalancePerLevelDat? item)
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
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<RitualBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<RitualBalancePerLevelDat>();
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
            items = Array.Empty<RitualBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="RitualBalancePerLevelDat"/> with <see cref="RitualBalancePerLevelDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, RitualBalancePerLevelDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, RitualBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, RitualBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, RitualBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private RitualBalancePerLevelDat[] Load()
    {
        const string filePath = "Data/RitualBalancePerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RitualBalancePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

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

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RitualBalancePerLevelDat()
            {
                MinLevel = minlevelLoading,
                Unknown4 = unknown4Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
