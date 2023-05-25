using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="IncursionRoomsDat"/> related data and helper methods.
/// </summary>
public sealed class IncursionRoomsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<IncursionRoomsDat> Items { get; }

    private Dictionary<string, List<IncursionRoomsDat>>? byId;
    private Dictionary<string, List<IncursionRoomsDat>>? byName;
    private Dictionary<int, List<IncursionRoomsDat>>? byTier;
    private Dictionary<int, List<IncursionRoomsDat>>? byMinLevel;
    private Dictionary<int, List<IncursionRoomsDat>>? byRoomUpgrade_IncursionRoomsKey;
    private Dictionary<int, List<IncursionRoomsDat>>? byMods;
    private Dictionary<string, List<IncursionRoomsDat>>? byPresentARMFile;
    private Dictionary<int, List<IncursionRoomsDat>>? byHASH16;
    private Dictionary<int, List<IncursionRoomsDat>>? byIncursionArchitectKey;
    private Dictionary<string, List<IncursionRoomsDat>>? byPastARMFile;
    private Dictionary<string, List<IncursionRoomsDat>>? byTSIFile;
    private Dictionary<string, List<IncursionRoomsDat>>? byUIIcon;
    private Dictionary<string, List<IncursionRoomsDat>>? byFlavourText;
    private Dictionary<string, List<IncursionRoomsDat>>? byDescription;
    private Dictionary<int, List<IncursionRoomsDat>>? byAchievementItemsKeys;
    private Dictionary<int, List<IncursionRoomsDat>>? byUnknown132;
    private Dictionary<int, List<IncursionRoomsDat>>? byUnknown136;
    private Dictionary<int, List<IncursionRoomsDat>>? byRoomUpgradeFrom_IncursionRoomsKey;
    private Dictionary<int, List<IncursionRoomsDat>>? byItemisedFlavourText;
    private Dictionary<string, List<IncursionRoomsDat>>? byUnknown164;
    private Dictionary<int, List<IncursionRoomsDat>>? byUnknown172;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncursionRoomsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal IncursionRoomsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out IncursionRoomsDat? item)
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
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
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out IncursionRoomsDat? item)
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
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
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byTier is null)
        {
            byTier = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier;

                if (!byTier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMinLevel(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMinLevel(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.MinLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMinLevel(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byMinLevel is null)
        {
            byMinLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MinLevel;

                if (!byMinLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMinLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMinLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byMinLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByMinLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMinLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.RoomUpgrade_IncursionRoomsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRoomUpgrade_IncursionRoomsKey(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRoomUpgrade_IncursionRoomsKey(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.RoomUpgrade_IncursionRoomsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRoomUpgrade_IncursionRoomsKey(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byRoomUpgrade_IncursionRoomsKey is null)
        {
            byRoomUpgrade_IncursionRoomsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.RoomUpgrade_IncursionRoomsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRoomUpgrade_IncursionRoomsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRoomUpgrade_IncursionRoomsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRoomUpgrade_IncursionRoomsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byRoomUpgrade_IncursionRoomsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByRoomUpgrade_IncursionRoomsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRoomUpgrade_IncursionRoomsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Mods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMods(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMods(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Mods"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMods(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byMods is null)
        {
            byMods = new();
            foreach (var item in Items)
            {
                var itemKey = item.Mods;
                foreach (var listKey in itemKey)
                {
                    if (!byMods.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMods.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMods.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byMods"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByMods(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMods(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.PresentARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPresentARMFile(string? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPresentARMFile(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.PresentARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPresentARMFile(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byPresentARMFile is null)
        {
            byPresentARMFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PresentARMFile;

                if (!byPresentARMFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPresentARMFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPresentARMFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byPresentARMFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByPresentARMFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPresentARMFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH16(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byHASH16 is null)
        {
            byHASH16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH16;

                if (!byHASH16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.IncursionArchitectKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIncursionArchitectKey(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIncursionArchitectKey(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.IncursionArchitectKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIncursionArchitectKey(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byIncursionArchitectKey is null)
        {
            byIncursionArchitectKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.IncursionArchitectKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byIncursionArchitectKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byIncursionArchitectKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byIncursionArchitectKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byIncursionArchitectKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByIncursionArchitectKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIncursionArchitectKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.PastARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPastARMFile(string? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPastARMFile(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.PastARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPastARMFile(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byPastARMFile is null)
        {
            byPastARMFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PastARMFile;

                if (!byPastARMFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPastARMFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPastARMFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byPastARMFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByPastARMFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPastARMFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.TSIFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTSIFile(string? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTSIFile(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.TSIFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTSIFile(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byTSIFile is null)
        {
            byTSIFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.TSIFile;

                if (!byTSIFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTSIFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTSIFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byTSIFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByTSIFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTSIFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.UIIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUIIcon(string? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUIIcon(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.UIIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUIIcon(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byUIIcon is null)
        {
            byUIIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.UIIcon;

                if (!byUIIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUIIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUIIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byUIIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByUIIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUIIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFlavourText(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byFlavourText is null)
        {
            byFlavourText = new();
            foreach (var item in Items)
            {
                var itemKey = item.FlavourText;

                if (!byFlavourText.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFlavourText.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFlavourText.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out IncursionRoomsDat? item)
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
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
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byAchievementItemsKeys is null)
        {
            byAchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown132(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown132(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown132"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown132(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byUnknown132 is null)
        {
            byUnknown132 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown132;

                if (!byUnknown132.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown132.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown132.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byUnknown132"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByUnknown132(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown132(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown136(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown136(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown136(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byUnknown136 is null)
        {
            byUnknown136 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown136;

                if (!byUnknown136.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown136.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown136.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byUnknown136"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByUnknown136(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown136(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.RoomUpgradeFrom_IncursionRoomsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRoomUpgradeFrom_IncursionRoomsKey(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRoomUpgradeFrom_IncursionRoomsKey(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.RoomUpgradeFrom_IncursionRoomsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRoomUpgradeFrom_IncursionRoomsKey(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byRoomUpgradeFrom_IncursionRoomsKey is null)
        {
            byRoomUpgradeFrom_IncursionRoomsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.RoomUpgradeFrom_IncursionRoomsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRoomUpgradeFrom_IncursionRoomsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRoomUpgradeFrom_IncursionRoomsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRoomUpgradeFrom_IncursionRoomsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byRoomUpgradeFrom_IncursionRoomsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByRoomUpgradeFrom_IncursionRoomsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRoomUpgradeFrom_IncursionRoomsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.ItemisedFlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemisedFlavourText(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemisedFlavourText(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.ItemisedFlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemisedFlavourText(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byItemisedFlavourText is null)
        {
            byItemisedFlavourText = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemisedFlavourText;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemisedFlavourText.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemisedFlavourText.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemisedFlavourText.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byItemisedFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByItemisedFlavourText(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemisedFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown164(string? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown164(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown164(string? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byUnknown164 is null)
        {
            byUnknown164 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown164;

                if (!byUnknown164.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown164.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown164.TryGetValue(key, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byUnknown164"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, IncursionRoomsDat>> GetManyToManyByUnknown164(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<string, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown164(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown172(int? key, out IncursionRoomsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown172(key, out var items))
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
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown172(int? key, out IReadOnlyList<IncursionRoomsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        if (byUnknown172 is null)
        {
            byUnknown172 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown172;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown172.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown172.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown172.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<IncursionRoomsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="IncursionRoomsDat"/> with <see cref="IncursionRoomsDat.byUnknown172"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, IncursionRoomsDat>> GetManyToManyByUnknown172(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, IncursionRoomsDat>>();
        }

        var items = new List<ResultItem<int, IncursionRoomsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown172(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, IncursionRoomsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private IncursionRoomsDat[] Load()
    {
        const string filePath = "Data/IncursionRooms.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionRoomsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RoomUpgrade_IncursionRoomsKey
            (var roomupgrade_incursionroomskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Mods
            (var tempmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsLoading = tempmodsLoading.AsReadOnly();

            // loading PresentARMFile
            (var presentarmfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IncursionArchitectKey
            (var incursionarchitectkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading PastARMFile
            (var pastarmfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TSIFile
            (var tsifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UIIcon
            (var uiiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RoomUpgradeFrom_IncursionRoomsKey
            (var roomupgradefrom_incursionroomskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading ItemisedFlavourText
            (var itemisedflavourtextLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown172
            (var tempunknown172Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown172Loading = tempunknown172Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionRoomsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Tier = tierLoading,
                MinLevel = minlevelLoading,
                RoomUpgrade_IncursionRoomsKey = roomupgrade_incursionroomskeyLoading,
                Mods = modsLoading,
                PresentARMFile = presentarmfileLoading,
                HASH16 = hash16Loading,
                IncursionArchitectKey = incursionarchitectkeyLoading,
                PastARMFile = pastarmfileLoading,
                TSIFile = tsifileLoading,
                UIIcon = uiiconLoading,
                FlavourText = flavourtextLoading,
                Description = descriptionLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown132 = unknown132Loading,
                Unknown136 = unknown136Loading,
                RoomUpgradeFrom_IncursionRoomsKey = roomupgradefrom_incursionroomskeyLoading,
                ItemisedFlavourText = itemisedflavourtextLoading,
                Unknown164 = unknown164Loading,
                Unknown172 = unknown172Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
