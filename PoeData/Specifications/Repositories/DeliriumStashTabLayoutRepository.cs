using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DeliriumStashTabLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class DeliriumStashTabLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DeliriumStashTabLayoutDat> Items { get; }

    private Dictionary<string, List<DeliriumStashTabLayoutDat>>? byId;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byBaseItemType;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byX;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byY;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byIntId;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byWidth;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byHeight;
    private Dictionary<int, List<DeliriumStashTabLayoutDat>>? byUnknown44;
    private Dictionary<bool, List<DeliriumStashTabLayoutDat>>? byUnknown48;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeliriumStashTabLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DeliriumStashTabLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out DeliriumStashTabLayoutDat? item)
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
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
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, DeliriumStashTabLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<string, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemType(int? key, out DeliriumStashTabLayoutDat? item)
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.BaseItemType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemType(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
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
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byBaseItemType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByBaseItemType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.X"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByX(int? key, out DeliriumStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByX(key, out var items))
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.X"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByX(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        if (byX is null)
        {
            byX = new();
            foreach (var item in Items)
            {
                var itemKey = item.X;

                if (!byX.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byX.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byX.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Y"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByY(int? key, out DeliriumStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByY(key, out var items))
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Y"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByY(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        if (byY is null)
        {
            byY = new();
            foreach (var item in Items)
            {
                var itemKey = item.Y;

                if (!byY.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byY.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byY.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntId(int? key, out DeliriumStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIntId(key, out var items))
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntId(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        if (byIntId is null)
        {
            byIntId = new();
            foreach (var item in Items)
            {
                var itemKey = item.IntId;

                if (!byIntId.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIntId.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIntId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byIntId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByIntId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWidth(int? key, out DeliriumStashTabLayoutDat? item)
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Width"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWidth(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
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
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byWidth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByWidth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWidth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeight(int? key, out DeliriumStashTabLayoutDat? item)
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Height"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeight(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
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
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byHeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByHeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out DeliriumStashTabLayoutDat? item)
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
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
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DeliriumStashTabLayoutDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(bool? key, out DeliriumStashTabLayoutDat? item)
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
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(bool? key, out IReadOnlyList<DeliriumStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DeliriumStashTabLayoutDat>();
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
            items = Array.Empty<DeliriumStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DeliriumStashTabLayoutDat"/> with <see cref="DeliriumStashTabLayoutDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, DeliriumStashTabLayoutDat>> GetManyToManyByUnknown48(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, DeliriumStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<bool, DeliriumStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, DeliriumStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DeliriumStashTabLayoutDat[] Load()
    {
        const string filePath = "Data/DeliriumStashTabLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DeliriumStashTabLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading X
            (var xLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Y
            (var yLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DeliriumStashTabLayoutDat()
            {
                Id = idLoading,
                BaseItemType = baseitemtypeLoading,
                X = xLoading,
                Y = yLoading,
                IntId = intidLoading,
                Width = widthLoading,
                Height = heightLoading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
