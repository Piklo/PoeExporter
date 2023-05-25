using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistStorageLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class HeistStorageLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistStorageLayoutDat> Items { get; }

    private Dictionary<string, List<HeistStorageLayoutDat>>? byId;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byBaseItemType;
    private Dictionary<bool, List<HeistStorageLayoutDat>>? byUnknown24;
    private Dictionary<string, List<HeistStorageLayoutDat>>? byButtonFile;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byUnknown33;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byHeistJobsKey;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byColumns;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byRows;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byUnknown61;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byUnknown65;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byUnknown69;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byUnknown73;
    private Dictionary<int, List<HeistStorageLayoutDat>>? byItemClass;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistStorageLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistStorageLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HeistStorageLayoutDat? item)
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
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
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistStorageLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<string, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemType(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemType(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemType(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byBaseItemType is null)
        {
            byBaseItemType = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemType;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemType.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemType.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byBaseItemType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByBaseItemType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out HeistStorageLayoutDat? item)
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
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
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HeistStorageLayoutDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<bool, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.ButtonFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByButtonFile(string? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByButtonFile(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.ButtonFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByButtonFile(string? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byButtonFile is null)
        {
            byButtonFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ButtonFile;

                if (!byButtonFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byButtonFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byButtonFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byButtonFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistStorageLayoutDat>> GetManyToManyByButtonFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<string, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByButtonFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown33(int? key, out HeistStorageLayoutDat? item)
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown33(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
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
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byUnknown33"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByUnknown33(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown33(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistJobsKey(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistJobsKey(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistJobsKey(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byHeistJobsKey is null)
        {
            byHeistJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byHeistJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByHeistJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Columns"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColumns(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColumns(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Columns"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColumns(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byColumns is null)
        {
            byColumns = new();
            foreach (var item in Items)
            {
                var itemKey = item.Columns;

                if (!byColumns.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColumns.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColumns.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byColumns"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByColumns(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColumns(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Rows"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRows(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRows(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Rows"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRows(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byRows is null)
        {
            byRows = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rows;

                if (!byRows.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRows.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRows.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byRows"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByRows(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRows(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(int? key, out HeistStorageLayoutDat? item)
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
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
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByUnknown61(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(int? key, out HeistStorageLayoutDat? item)
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
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
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByUnknown65(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown69(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown69(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown69(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byUnknown69 is null)
        {
            byUnknown69 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown69;

                if (!byUnknown69.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown69.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown69.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byUnknown69"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByUnknown69(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown69(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown73(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown73(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.Unknown73"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown73(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byUnknown73 is null)
        {
            byUnknown73 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown73;

                if (!byUnknown73.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown73.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown73.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byUnknown73"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByUnknown73(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown73(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClass(int? key, out HeistStorageLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClass(key, out var items))
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
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.ItemClass"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClass(int? key, out IReadOnlyList<HeistStorageLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        if (byItemClass is null)
        {
            byItemClass = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClass;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClass.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClass.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClass.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistStorageLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistStorageLayoutDat"/> with <see cref="HeistStorageLayoutDat.byItemClass"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistStorageLayoutDat>> GetManyToManyByItemClass(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistStorageLayoutDat>>();
        }

        var items = new List<ResultItem<int, HeistStorageLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClass(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistStorageLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistStorageLayoutDat[] Load()
    {
        const string filePath = "Data/HeistStorageLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistStorageLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ButtonFile
            (var buttonfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Columns
            (var columnsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Rows
            (var rowsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistStorageLayoutDat()
            {
                Id = idLoading,
                BaseItemType = baseitemtypeLoading,
                Unknown24 = unknown24Loading,
                ButtonFile = buttonfileLoading,
                Unknown33 = unknown33Loading,
                HeistJobsKey = heistjobskeyLoading,
                Columns = columnsLoading,
                Rows = rowsLoading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown69 = unknown69Loading,
                Unknown73 = unknown73Loading,
                ItemClass = itemclassLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
