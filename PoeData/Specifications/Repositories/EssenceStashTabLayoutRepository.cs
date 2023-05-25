using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="EssenceStashTabLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class EssenceStashTabLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<EssenceStashTabLayoutDat> Items { get; }

    private Dictionary<string, List<EssenceStashTabLayoutDat>>? byId;
    private Dictionary<int, List<EssenceStashTabLayoutDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<EssenceStashTabLayoutDat>>? byX;
    private Dictionary<int, List<EssenceStashTabLayoutDat>>? byY;
    private Dictionary<int, List<EssenceStashTabLayoutDat>>? byIntId;
    private Dictionary<int, List<EssenceStashTabLayoutDat>>? bySlotWidth;
    private Dictionary<int, List<EssenceStashTabLayoutDat>>? bySlotHeight;
    private Dictionary<bool, List<EssenceStashTabLayoutDat>>? byIsUpgradableEssenceSlot;

    /// <summary>
    /// Initializes a new instance of the <see cref="EssenceStashTabLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal EssenceStashTabLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out EssenceStashTabLayoutDat? item)
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
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
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, EssenceStashTabLayoutDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<string, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out EssenceStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        if (byBaseItemTypesKey is null)
        {
            byBaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceStashTabLayoutDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.X"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByX(int? key, out EssenceStashTabLayoutDat? item)
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.X"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByX(int? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
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
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.byX"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceStashTabLayoutDat>> GetManyToManyByX(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByX(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.Y"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByY(int? key, out EssenceStashTabLayoutDat? item)
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.Y"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByY(int? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
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
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.byY"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceStashTabLayoutDat>> GetManyToManyByY(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByY(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIntId(int? key, out EssenceStashTabLayoutDat? item)
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.IntId"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIntId(int? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
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
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.byIntId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceStashTabLayoutDat>> GetManyToManyByIntId(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIntId(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.SlotWidth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySlotWidth(int? key, out EssenceStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySlotWidth(key, out var items))
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.SlotWidth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySlotWidth(int? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        if (bySlotWidth is null)
        {
            bySlotWidth = new();
            foreach (var item in Items)
            {
                var itemKey = item.SlotWidth;

                if (!bySlotWidth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySlotWidth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySlotWidth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.bySlotWidth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceStashTabLayoutDat>> GetManyToManyBySlotWidth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySlotWidth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.SlotHeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySlotHeight(int? key, out EssenceStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySlotHeight(key, out var items))
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.SlotHeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySlotHeight(int? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        if (bySlotHeight is null)
        {
            bySlotHeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SlotHeight;

                if (!bySlotHeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySlotHeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySlotHeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.bySlotHeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, EssenceStashTabLayoutDat>> GetManyToManyBySlotHeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<int, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySlotHeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.IsUpgradableEssenceSlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsUpgradableEssenceSlot(bool? key, out EssenceStashTabLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsUpgradableEssenceSlot(key, out var items))
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
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.IsUpgradableEssenceSlot"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsUpgradableEssenceSlot(bool? key, out IReadOnlyList<EssenceStashTabLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        if (byIsUpgradableEssenceSlot is null)
        {
            byIsUpgradableEssenceSlot = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsUpgradableEssenceSlot;

                if (!byIsUpgradableEssenceSlot.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsUpgradableEssenceSlot.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsUpgradableEssenceSlot.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<EssenceStashTabLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="EssenceStashTabLayoutDat"/> with <see cref="EssenceStashTabLayoutDat.byIsUpgradableEssenceSlot"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, EssenceStashTabLayoutDat>> GetManyToManyByIsUpgradableEssenceSlot(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, EssenceStashTabLayoutDat>>();
        }

        var items = new List<ResultItem<bool, EssenceStashTabLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsUpgradableEssenceSlot(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, EssenceStashTabLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private EssenceStashTabLayoutDat[] Load()
    {
        const string filePath = "Data/EssenceStashTabLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EssenceStashTabLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading X
            (var xLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Y
            (var yLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SlotWidth
            (var slotwidthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SlotHeight
            (var slotheightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsUpgradableEssenceSlot
            (var isupgradableessenceslotLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EssenceStashTabLayoutDat()
            {
                Id = idLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                X = xLoading,
                Y = yLoading,
                IntId = intidLoading,
                SlotWidth = slotwidthLoading,
                SlotHeight = slotheightLoading,
                IsUpgradableEssenceSlot = isupgradableessenceslotLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
