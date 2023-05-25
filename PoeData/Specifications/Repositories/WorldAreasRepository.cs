using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WorldAreasDat"/> related data and helper methods.
/// </summary>
public sealed class WorldAreasRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WorldAreasDat> Items { get; }

    private Dictionary<string, List<WorldAreasDat>>? byId;
    private Dictionary<string, List<WorldAreasDat>>? byName;
    private Dictionary<int, List<WorldAreasDat>>? byAct;
    private Dictionary<bool, List<WorldAreasDat>>? byIsTown;
    private Dictionary<bool, List<WorldAreasDat>>? byHasWaypoint;
    private Dictionary<int, List<WorldAreasDat>>? byConnections_WorldAreasKeys;
    private Dictionary<int, List<WorldAreasDat>>? byAreaLevel;
    private Dictionary<int, List<WorldAreasDat>>? byHASH16;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown46;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown50;
    private Dictionary<string, List<WorldAreasDat>>? byLoadingScreen_DDSFile;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown62;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown78;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown94;
    private Dictionary<int, List<WorldAreasDat>>? byTopologiesKeys;
    private Dictionary<int, List<WorldAreasDat>>? byParentTown_WorldAreasKey;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown122;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown126;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown142;
    private Dictionary<int, List<WorldAreasDat>>? byBosses_MonsterVarietiesKeys;
    private Dictionary<int, List<WorldAreasDat>>? byMonsters_MonsterVarietiesKeys;
    private Dictionary<int, List<WorldAreasDat>>? bySpawnWeight_TagsKeys;
    private Dictionary<int, List<WorldAreasDat>>? bySpawnWeight_Values;
    private Dictionary<bool, List<WorldAreasDat>>? byIsMapArea;
    private Dictionary<int, List<WorldAreasDat>>? byFullClear_AchievementItemsKeys;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown239;
    private Dictionary<int, List<WorldAreasDat>>? byAchievementItemsKey;
    private Dictionary<int, List<WorldAreasDat>>? byModsKeys;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown287;
    private Dictionary<int, List<WorldAreasDat>>? byVaalArea;
    private Dictionary<bool, List<WorldAreasDat>>? byUnknown307;
    private Dictionary<int, List<WorldAreasDat>>? byMaxLevel;
    private Dictionary<int, List<WorldAreasDat>>? byAreaTypeTags;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown328;
    private Dictionary<bool, List<WorldAreasDat>>? byIsHideout;
    private Dictionary<string, List<WorldAreasDat>>? byInflection;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown341;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown345;
    private Dictionary<int, List<WorldAreasDat>>? byTags;
    private Dictionary<bool, List<WorldAreasDat>>? byIsVaalArea;
    private Dictionary<bool, List<WorldAreasDat>>? byIsLabyrinthAirlock;
    private Dictionary<bool, List<WorldAreasDat>>? byIsLabyrinthArea;
    private Dictionary<int, List<WorldAreasDat>>? byTwinnedFullClear_AchievementItemsKey;
    private Dictionary<int, List<WorldAreasDat>>? byEnter_AchievementItemsKey;
    private Dictionary<string, List<WorldAreasDat>>? byTSIFile;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown408;
    private Dictionary<int, List<WorldAreasDat>>? byWaypointActivation_AchievementItemsKeys;
    private Dictionary<bool, List<WorldAreasDat>>? byIsUniqueMapArea;
    private Dictionary<bool, List<WorldAreasDat>>? byIsLabyrinthBossArea;
    private Dictionary<int, List<WorldAreasDat>>? byFirstEntry_NPCTextAudioKey;
    private Dictionary<int, List<WorldAreasDat>>? byFirstEntry_SoundEffectsKey;
    private Dictionary<string, List<WorldAreasDat>>? byFirstEntry_NPCsKey;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown482;
    private Dictionary<int, List<WorldAreasDat>>? byEnvironmentsKey;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown502;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown506;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown522;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown526;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown530;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown534;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown538;
    private Dictionary<bool, List<WorldAreasDat>>? byUnknown542;
    private Dictionary<bool, List<WorldAreasDat>>? byUnknown543;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown544;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown548;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown552;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown556;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown572;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown588;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown592;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown608;
    private Dictionary<int, List<WorldAreasDat>>? byUnknown624;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorldAreasRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WorldAreasRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out WorldAreasDat? item)
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
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
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldAreasDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldAreasDat>>();
        }

        var items = new List<ResultItem<string, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out WorldAreasDat? item)
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
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
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldAreasDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldAreasDat>>();
        }

        var items = new List<ResultItem<string, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Act"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAct(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAct(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Act"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAct(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byAct is null)
        {
            byAct = new();
            foreach (var item in Items)
            {
                var itemKey = item.Act;

                if (!byAct.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAct.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAct.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byAct"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByAct(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAct(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsTown"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsTown(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsTown(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsTown"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsTown(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsTown is null)
        {
            byIsTown = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsTown;

                if (!byIsTown.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsTown.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsTown.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsTown"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsTown(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsTown(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.HasWaypoint"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHasWaypoint(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHasWaypoint(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.HasWaypoint"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHasWaypoint(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byHasWaypoint is null)
        {
            byHasWaypoint = new();
            foreach (var item in Items)
            {
                var itemKey = item.HasWaypoint;

                if (!byHasWaypoint.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHasWaypoint.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHasWaypoint.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byHasWaypoint"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByHasWaypoint(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHasWaypoint(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Connections_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByConnections_WorldAreasKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByConnections_WorldAreasKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Connections_WorldAreasKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByConnections_WorldAreasKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byConnections_WorldAreasKeys is null)
        {
            byConnections_WorldAreasKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Connections_WorldAreasKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byConnections_WorldAreasKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byConnections_WorldAreasKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byConnections_WorldAreasKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byConnections_WorldAreasKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByConnections_WorldAreasKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByConnections_WorldAreasKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaLevel(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaLevel(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaLevel(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byAreaLevel is null)
        {
            byAreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaLevel;

                if (!byAreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byAreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByAreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out WorldAreasDat? item)
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
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
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown46(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown46(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown46(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown46 is null)
        {
            byUnknown46 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown46;

                if (!byUnknown46.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown46.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown46.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown46"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown46(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown46(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown50(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown50(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown50(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown50 is null)
        {
            byUnknown50 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown50;

                if (!byUnknown50.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown50.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown50.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown50"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown50(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown50(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.LoadingScreen_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLoadingScreen_DDSFile(string? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLoadingScreen_DDSFile(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.LoadingScreen_DDSFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLoadingScreen_DDSFile(string? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byLoadingScreen_DDSFile is null)
        {
            byLoadingScreen_DDSFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.LoadingScreen_DDSFile;

                if (!byLoadingScreen_DDSFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLoadingScreen_DDSFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLoadingScreen_DDSFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byLoadingScreen_DDSFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldAreasDat>> GetManyToManyByLoadingScreen_DDSFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldAreasDat>>();
        }

        var items = new List<ResultItem<string, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLoadingScreen_DDSFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown62(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown62 is null)
        {
            byUnknown62 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown62;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown62.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown62.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown62.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown62(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown78(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown78(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown78"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown78(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown78 is null)
        {
            byUnknown78 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown78;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown78.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown78.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown78.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown78"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown78(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown78(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown94"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown94(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown94(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown94"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown94(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown94 is null)
        {
            byUnknown94 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown94;

                if (!byUnknown94.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown94.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown94.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown94"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown94(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown94(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.TopologiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTopologiesKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTopologiesKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.TopologiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTopologiesKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byTopologiesKeys is null)
        {
            byTopologiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.TopologiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTopologiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTopologiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTopologiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byTopologiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByTopologiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTopologiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.ParentTown_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByParentTown_WorldAreasKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByParentTown_WorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.ParentTown_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByParentTown_WorldAreasKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byParentTown_WorldAreasKey is null)
        {
            byParentTown_WorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ParentTown_WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byParentTown_WorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byParentTown_WorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byParentTown_WorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byParentTown_WorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByParentTown_WorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByParentTown_WorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown122(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown122(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown122(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown122 is null)
        {
            byUnknown122 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown122;

                if (!byUnknown122.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown122.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown122.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown122"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown122(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown122(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown126(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown126(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown126(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown126 is null)
        {
            byUnknown126 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown126;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown126.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown126.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown126.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown126"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown126(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown126(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown142(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown142 is null)
        {
            byUnknown142 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown142;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown142.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown142.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown142.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown142(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Bosses_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBosses_MonsterVarietiesKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBosses_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Bosses_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBosses_MonsterVarietiesKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byBosses_MonsterVarietiesKeys is null)
        {
            byBosses_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Bosses_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byBosses_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBosses_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBosses_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byBosses_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByBosses_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBosses_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Monsters_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsters_MonsterVarietiesKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsters_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Monsters_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsters_MonsterVarietiesKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byMonsters_MonsterVarietiesKeys is null)
        {
            byMonsters_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Monsters_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsters_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsters_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsters_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byMonsters_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByMonsters_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsters_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_TagsKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_TagsKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (bySpawnWeight_TagsKeys is null)
        {
            bySpawnWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.bySpawnWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyBySpawnWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_Values(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight_Values(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_Values(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (bySpawnWeight_Values is null)
        {
            bySpawnWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!bySpawnWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySpawnWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySpawnWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.bySpawnWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyBySpawnWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsMapArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsMapArea(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsMapArea(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsMapArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsMapArea(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsMapArea is null)
        {
            byIsMapArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsMapArea;

                if (!byIsMapArea.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsMapArea.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsMapArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsMapArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsMapArea(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsMapArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FullClear_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFullClear_AchievementItemsKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFullClear_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FullClear_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFullClear_AchievementItemsKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byFullClear_AchievementItemsKeys is null)
        {
            byFullClear_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.FullClear_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byFullClear_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFullClear_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFullClear_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byFullClear_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByFullClear_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFullClear_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown239"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown239(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown239(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown239"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown239(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown239 is null)
        {
            byUnknown239 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown239;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown239.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown239.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown239.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown239"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown239(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown239(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModsKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModsKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.ModsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModsKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byModsKeys is null)
        {
            byModsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byModsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byModsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByModsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown287"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown287(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown287(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown287"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown287(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown287 is null)
        {
            byUnknown287 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown287;

                if (!byUnknown287.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown287.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown287.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown287"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown287(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown287(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.VaalArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVaalArea(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVaalArea(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.VaalArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVaalArea(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byVaalArea is null)
        {
            byVaalArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.VaalArea;
                foreach (var listKey in itemKey)
                {
                    if (!byVaalArea.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byVaalArea.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byVaalArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byVaalArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByVaalArea(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVaalArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown307"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown307(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown307(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown307"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown307(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown307 is null)
        {
            byUnknown307 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown307;

                if (!byUnknown307.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown307.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown307.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown307"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByUnknown307(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown307(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaxLevel(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byMaxLevel is null)
        {
            byMaxLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MaxLevel;

                if (!byMaxLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMaxLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMaxLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.AreaTypeTags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAreaTypeTags(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAreaTypeTags(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.AreaTypeTags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAreaTypeTags(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byAreaTypeTags is null)
        {
            byAreaTypeTags = new();
            foreach (var item in Items)
            {
                var itemKey = item.AreaTypeTags;
                foreach (var listKey in itemKey)
                {
                    if (!byAreaTypeTags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAreaTypeTags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAreaTypeTags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byAreaTypeTags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByAreaTypeTags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAreaTypeTags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown328"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown328(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown328(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown328"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown328(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown328 is null)
        {
            byUnknown328 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown328;

                if (!byUnknown328.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown328.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown328.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown328"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown328(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown328(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsHideout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsHideout(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsHideout(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsHideout"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsHideout(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsHideout is null)
        {
            byIsHideout = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsHideout;

                if (!byIsHideout.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsHideout.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsHideout.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsHideout"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsHideout(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsHideout(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Inflection"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInflection(string? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInflection(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Inflection"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInflection(string? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byInflection is null)
        {
            byInflection = new();
            foreach (var item in Items)
            {
                var itemKey = item.Inflection;

                if (!byInflection.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInflection.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInflection.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byInflection"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldAreasDat>> GetManyToManyByInflection(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldAreasDat>>();
        }

        var items = new List<ResultItem<string, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInflection(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown341"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown341(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown341(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown341"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown341(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown341 is null)
        {
            byUnknown341 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown341;

                if (!byUnknown341.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown341.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown341.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown341"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown341(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown341(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown345"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown345(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown345(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown345"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown345(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown345 is null)
        {
            byUnknown345 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown345;

                if (!byUnknown345.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown345.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown345.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown345"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown345(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown345(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTags(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTags(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Tags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTags(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byTags is null)
        {
            byTags = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tags;
                foreach (var listKey in itemKey)
                {
                    if (!byTags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byTags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByTags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsVaalArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsVaalArea(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsVaalArea(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsVaalArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsVaalArea(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsVaalArea is null)
        {
            byIsVaalArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsVaalArea;

                if (!byIsVaalArea.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsVaalArea.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsVaalArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsVaalArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsVaalArea(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsVaalArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsLabyrinthAirlock"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLabyrinthAirlock(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLabyrinthAirlock(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsLabyrinthAirlock"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLabyrinthAirlock(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsLabyrinthAirlock is null)
        {
            byIsLabyrinthAirlock = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLabyrinthAirlock;

                if (!byIsLabyrinthAirlock.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLabyrinthAirlock.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLabyrinthAirlock.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsLabyrinthAirlock"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsLabyrinthAirlock(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLabyrinthAirlock(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsLabyrinthArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLabyrinthArea(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLabyrinthArea(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsLabyrinthArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLabyrinthArea(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsLabyrinthArea is null)
        {
            byIsLabyrinthArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLabyrinthArea;

                if (!byIsLabyrinthArea.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLabyrinthArea.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLabyrinthArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsLabyrinthArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsLabyrinthArea(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLabyrinthArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.TwinnedFullClear_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTwinnedFullClear_AchievementItemsKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTwinnedFullClear_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.TwinnedFullClear_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTwinnedFullClear_AchievementItemsKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byTwinnedFullClear_AchievementItemsKey is null)
        {
            byTwinnedFullClear_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.TwinnedFullClear_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byTwinnedFullClear_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byTwinnedFullClear_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byTwinnedFullClear_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byTwinnedFullClear_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByTwinnedFullClear_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTwinnedFullClear_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Enter_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnter_AchievementItemsKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnter_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Enter_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnter_AchievementItemsKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byEnter_AchievementItemsKey is null)
        {
            byEnter_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Enter_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnter_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnter_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnter_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byEnter_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByEnter_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnter_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.TSIFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTSIFile(string? key, out WorldAreasDat? item)
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.TSIFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTSIFile(string? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
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
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byTSIFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldAreasDat>> GetManyToManyByTSIFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldAreasDat>>();
        }

        var items = new List<ResultItem<string, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTSIFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown408"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown408(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown408(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown408"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown408(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown408 is null)
        {
            byUnknown408 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown408;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown408.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown408.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown408.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown408"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown408(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown408(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.WaypointActivation_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWaypointActivation_AchievementItemsKeys(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWaypointActivation_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.WaypointActivation_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWaypointActivation_AchievementItemsKeys(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byWaypointActivation_AchievementItemsKeys is null)
        {
            byWaypointActivation_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.WaypointActivation_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byWaypointActivation_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byWaypointActivation_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byWaypointActivation_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byWaypointActivation_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByWaypointActivation_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWaypointActivation_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsUniqueMapArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsUniqueMapArea(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsUniqueMapArea(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsUniqueMapArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsUniqueMapArea(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsUniqueMapArea is null)
        {
            byIsUniqueMapArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsUniqueMapArea;

                if (!byIsUniqueMapArea.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsUniqueMapArea.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsUniqueMapArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsUniqueMapArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsUniqueMapArea(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsUniqueMapArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsLabyrinthBossArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLabyrinthBossArea(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLabyrinthBossArea(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.IsLabyrinthBossArea"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLabyrinthBossArea(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byIsLabyrinthBossArea is null)
        {
            byIsLabyrinthBossArea = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLabyrinthBossArea;

                if (!byIsLabyrinthBossArea.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLabyrinthBossArea.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLabyrinthBossArea.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byIsLabyrinthBossArea"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByIsLabyrinthBossArea(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLabyrinthBossArea(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FirstEntry_NPCTextAudioKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFirstEntry_NPCTextAudioKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFirstEntry_NPCTextAudioKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FirstEntry_NPCTextAudioKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFirstEntry_NPCTextAudioKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byFirstEntry_NPCTextAudioKey is null)
        {
            byFirstEntry_NPCTextAudioKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FirstEntry_NPCTextAudioKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFirstEntry_NPCTextAudioKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFirstEntry_NPCTextAudioKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFirstEntry_NPCTextAudioKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byFirstEntry_NPCTextAudioKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByFirstEntry_NPCTextAudioKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFirstEntry_NPCTextAudioKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FirstEntry_SoundEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFirstEntry_SoundEffectsKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFirstEntry_SoundEffectsKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FirstEntry_SoundEffectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFirstEntry_SoundEffectsKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byFirstEntry_SoundEffectsKey is null)
        {
            byFirstEntry_SoundEffectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FirstEntry_SoundEffectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFirstEntry_SoundEffectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFirstEntry_SoundEffectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFirstEntry_SoundEffectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byFirstEntry_SoundEffectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByFirstEntry_SoundEffectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFirstEntry_SoundEffectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FirstEntry_NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFirstEntry_NPCsKey(string? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFirstEntry_NPCsKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.FirstEntry_NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFirstEntry_NPCsKey(string? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byFirstEntry_NPCsKey is null)
        {
            byFirstEntry_NPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FirstEntry_NPCsKey;

                if (!byFirstEntry_NPCsKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFirstEntry_NPCsKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFirstEntry_NPCsKey.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byFirstEntry_NPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WorldAreasDat>> GetManyToManyByFirstEntry_NPCsKey(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WorldAreasDat>>();
        }

        var items = new List<ResultItem<string, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFirstEntry_NPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown482"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown482(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown482(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown482"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown482(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown482 is null)
        {
            byUnknown482 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown482;

                if (!byUnknown482.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown482.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown482.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown482"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown482(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown482(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.EnvironmentsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnvironmentsKey(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnvironmentsKey(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.EnvironmentsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnvironmentsKey(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byEnvironmentsKey is null)
        {
            byEnvironmentsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnvironmentsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnvironmentsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnvironmentsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnvironmentsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byEnvironmentsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByEnvironmentsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnvironmentsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown502"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown502(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown502(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown502"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown502(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown502 is null)
        {
            byUnknown502 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown502;

                if (!byUnknown502.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown502.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown502.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown502"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown502(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown502(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown506"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown506(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown506(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown506"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown506(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown506 is null)
        {
            byUnknown506 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown506;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown506.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown506.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown506.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown506"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown506(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown506(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown522"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown522(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown522(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown522"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown522(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown522 is null)
        {
            byUnknown522 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown522;

                if (!byUnknown522.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown522.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown522.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown522"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown522(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown522(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown526"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown526(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown526(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown526"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown526(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown526 is null)
        {
            byUnknown526 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown526;

                if (!byUnknown526.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown526.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown526.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown526"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown526(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown526(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown530"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown530(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown530(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown530"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown530(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown530 is null)
        {
            byUnknown530 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown530;

                if (!byUnknown530.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown530.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown530.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown530"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown530(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown530(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown534"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown534(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown534(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown534"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown534(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown534 is null)
        {
            byUnknown534 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown534;

                if (!byUnknown534.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown534.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown534.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown534"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown534(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown534(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown538"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown538(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown538(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown538"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown538(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown538 is null)
        {
            byUnknown538 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown538;

                if (!byUnknown538.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown538.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown538.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown538"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown538(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown538(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown542"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown542(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown542(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown542"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown542(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown542 is null)
        {
            byUnknown542 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown542;

                if (!byUnknown542.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown542.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown542.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown542"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByUnknown542(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown542(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown543"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown543(bool? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown543(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown543"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown543(bool? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown543 is null)
        {
            byUnknown543 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown543;

                if (!byUnknown543.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown543.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown543.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown543"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WorldAreasDat>> GetManyToManyByUnknown543(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WorldAreasDat>>();
        }

        var items = new List<ResultItem<bool, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown543(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown544"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown544(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown544(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown544"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown544(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown544 is null)
        {
            byUnknown544 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown544;

                if (!byUnknown544.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown544.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown544.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown544"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown544(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown544(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown548"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown548(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown548(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown548"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown548(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown548 is null)
        {
            byUnknown548 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown548;

                if (!byUnknown548.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown548.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown548.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown548"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown548(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown548(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown552"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown552(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown552(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown552"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown552(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown552 is null)
        {
            byUnknown552 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown552;

                if (!byUnknown552.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown552.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown552.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown552"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown552(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown552(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown556"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown556(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown556(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown556"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown556(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown556 is null)
        {
            byUnknown556 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown556;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown556.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown556.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown556.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown556"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown556(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown556(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown572"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown572(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown572(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown572"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown572(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown572 is null)
        {
            byUnknown572 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown572;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown572.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown572.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown572.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown572"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown572(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown572(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown588"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown588(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown588(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown588"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown588(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown588 is null)
        {
            byUnknown588 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown588;

                if (!byUnknown588.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown588.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown588.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown588"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown588(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown588(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown592"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown592(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown592(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown592"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown592(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown592 is null)
        {
            byUnknown592 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown592;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown592.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown592.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown592.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown592"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown592(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown592(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown608"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown608(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown608(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown608"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown608(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown608 is null)
        {
            byUnknown608 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown608;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown608.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown608.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown608.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown608"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown608(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown608(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown624"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown624(int? key, out WorldAreasDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown624(key, out var items))
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
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.Unknown624"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown624(int? key, out IReadOnlyList<WorldAreasDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        if (byUnknown624 is null)
        {
            byUnknown624 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown624;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown624.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown624.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown624.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WorldAreasDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WorldAreasDat"/> with <see cref="WorldAreasDat.byUnknown624"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WorldAreasDat>> GetManyToManyByUnknown624(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WorldAreasDat>>();
        }

        var items = new List<ResultItem<int, WorldAreasDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown624(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WorldAreasDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WorldAreasDat[] Load()
    {
        const string filePath = "Data/WorldAreas.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WorldAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Act
            (var actLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsTown
            (var istownLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasWaypoint
            (var haswaypointLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Connections_WorldAreasKeys
            (var tempconnections_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var connections_worldareaskeysLoading = tempconnections_worldareaskeysLoading.AsReadOnly();

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LoadingScreen_DDSFile
            (var loadingscreen_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown78
            (var tempunknown78Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown78Loading = tempunknown78Loading.AsReadOnly();

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TopologiesKeys
            (var temptopologieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var topologieskeysLoading = temptopologieskeysLoading.AsReadOnly();

            // loading ParentTown_WorldAreasKey
            (var parenttown_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Bosses_MonsterVarietiesKeys
            (var tempbosses_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bosses_monstervarietieskeysLoading = tempbosses_monstervarietieskeysLoading.AsReadOnly();

            // loading Monsters_MonsterVarietiesKeys
            (var tempmonsters_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsters_monstervarietieskeysLoading = tempmonsters_monstervarietieskeysLoading.AsReadOnly();

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading IsMapArea
            (var ismapareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading FullClear_AchievementItemsKeys
            (var tempfullclear_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var fullclear_achievementitemskeysLoading = tempfullclear_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown239
            (var unknown239Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading Unknown287
            (var unknown287Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VaalArea
            (var tempvaalareaLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var vaalareaLoading = tempvaalareaLoading.AsReadOnly();

            // loading Unknown307
            (var unknown307Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AreaTypeTags
            (var tempareatypetagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var areatypetagsLoading = tempareatypetagsLoading.AsReadOnly();

            // loading Unknown328
            (var unknown328Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsHideout
            (var ishideoutLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Inflection
            (var inflectionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown341
            (var unknown341Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown345
            (var unknown345Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tags
            (var temptagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagsLoading = temptagsLoading.AsReadOnly();

            // loading IsVaalArea
            (var isvaalareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLabyrinthAirlock
            (var islabyrinthairlockLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLabyrinthArea
            (var islabyrinthareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TwinnedFullClear_AchievementItemsKey
            (var twinnedfullclear_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Enter_AchievementItemsKey
            (var enter_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TSIFile
            (var tsifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown408
            (var unknown408Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WaypointActivation_AchievementItemsKeys
            (var tempwaypointactivation_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var waypointactivation_achievementitemskeysLoading = tempwaypointactivation_achievementitemskeysLoading.AsReadOnly();

            // loading IsUniqueMapArea
            (var isuniquemapareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLabyrinthBossArea
            (var islabyrinthbossareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading FirstEntry_NPCTextAudioKey
            (var firstentry_npctextaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FirstEntry_SoundEffectsKey
            (var firstentry_soundeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FirstEntry_NPCsKey
            (var firstentry_npcskeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown482
            (var unknown482Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnvironmentsKey
            (var environmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown502
            (var unknown502Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown506
            (var unknown506Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown522
            (var unknown522Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown526
            (var unknown526Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown530
            (var unknown530Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown534
            (var unknown534Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown538
            (var unknown538Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown542
            (var unknown542Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown543
            (var unknown543Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown544
            (var unknown544Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown548
            (var unknown548Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown552
            (var unknown552Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown556
            (var tempunknown556Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown556Loading = tempunknown556Loading.AsReadOnly();

            // loading Unknown572
            (var unknown572Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown588
            (var unknown588Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown592
            (var unknown592Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown608
            (var unknown608Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown624
            (var unknown624Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WorldAreasDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Act = actLoading,
                IsTown = istownLoading,
                HasWaypoint = haswaypointLoading,
                Connections_WorldAreasKeys = connections_worldareaskeysLoading,
                AreaLevel = arealevelLoading,
                HASH16 = hash16Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                LoadingScreen_DDSFile = loadingscreen_ddsfileLoading,
                Unknown62 = unknown62Loading,
                Unknown78 = unknown78Loading,
                Unknown94 = unknown94Loading,
                TopologiesKeys = topologieskeysLoading,
                ParentTown_WorldAreasKey = parenttown_worldareaskeyLoading,
                Unknown122 = unknown122Loading,
                Unknown126 = unknown126Loading,
                Unknown142 = unknown142Loading,
                Bosses_MonsterVarietiesKeys = bosses_monstervarietieskeysLoading,
                Monsters_MonsterVarietiesKeys = monsters_monstervarietieskeysLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                IsMapArea = ismapareaLoading,
                FullClear_AchievementItemsKeys = fullclear_achievementitemskeysLoading,
                Unknown239 = unknown239Loading,
                AchievementItemsKey = achievementitemskeyLoading,
                ModsKeys = modskeysLoading,
                Unknown287 = unknown287Loading,
                VaalArea = vaalareaLoading,
                Unknown307 = unknown307Loading,
                MaxLevel = maxlevelLoading,
                AreaTypeTags = areatypetagsLoading,
                Unknown328 = unknown328Loading,
                IsHideout = ishideoutLoading,
                Inflection = inflectionLoading,
                Unknown341 = unknown341Loading,
                Unknown345 = unknown345Loading,
                Tags = tagsLoading,
                IsVaalArea = isvaalareaLoading,
                IsLabyrinthAirlock = islabyrinthairlockLoading,
                IsLabyrinthArea = islabyrinthareaLoading,
                TwinnedFullClear_AchievementItemsKey = twinnedfullclear_achievementitemskeyLoading,
                Enter_AchievementItemsKey = enter_achievementitemskeyLoading,
                TSIFile = tsifileLoading,
                Unknown408 = unknown408Loading,
                WaypointActivation_AchievementItemsKeys = waypointactivation_achievementitemskeysLoading,
                IsUniqueMapArea = isuniquemapareaLoading,
                IsLabyrinthBossArea = islabyrinthbossareaLoading,
                FirstEntry_NPCTextAudioKey = firstentry_npctextaudiokeyLoading,
                FirstEntry_SoundEffectsKey = firstentry_soundeffectskeyLoading,
                FirstEntry_NPCsKey = firstentry_npcskeyLoading,
                Unknown482 = unknown482Loading,
                EnvironmentsKey = environmentskeyLoading,
                Unknown502 = unknown502Loading,
                Unknown506 = unknown506Loading,
                Unknown522 = unknown522Loading,
                Unknown526 = unknown526Loading,
                Unknown530 = unknown530Loading,
                Unknown534 = unknown534Loading,
                Unknown538 = unknown538Loading,
                Unknown542 = unknown542Loading,
                Unknown543 = unknown543Loading,
                Unknown544 = unknown544Loading,
                Unknown548 = unknown548Loading,
                Unknown552 = unknown552Loading,
                Unknown556 = unknown556Loading,
                Unknown572 = unknown572Loading,
                Unknown588 = unknown588Loading,
                Unknown592 = unknown592Loading,
                Unknown608 = unknown608Loading,
                Unknown624 = unknown624Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
