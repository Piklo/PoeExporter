using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WindowCursorsDat"/> related data and helper methods.
/// </summary>
public sealed class WindowCursorsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WindowCursorsDat> Items { get; }

    private Dictionary<string, List<WindowCursorsDat>>? byId;
    private Dictionary<string, List<WindowCursorsDat>>? byCursorDefault;
    private Dictionary<string, List<WindowCursorsDat>>? byCursorClick;
    private Dictionary<int, List<WindowCursorsDat>>? byUnknown24;
    private Dictionary<int, List<WindowCursorsDat>>? byUnknown28;
    private Dictionary<string, List<WindowCursorsDat>>? byCursorHover;
    private Dictionary<string, List<WindowCursorsDat>>? byDescription;
    private Dictionary<bool, List<WindowCursorsDat>>? byIsEnabled;
    private Dictionary<int, List<WindowCursorsDat>>? byUnknown49;

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowCursorsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WindowCursorsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out WindowCursorsDat? item)
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
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
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WindowCursorsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<string, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.CursorDefault"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCursorDefault(string? key, out WindowCursorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCursorDefault(key, out var items))
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.CursorDefault"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCursorDefault(string? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        if (byCursorDefault is null)
        {
            byCursorDefault = new();
            foreach (var item in Items)
            {
                var itemKey = item.CursorDefault;

                if (!byCursorDefault.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCursorDefault.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCursorDefault.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byCursorDefault"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WindowCursorsDat>> GetManyToManyByCursorDefault(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<string, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCursorDefault(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.CursorClick"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCursorClick(string? key, out WindowCursorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCursorClick(key, out var items))
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.CursorClick"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCursorClick(string? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        if (byCursorClick is null)
        {
            byCursorClick = new();
            foreach (var item in Items)
            {
                var itemKey = item.CursorClick;

                if (!byCursorClick.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCursorClick.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCursorClick.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byCursorClick"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WindowCursorsDat>> GetManyToManyByCursorClick(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<string, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCursorClick(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out WindowCursorsDat? item)
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
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
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WindowCursorsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<int, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(int? key, out WindowCursorsDat? item)
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(int? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
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
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WindowCursorsDat>> GetManyToManyByUnknown28(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<int, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.CursorHover"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCursorHover(string? key, out WindowCursorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCursorHover(key, out var items))
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.CursorHover"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCursorHover(string? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        if (byCursorHover is null)
        {
            byCursorHover = new();
            foreach (var item in Items)
            {
                var itemKey = item.CursorHover;

                if (!byCursorHover.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCursorHover.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCursorHover.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byCursorHover"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WindowCursorsDat>> GetManyToManyByCursorHover(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<string, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCursorHover(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out WindowCursorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WindowCursorsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<string, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsEnabled(bool? key, out WindowCursorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsEnabled(key, out var items))
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.IsEnabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsEnabled(bool? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        if (byIsEnabled is null)
        {
            byIsEnabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsEnabled;

                if (!byIsEnabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsEnabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsEnabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byIsEnabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WindowCursorsDat>> GetManyToManyByIsEnabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<bool, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsEnabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown49(int? key, out WindowCursorsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown49(key, out var items))
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
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.Unknown49"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown49(int? key, out IReadOnlyList<WindowCursorsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        if (byUnknown49 is null)
        {
            byUnknown49 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown49;

                if (!byUnknown49.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown49.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown49.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WindowCursorsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WindowCursorsDat"/> with <see cref="WindowCursorsDat.byUnknown49"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WindowCursorsDat>> GetManyToManyByUnknown49(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WindowCursorsDat>>();
        }

        var items = new List<ResultItem<int, WindowCursorsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown49(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WindowCursorsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WindowCursorsDat[] Load()
    {
        const string filePath = "Data/WindowCursors.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WindowCursorsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CursorDefault
            (var cursordefaultLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CursorClick
            (var cursorclickLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CursorHover
            (var cursorhoverLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WindowCursorsDat()
            {
                Id = idLoading,
                CursorDefault = cursordefaultLoading,
                CursorClick = cursorclickLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                CursorHover = cursorhoverLoading,
                Description = descriptionLoading,
                IsEnabled = isenabledLoading,
                Unknown49 = unknown49Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
