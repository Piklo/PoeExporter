using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HellscapeModificationInventoryLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class HellscapeModificationInventoryLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HellscapeModificationInventoryLayoutDat> Items { get; }

    private Dictionary<string, List<HellscapeModificationInventoryLayoutDat>>? byId;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byColumn;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byRow;
    private Dictionary<bool, List<HellscapeModificationInventoryLayoutDat>>? byIsMapSlot;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byUnknown17;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byWidth;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byHeight;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byStat;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byStatValue;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byUnlockedWith;
    private Dictionary<int, List<HellscapeModificationInventoryLayoutDat>>? byQuest;

    /// <summary>
    /// Initializes a new instance of the <see cref="HellscapeModificationInventoryLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HellscapeModificationInventoryLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out HellscapeModificationInventoryLayoutDat? item)
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
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
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HellscapeModificationInventoryLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<string, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Column"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColumn(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColumn(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Column"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColumn(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byColumn is null)
        {
            byColumn = new();
            foreach (var item in Items)
            {
                var itemKey = item.Column;

                if (!byColumn.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColumn.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColumn.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byColumn"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByColumn(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColumn(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Row"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRow(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRow(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Row"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRow(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byRow is null)
        {
            byRow = new();
            foreach (var item in Items)
            {
                var itemKey = item.Row;

                if (!byRow.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRow.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRow.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byRow"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByRow(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRow(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.IsMapSlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsMapSlot(bool? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsMapSlot(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.IsMapSlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsMapSlot(bool? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byIsMapSlot is null)
        {
            byIsMapSlot = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsMapSlot;

                if (!byIsMapSlot.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsMapSlot.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsMapSlot.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byIsMapSlot"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, HellscapeModificationInventoryLayoutDat>> GetManyToManyByIsMapSlot(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<bool, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsMapSlot(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown17(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown17(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Unknown17"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown17(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byUnknown17 is null)
        {
            byUnknown17 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown17;

                if (!byUnknown17.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown17.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown17.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byUnknown17"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByUnknown17(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown17(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWidth(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWidth(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWidth(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byWidth is null)
        {
            byWidth = new();
            foreach (var item in Items)
            {
                var itemKey = item.Width;

                if (!byWidth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWidth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWidth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byWidth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByWidth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWidth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeight(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeight(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeight(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byHeight is null)
        {
            byHeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.Height;

                if (!byHeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byHeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByHeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Stat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Stat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byStat is null)
        {
            byStat = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStat.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStat.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byStat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByStat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.StatValue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValue(int? key, out HellscapeModificationInventoryLayoutDat? item)
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.StatValue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValue(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
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
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byStatValue"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByStatValue(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValue(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.UnlockedWith"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnlockedWith(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnlockedWith(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.UnlockedWith"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnlockedWith(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byUnlockedWith is null)
        {
            byUnlockedWith = new();
            foreach (var item in Items)
            {
                var itemKey = item.UnlockedWith;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnlockedWith.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnlockedWith.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnlockedWith.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byUnlockedWith"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByUnlockedWith(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnlockedWith(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Quest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByQuest(int? key, out HellscapeModificationInventoryLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByQuest(key, out var items))
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
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.Quest"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByQuest(int? key, out IReadOnlyList<HellscapeModificationInventoryLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        if (byQuest is null)
        {
            byQuest = new();
            foreach (var item in Items)
            {
                var itemKey = item.Quest;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byQuest.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byQuest.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byQuest.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HellscapeModificationInventoryLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HellscapeModificationInventoryLayoutDat"/> with <see cref="HellscapeModificationInventoryLayoutDat.byQuest"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HellscapeModificationInventoryLayoutDat>> GetManyToManyByQuest(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();
        }

        var items = new List<ResultItem<int, HellscapeModificationInventoryLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByQuest(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HellscapeModificationInventoryLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HellscapeModificationInventoryLayoutDat[] Load()
    {
        const string filePath = "Data/HellscapeModificationInventoryLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeModificationInventoryLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Column
            (var columnLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Row
            (var rowLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMapSlot
            (var ismapslotLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat
            (var statLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnlockedWith
            (var unlockedwithLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Quest
            (var questLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeModificationInventoryLayoutDat()
            {
                Id = idLoading,
                Column = columnLoading,
                Row = rowLoading,
                IsMapSlot = ismapslotLoading,
                Unknown17 = unknown17Loading,
                Width = widthLoading,
                Height = heightLoading,
                Stat = statLoading,
                StatValue = statvalueLoading,
                UnlockedWith = unlockedwithLoading,
                Quest = questLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
