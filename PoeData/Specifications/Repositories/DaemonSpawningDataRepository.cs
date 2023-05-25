using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DaemonSpawningDataDat"/> related data and helper methods.
/// </summary>
public sealed class DaemonSpawningDataRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DaemonSpawningDataDat> Items { get; }

    private Dictionary<string, List<DaemonSpawningDataDat>>? byId;
    private Dictionary<int, List<DaemonSpawningDataDat>>? byMonsterVarieties;
    private Dictionary<int, List<DaemonSpawningDataDat>>? byUnknown24;
    private Dictionary<bool, List<DaemonSpawningDataDat>>? byUnknown28;
    private Dictionary<int, List<DaemonSpawningDataDat>>? byUnknown29;
    private Dictionary<int, List<DaemonSpawningDataDat>>? byUnknown33;
    private Dictionary<bool, List<DaemonSpawningDataDat>>? byUnknown37;
    private Dictionary<bool, List<DaemonSpawningDataDat>>? byUnknown38;
    private Dictionary<bool, List<DaemonSpawningDataDat>>? byUnknown39;

    /// <summary>
    /// Initializes a new instance of the <see cref="DaemonSpawningDataRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DaemonSpawningDataRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out DaemonSpawningDataDat? item)
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
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
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DaemonSpawningDataDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<string, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.MonsterVarieties"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarieties(int? key, out DaemonSpawningDataDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarieties(key, out var items))
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.MonsterVarieties"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarieties(int? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        if (byMonsterVarieties is null)
        {
            byMonsterVarieties = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarieties;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterVarieties.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterVarieties.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterVarieties.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byMonsterVarieties"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DaemonSpawningDataDat>> GetManyToManyByMonsterVarieties(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<int, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarieties(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out DaemonSpawningDataDat? item)
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
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
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DaemonSpawningDataDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<int, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(bool? key, out DaemonSpawningDataDat? item)
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(bool? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
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
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DaemonSpawningDataDat>> GetManyToManyByUnknown28(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<bool, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(int? key, out DaemonSpawningDataDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown29(key, out var items))
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(int? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;

                if (!byUnknown29.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown29.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DaemonSpawningDataDat>> GetManyToManyByUnknown29(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<int, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown33(int? key, out DaemonSpawningDataDat? item)
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown33(int? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
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
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown33"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DaemonSpawningDataDat>> GetManyToManyByUnknown33(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<int, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown33(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown37(bool? key, out DaemonSpawningDataDat? item)
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown37"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown37(bool? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        if (byUnknown37 is null)
        {
            byUnknown37 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown37;

                if (!byUnknown37.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown37.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown37.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown37"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DaemonSpawningDataDat>> GetManyToManyByUnknown37(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<bool, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown37(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown38"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown38(bool? key, out DaemonSpawningDataDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown38(key, out var items))
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown38"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown38(bool? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        if (byUnknown38 is null)
        {
            byUnknown38 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown38;

                if (!byUnknown38.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown38.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown38.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown38"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DaemonSpawningDataDat>> GetManyToManyByUnknown38(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<bool, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown38(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown39"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown39(bool? key, out DaemonSpawningDataDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown39(key, out var items))
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
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.Unknown39"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown39(bool? key, out IReadOnlyList<DaemonSpawningDataDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        if (byUnknown39 is null)
        {
            byUnknown39 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown39;

                if (!byUnknown39.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown39.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown39.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DaemonSpawningDataDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DaemonSpawningDataDat"/> with <see cref="DaemonSpawningDataDat.byUnknown39"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DaemonSpawningDataDat>> GetManyToManyByUnknown39(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DaemonSpawningDataDat>>();
        }

        var items = new List<ResultItem<bool, DaemonSpawningDataDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown39(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DaemonSpawningDataDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DaemonSpawningDataDat[] Load()
    {
        const string filePath = "Data/DaemonSpawningData.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DaemonSpawningDataDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarieties
            (var tempmonstervarietiesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietiesLoading = tempmonstervarietiesLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown38
            (var unknown38Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown39
            (var unknown39Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DaemonSpawningDataDat()
            {
                Id = idLoading,
                MonsterVarieties = monstervarietiesLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown29 = unknown29Loading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
                Unknown38 = unknown38Loading,
                Unknown39 = unknown39Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
