using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="FragmentStashTabLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class FragmentStashTabLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<FragmentStashTabLayoutDat> Items { get; }

    private Dictionary<string, List<FragmentStashTabLayoutDat>>? byId;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? byPosX;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? byPosY;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? byOrder;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? bySizeX;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? bySizeY;
    private Dictionary<bool, List<FragmentStashTabLayoutDat>>? byUnknown28;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? byTab;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? byUnknown33;
    private Dictionary<bool, List<FragmentStashTabLayoutDat>>? byIsDisabled;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? bySubtab;
    private Dictionary<int, List<FragmentStashTabLayoutDat>>? byFragmentItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="FragmentStashTabLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal FragmentStashTabLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out FragmentStashTabLayoutDat? item)
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
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
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, FragmentStashTabLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<string, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.PosX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPosX(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPosX(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.PosX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPosX(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (byPosX is null)
        {
            byPosX = new();
            foreach (var item in Items)
            {
                var itemKey = item.PosX;

                if (!byPosX.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPosX.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPosX.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byPosX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyByPosX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPosX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.PosY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPosY(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPosY(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.PosY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPosY(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (byPosY is null)
        {
            byPosY = new();
            foreach (var item in Items)
            {
                var itemKey = item.PosY;

                if (!byPosY.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPosY.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPosY.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byPosY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyByPosY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPosY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Order"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOrder(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOrder(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Order"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOrder(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (byOrder is null)
        {
            byOrder = new();
            foreach (var item in Items)
            {
                var itemKey = item.Order;

                if (!byOrder.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOrder.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOrder.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byOrder"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyByOrder(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOrder(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.SizeX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySizeX(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySizeX(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.SizeX"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySizeX(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (bySizeX is null)
        {
            bySizeX = new();
            foreach (var item in Items)
            {
                var itemKey = item.SizeX;

                if (!bySizeX.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySizeX.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySizeX.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.bySizeX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyBySizeX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySizeX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.SizeY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySizeY(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySizeY(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.SizeY"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySizeY(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (bySizeY is null)
        {
            bySizeY = new();
            foreach (var item in Items)
            {
                var itemKey = item.SizeY;

                if (!bySizeY.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySizeY.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySizeY.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.bySizeY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyBySizeY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySizeY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(bool? key, out FragmentStashTabLayoutDat? item)
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(bool? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
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
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, FragmentStashTabLayoutDat>> GetManyToManyByUnknown28(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<bool, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Tab"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTab(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTab(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Tab"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTab(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (byTab is null)
        {
            byTab = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tab;

                if (!byTab.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTab.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTab.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byTab"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyByTab(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTab(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown33(int? key, out FragmentStashTabLayoutDat? item)
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown33(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
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
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byUnknown33"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyByUnknown33(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown33(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsDisabled(bool? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsDisabled(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.IsDisabled"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsDisabled(bool? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (byIsDisabled is null)
        {
            byIsDisabled = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsDisabled;

                if (!byIsDisabled.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsDisabled.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsDisabled.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byIsDisabled"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, FragmentStashTabLayoutDat>> GetManyToManyByIsDisabled(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<bool, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsDisabled(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Subtab"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySubtab(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySubtab(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.Subtab"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySubtab(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (bySubtab is null)
        {
            bySubtab = new();
            foreach (var item in Items)
            {
                var itemKey = item.Subtab;

                if (!bySubtab.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySubtab.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySubtab.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.bySubtab"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyBySubtab(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySubtab(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.FragmentItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFragmentItems(int? key, out FragmentStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFragmentItems(key, out var items))
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
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.FragmentItems"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFragmentItems(int? key, out IReadOnlyList<FragmentStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        if (byFragmentItems is null)
        {
            byFragmentItems = new();
            foreach (var item in Items)
            {
                var itemKey = item.FragmentItems;
                foreach (var listKey in itemKey)
                {
                    if (!byFragmentItems.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFragmentItems.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFragmentItems.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<FragmentStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="FragmentStashTabLayoutDat"/> with <see cref="FragmentStashTabLayoutDat.byFragmentItems"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, FragmentStashTabLayoutDat>> GetManyToManyByFragmentItems(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, FragmentStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, FragmentStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFragmentItems(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, FragmentStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private FragmentStashTabLayoutDat[] Load()
    {
        const string filePath = "Data/FragmentStashTabLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FragmentStashTabLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PosX
            (var posxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PosY
            (var posyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Order
            (var orderLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SizeX
            (var sizexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SizeY
            (var sizeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Tab
            (var tabLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Subtab
            (var subtabLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FragmentItems
            (var tempfragmentitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var fragmentitemsLoading = tempfragmentitemsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FragmentStashTabLayoutDat()
            {
                Id = idLoading,
                PosX = posxLoading,
                PosY = posyLoading,
                Order = orderLoading,
                SizeX = sizexLoading,
                SizeY = sizeyLoading,
                Unknown28 = unknown28Loading,
                Tab = tabLoading,
                Unknown33 = unknown33Loading,
                IsDisabled = isdisabledLoading,
                Subtab = subtabLoading,
                FragmentItems = fragmentitemsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
