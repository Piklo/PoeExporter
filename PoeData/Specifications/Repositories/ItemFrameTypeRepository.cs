using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemFrameTypeDat"/> related data and helper methods.
/// </summary>
public sealed class ItemFrameTypeRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemFrameTypeDat> Items { get; }

    private Dictionary<string, List<ItemFrameTypeDat>>? byId;
    private Dictionary<bool, List<ItemFrameTypeDat>>? byUnknown8;
    private Dictionary<bool, List<ItemFrameTypeDat>>? byDoubleLine;
    private Dictionary<string, List<ItemFrameTypeDat>>? byHeaderSingle;
    private Dictionary<string, List<ItemFrameTypeDat>>? byHeaderDouble;
    private Dictionary<string, List<ItemFrameTypeDat>>? byHardmodeHeaderSingle;
    private Dictionary<string, List<ItemFrameTypeDat>>? byHardmodeHeaderDouble;
    private Dictionary<int, List<ItemFrameTypeDat>>? byColor;
    private Dictionary<string, List<ItemFrameTypeDat>>? bySeparator;
    private Dictionary<bool, List<ItemFrameTypeDat>>? byUnknown66;
    private Dictionary<int, List<ItemFrameTypeDat>>? byRarity;
    private Dictionary<int, List<ItemFrameTypeDat>>? byDisplayString;
    private Dictionary<string, List<ItemFrameTypeDat>>? byColorMarkup;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemFrameTypeRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemFrameTypeRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ItemFrameTypeDat? item)
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
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
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(bool? key, out ItemFrameTypeDat? item)
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(bool? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
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
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemFrameTypeDat>> GetManyToManyByUnknown8(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<bool, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.DoubleLine"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDoubleLine(bool? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDoubleLine(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.DoubleLine"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDoubleLine(bool? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byDoubleLine is null)
        {
            byDoubleLine = new();
            foreach (var item in Items)
            {
                var itemKey = item.DoubleLine;

                if (!byDoubleLine.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDoubleLine.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDoubleLine.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byDoubleLine"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemFrameTypeDat>> GetManyToManyByDoubleLine(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<bool, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDoubleLine(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HeaderSingle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeaderSingle(string? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeaderSingle(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HeaderSingle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeaderSingle(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byHeaderSingle is null)
        {
            byHeaderSingle = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeaderSingle;

                if (!byHeaderSingle.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeaderSingle.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeaderSingle.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byHeaderSingle"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyByHeaderSingle(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeaderSingle(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HeaderDouble"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeaderDouble(string? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeaderDouble(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HeaderDouble"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeaderDouble(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byHeaderDouble is null)
        {
            byHeaderDouble = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeaderDouble;

                if (!byHeaderDouble.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeaderDouble.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeaderDouble.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byHeaderDouble"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyByHeaderDouble(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeaderDouble(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HardmodeHeaderSingle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHardmodeHeaderSingle(string? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHardmodeHeaderSingle(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HardmodeHeaderSingle"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHardmodeHeaderSingle(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byHardmodeHeaderSingle is null)
        {
            byHardmodeHeaderSingle = new();
            foreach (var item in Items)
            {
                var itemKey = item.HardmodeHeaderSingle;

                if (!byHardmodeHeaderSingle.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHardmodeHeaderSingle.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHardmodeHeaderSingle.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byHardmodeHeaderSingle"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyByHardmodeHeaderSingle(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHardmodeHeaderSingle(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HardmodeHeaderDouble"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHardmodeHeaderDouble(string? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHardmodeHeaderDouble(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.HardmodeHeaderDouble"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHardmodeHeaderDouble(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byHardmodeHeaderDouble is null)
        {
            byHardmodeHeaderDouble = new();
            foreach (var item in Items)
            {
                var itemKey = item.HardmodeHeaderDouble;

                if (!byHardmodeHeaderDouble.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHardmodeHeaderDouble.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHardmodeHeaderDouble.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byHardmodeHeaderDouble"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyByHardmodeHeaderDouble(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHardmodeHeaderDouble(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Color"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColor(int? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColor(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Color"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColor(int? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byColor is null)
        {
            byColor = new();
            foreach (var item in Items)
            {
                var itemKey = item.Color;
                foreach (var listKey in itemKey)
                {
                    if (!byColor.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byColor.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byColor.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byColor"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemFrameTypeDat>> GetManyToManyByColor(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<int, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColor(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Separator"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySeparator(string? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySeparator(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Separator"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySeparator(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (bySeparator is null)
        {
            bySeparator = new();
            foreach (var item in Items)
            {
                var itemKey = item.Separator;

                if (!bySeparator.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySeparator.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySeparator.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.bySeparator"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyBySeparator(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySeparator(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(bool? key, out ItemFrameTypeDat? item)
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(bool? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ItemFrameTypeDat>> GetManyToManyByUnknown66(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<bool, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Rarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRarity(int? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRarity(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.Rarity"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRarity(int? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byRarity is null)
        {
            byRarity = new();
            foreach (var item in Items)
            {
                var itemKey = item.Rarity;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRarity.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRarity.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRarity.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byRarity"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemFrameTypeDat>> GetManyToManyByRarity(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<int, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRarity(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.DisplayString"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDisplayString(int? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDisplayString(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.DisplayString"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDisplayString(int? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byDisplayString is null)
        {
            byDisplayString = new();
            foreach (var item in Items)
            {
                var itemKey = item.DisplayString;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDisplayString.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDisplayString.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDisplayString.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byDisplayString"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemFrameTypeDat>> GetManyToManyByDisplayString(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<int, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDisplayString(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.ColorMarkup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByColorMarkup(string? key, out ItemFrameTypeDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByColorMarkup(key, out var items))
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
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.ColorMarkup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByColorMarkup(string? key, out IReadOnlyList<ItemFrameTypeDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        if (byColorMarkup is null)
        {
            byColorMarkup = new();
            foreach (var item in Items)
            {
                var itemKey = item.ColorMarkup;

                if (!byColorMarkup.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byColorMarkup.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byColorMarkup.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemFrameTypeDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemFrameTypeDat"/> with <see cref="ItemFrameTypeDat.byColorMarkup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemFrameTypeDat>> GetManyToManyByColorMarkup(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemFrameTypeDat>>();
        }

        var items = new List<ResultItem<string, ItemFrameTypeDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByColorMarkup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemFrameTypeDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemFrameTypeDat[] Load()
    {
        const string filePath = "Data/ItemFrameType.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemFrameTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DoubleLine
            (var doublelineLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HeaderSingle
            (var headersingleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeaderDouble
            (var headerdoubleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HardmodeHeaderSingle
            (var hardmodeheadersingleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HardmodeHeaderDouble
            (var hardmodeheaderdoubleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Color
            (var tempcolorLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var colorLoading = tempcolorLoading.AsReadOnly();

            // loading Separator
            (var separatorLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DisplayString
            (var displaystringLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ColorMarkup
            (var colormarkupLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemFrameTypeDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                DoubleLine = doublelineLoading,
                HeaderSingle = headersingleLoading,
                HeaderDouble = headerdoubleLoading,
                HardmodeHeaderSingle = hardmodeheadersingleLoading,
                HardmodeHeaderDouble = hardmodeheaderdoubleLoading,
                Color = colorLoading,
                Separator = separatorLoading,
                Unknown66 = unknown66Loading,
                Rarity = rarityLoading,
                DisplayString = displaystringLoading,
                ColorMarkup = colormarkupLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
