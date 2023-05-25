using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalUpgradesDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalUpgradesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalUpgradesDat> Items { get; }

    private Dictionary<string, List<BetrayalUpgradesDat>>? byId;
    private Dictionary<string, List<BetrayalUpgradesDat>>? byName;
    private Dictionary<string, List<BetrayalUpgradesDat>>? byDescription;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byModsKey;
    private Dictionary<string, List<BetrayalUpgradesDat>>? byArtFile;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byBetrayalUpgradeSlotsKey;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byUnknown52;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byItemVisualIdentityKey0;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byItemVisualIdentityKey1;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byGrantedEffectsKey;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byUnknown116;
    private Dictionary<int, List<BetrayalUpgradesDat>>? byItemClassesKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalUpgradesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalUpgradesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BetrayalUpgradesDat? item)
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
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
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalUpgradesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<string, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByName(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalUpgradesDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<string, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out BetrayalUpgradesDat? item)
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
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
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalUpgradesDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<string, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKey(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ModsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKey(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byModsKey is null)
        {
            byModsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byModsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByModsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArtFile(string? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArtFile(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ArtFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArtFile(string? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byArtFile is null)
        {
            byArtFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArtFile;

                if (!byArtFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArtFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArtFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byArtFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalUpgradesDat>> GetManyToManyByArtFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<string, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArtFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.BetrayalUpgradeSlotsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalUpgradeSlotsKey(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalUpgradeSlotsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.BetrayalUpgradeSlotsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalUpgradeSlotsKey(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byBetrayalUpgradeSlotsKey is null)
        {
            byBetrayalUpgradeSlotsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalUpgradeSlotsKey;

                if (!byBetrayalUpgradeSlotsKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBetrayalUpgradeSlotsKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalUpgradeSlotsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byBetrayalUpgradeSlotsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByBetrayalUpgradeSlotsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalUpgradeSlotsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out BetrayalUpgradesDat? item)
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown52.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown52.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ItemVisualIdentityKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey0(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey0(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ItemVisualIdentityKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey0(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byItemVisualIdentityKey0 is null)
        {
            byItemVisualIdentityKey0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byItemVisualIdentityKey0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByItemVisualIdentityKey0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ItemVisualIdentityKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemVisualIdentityKey1(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemVisualIdentityKey1(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ItemVisualIdentityKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemVisualIdentityKey1(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byItemVisualIdentityKey1 is null)
        {
            byItemVisualIdentityKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemVisualIdentityKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemVisualIdentityKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemVisualIdentityKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemVisualIdentityKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byItemVisualIdentityKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByItemVisualIdentityKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemVisualIdentityKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsKey(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.GrantedEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsKey(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byGrantedEffectsKey is null)
        {
            byGrantedEffectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byGrantedEffectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByGrantedEffectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;

                if (!byUnknown116.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown116.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByUnknown116(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClassesKey(int? key, out BetrayalUpgradesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClassesKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.ItemClassesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClassesKey(int? key, out IReadOnlyList<BetrayalUpgradesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        if (byItemClassesKey is null)
        {
            byItemClassesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClassesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClassesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClassesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClassesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalUpgradesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalUpgradesDat"/> with <see cref="BetrayalUpgradesDat.byItemClassesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalUpgradesDat>> GetManyToManyByItemClassesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalUpgradesDat>>();
        }

        var items = new List<ResultItem<int, BetrayalUpgradesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClassesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalUpgradesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalUpgradesDat[] Load()
    {
        const string filePath = "Data/BetrayalUpgrades.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKey
            (var tempmodskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeyLoading = tempmodskeyLoading.AsReadOnly();

            // loading ArtFile
            (var artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalUpgradeSlotsKey
            (var betrayalupgradeslotskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading ItemVisualIdentityKey0
            (var itemvisualidentitykey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ItemVisualIdentityKey1
            (var itemvisualidentitykey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemClassesKey
            (var itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalUpgradesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Description = descriptionLoading,
                ModsKey = modskeyLoading,
                ArtFile = artfileLoading,
                BetrayalUpgradeSlotsKey = betrayalupgradeslotskeyLoading,
                Unknown52 = unknown52Loading,
                ItemVisualIdentityKey0 = itemvisualidentitykey0Loading,
                ItemVisualIdentityKey1 = itemvisualidentitykey1Loading,
                GrantedEffectsKey = grantedeffectskeyLoading,
                Unknown116 = unknown116Loading,
                ItemClassesKey = itemclasseskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
