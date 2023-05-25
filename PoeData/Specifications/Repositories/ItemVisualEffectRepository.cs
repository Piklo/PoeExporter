using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ItemVisualEffectDat"/> related data and helper methods.
/// </summary>
public sealed class ItemVisualEffectRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ItemVisualEffectDat> Items { get; }

    private Dictionary<string, List<ItemVisualEffectDat>>? byId;
    private Dictionary<string, List<ItemVisualEffectDat>>? byDaggerEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byBowEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byOneHandedMaceEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byOneHandedSwordEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byUnknown40;
    private Dictionary<string, List<ItemVisualEffectDat>>? byTwoHandedSwordEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byTwoHandedStaffEPKFile;
    private Dictionary<int, List<ItemVisualEffectDat>>? byUnknown64;
    private Dictionary<string, List<ItemVisualEffectDat>>? byTwoHandedMaceEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byOneHandedAxeEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byTwoHandedAxeEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byClawEPKFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byPETFile;
    private Dictionary<string, List<ItemVisualEffectDat>>? byShield;

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemVisualEffectRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ItemVisualEffectRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ItemVisualEffectDat? item)
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
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
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.DaggerEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDaggerEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDaggerEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.DaggerEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDaggerEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byDaggerEPKFile is null)
        {
            byDaggerEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.DaggerEPKFile;

                if (!byDaggerEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDaggerEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDaggerEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byDaggerEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByDaggerEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDaggerEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.BowEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBowEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBowEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.BowEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBowEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byBowEPKFile is null)
        {
            byBowEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.BowEPKFile;

                if (!byBowEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBowEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBowEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byBowEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByBowEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBowEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.OneHandedMaceEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandedMaceEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandedMaceEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.OneHandedMaceEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandedMaceEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byOneHandedMaceEPKFile is null)
        {
            byOneHandedMaceEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandedMaceEPKFile;

                if (!byOneHandedMaceEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOneHandedMaceEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandedMaceEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byOneHandedMaceEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByOneHandedMaceEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandedMaceEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.OneHandedSwordEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandedSwordEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandedSwordEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.OneHandedSwordEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandedSwordEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byOneHandedSwordEPKFile is null)
        {
            byOneHandedSwordEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandedSwordEPKFile;

                if (!byOneHandedSwordEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOneHandedSwordEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandedSwordEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byOneHandedSwordEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByOneHandedSwordEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandedSwordEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(string? key, out ItemVisualEffectDat? item)
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
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

        if (!byUnknown40.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByUnknown40(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedSwordEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandedSwordEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandedSwordEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedSwordEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandedSwordEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byTwoHandedSwordEPKFile is null)
        {
            byTwoHandedSwordEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandedSwordEPKFile;

                if (!byTwoHandedSwordEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTwoHandedSwordEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandedSwordEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byTwoHandedSwordEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByTwoHandedSwordEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandedSwordEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedStaffEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandedStaffEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandedStaffEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedStaffEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandedStaffEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byTwoHandedStaffEPKFile is null)
        {
            byTwoHandedStaffEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandedStaffEPKFile;

                if (!byTwoHandedStaffEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTwoHandedStaffEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandedStaffEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byTwoHandedStaffEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByTwoHandedStaffEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandedStaffEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ItemVisualEffectDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<int, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedMaceEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandedMaceEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandedMaceEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedMaceEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandedMaceEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byTwoHandedMaceEPKFile is null)
        {
            byTwoHandedMaceEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandedMaceEPKFile;

                if (!byTwoHandedMaceEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTwoHandedMaceEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandedMaceEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byTwoHandedMaceEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByTwoHandedMaceEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandedMaceEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.OneHandedAxeEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByOneHandedAxeEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByOneHandedAxeEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.OneHandedAxeEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByOneHandedAxeEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byOneHandedAxeEPKFile is null)
        {
            byOneHandedAxeEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.OneHandedAxeEPKFile;

                if (!byOneHandedAxeEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byOneHandedAxeEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byOneHandedAxeEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byOneHandedAxeEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByOneHandedAxeEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByOneHandedAxeEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedAxeEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwoHandedAxeEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwoHandedAxeEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.TwoHandedAxeEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwoHandedAxeEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byTwoHandedAxeEPKFile is null)
        {
            byTwoHandedAxeEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwoHandedAxeEPKFile;

                if (!byTwoHandedAxeEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTwoHandedAxeEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTwoHandedAxeEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byTwoHandedAxeEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByTwoHandedAxeEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwoHandedAxeEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.ClawEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByClawEPKFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByClawEPKFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.ClawEPKFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByClawEPKFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byClawEPKFile is null)
        {
            byClawEPKFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ClawEPKFile;

                if (!byClawEPKFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byClawEPKFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byClawEPKFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byClawEPKFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByClawEPKFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByClawEPKFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.PETFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPETFile(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPETFile(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.PETFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPETFile(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byPETFile is null)
        {
            byPETFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PETFile;

                if (!byPETFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPETFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPETFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byPETFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByPETFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPETFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Shield"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShield(string? key, out ItemVisualEffectDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShield(key, out var items))
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
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.Shield"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShield(string? key, out IReadOnlyList<ItemVisualEffectDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        if (byShield is null)
        {
            byShield = new();
            foreach (var item in Items)
            {
                var itemKey = item.Shield;

                if (!byShield.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShield.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShield.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ItemVisualEffectDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ItemVisualEffectDat"/> with <see cref="ItemVisualEffectDat.byShield"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ItemVisualEffectDat>> GetManyToManyByShield(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ItemVisualEffectDat>>();
        }

        var items = new List<ResultItem<string, ItemVisualEffectDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShield(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ItemVisualEffectDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ItemVisualEffectDat[] Load()
    {
        const string filePath = "Data/ItemVisualEffect.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemVisualEffectDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DaggerEPKFile
            (var daggerepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BowEPKFile
            (var bowepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OneHandedMaceEPKFile
            (var onehandedmaceepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OneHandedSwordEPKFile
            (var onehandedswordepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TwoHandedSwordEPKFile
            (var twohandedswordepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TwoHandedStaffEPKFile
            (var twohandedstaffepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TwoHandedMaceEPKFile
            (var twohandedmaceepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OneHandedAxeEPKFile
            (var onehandedaxeepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TwoHandedAxeEPKFile
            (var twohandedaxeepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClawEPKFile
            (var clawepkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PETFile
            (var petfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Shield
            (var shieldLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemVisualEffectDat()
            {
                Id = idLoading,
                DaggerEPKFile = daggerepkfileLoading,
                BowEPKFile = bowepkfileLoading,
                OneHandedMaceEPKFile = onehandedmaceepkfileLoading,
                OneHandedSwordEPKFile = onehandedswordepkfileLoading,
                Unknown40 = unknown40Loading,
                TwoHandedSwordEPKFile = twohandedswordepkfileLoading,
                TwoHandedStaffEPKFile = twohandedstaffepkfileLoading,
                Unknown64 = unknown64Loading,
                TwoHandedMaceEPKFile = twohandedmaceepkfileLoading,
                OneHandedAxeEPKFile = onehandedaxeepkfileLoading,
                TwoHandedAxeEPKFile = twohandedaxeepkfileLoading,
                ClawEPKFile = clawepkfileLoading,
                PETFile = petfileLoading,
                Shield = shieldLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
