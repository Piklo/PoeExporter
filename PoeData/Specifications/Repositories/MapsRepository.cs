using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MapsDat"/> related data and helper methods.
/// </summary>
public sealed class MapsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MapsDat> Items { get; }

    private Dictionary<int, List<MapsDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<MapsDat>>? byRegular_WorldAreasKey;
    private Dictionary<int, List<MapsDat>>? byUnique_WorldAreasKey;
    private Dictionary<int, List<MapsDat>>? byMapUpgrade_BaseItemTypesKey;
    private Dictionary<int, List<MapsDat>>? byMonsterPacksKeys;
    private Dictionary<int, List<MapsDat>>? byAchievementItem;
    private Dictionary<string, List<MapsDat>>? byRegular_GuildCharacter;
    private Dictionary<string, List<MapsDat>>? byUnique_GuildCharacter;
    private Dictionary<int, List<MapsDat>>? byTier;
    private Dictionary<int, List<MapsDat>>? byShaped_Base_MapsKey;
    private Dictionary<int, List<MapsDat>>? byShaped_AreaLevel;
    private Dictionary<int, List<MapsDat>>? byUpgradedFrom_MapsKey;
    private Dictionary<int, List<MapsDat>>? byMapsKey2;
    private Dictionary<int, List<MapsDat>>? byMapsKey3;
    private Dictionary<int, List<MapsDat>>? byMapSeriesKey;
    private Dictionary<bool, List<MapsDat>>? byUnknown156;
    private Dictionary<bool, List<MapsDat>>? byUnknown157;
    private Dictionary<bool, List<MapsDat>>? byUnknown158;

    /// <summary>
    /// Initializes a new instance of the <see cref="MapsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MapsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out MapsDat? item)
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
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
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Regular_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRegular_WorldAreasKey(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRegular_WorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Regular_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRegular_WorldAreasKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byRegular_WorldAreasKey is null)
        {
            byRegular_WorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Regular_WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byRegular_WorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byRegular_WorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byRegular_WorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byRegular_WorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByRegular_WorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRegular_WorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unique_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnique_WorldAreasKey(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnique_WorldAreasKey(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unique_WorldAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnique_WorldAreasKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byUnique_WorldAreasKey is null)
        {
            byUnique_WorldAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unique_WorldAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnique_WorldAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnique_WorldAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnique_WorldAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byUnique_WorldAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByUnique_WorldAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnique_WorldAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapUpgrade_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapUpgrade_BaseItemTypesKey(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapUpgrade_BaseItemTypesKey(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapUpgrade_BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapUpgrade_BaseItemTypesKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byMapUpgrade_BaseItemTypesKey is null)
        {
            byMapUpgrade_BaseItemTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapUpgrade_BaseItemTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapUpgrade_BaseItemTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapUpgrade_BaseItemTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapUpgrade_BaseItemTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byMapUpgrade_BaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByMapUpgrade_BaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapUpgrade_BaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MonsterPacksKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterPacksKeys(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterPacksKeys(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MonsterPacksKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterPacksKeys(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byMonsterPacksKeys is null)
        {
            byMonsterPacksKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterPacksKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterPacksKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterPacksKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterPacksKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byMonsterPacksKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByMonsterPacksKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterPacksKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.AchievementItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItem(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItem(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.AchievementItem"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItem(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byAchievementItem is null)
        {
            byAchievementItem = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItem;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAchievementItem.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAchievementItem.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAchievementItem.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byAchievementItem"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByAchievementItem(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItem(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Regular_GuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRegular_GuildCharacter(string? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRegular_GuildCharacter(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Regular_GuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRegular_GuildCharacter(string? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byRegular_GuildCharacter is null)
        {
            byRegular_GuildCharacter = new();
            foreach (var item in Items)
            {
                var itemKey = item.Regular_GuildCharacter;

                if (!byRegular_GuildCharacter.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRegular_GuildCharacter.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRegular_GuildCharacter.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byRegular_GuildCharacter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapsDat>> GetManyToManyByRegular_GuildCharacter(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapsDat>>();
        }

        var items = new List<ResultItem<string, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRegular_GuildCharacter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unique_GuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnique_GuildCharacter(string? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnique_GuildCharacter(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unique_GuildCharacter"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnique_GuildCharacter(string? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byUnique_GuildCharacter is null)
        {
            byUnique_GuildCharacter = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unique_GuildCharacter;

                if (!byUnique_GuildCharacter.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnique_GuildCharacter.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnique_GuildCharacter.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byUnique_GuildCharacter"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MapsDat>> GetManyToManyByUnique_GuildCharacter(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MapsDat>>();
        }

        var items = new List<ResultItem<string, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnique_GuildCharacter(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier(int? key, out MapsDat? item)
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Tier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
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
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byTier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByTier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Shaped_Base_MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShaped_Base_MapsKey(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShaped_Base_MapsKey(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Shaped_Base_MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShaped_Base_MapsKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byShaped_Base_MapsKey is null)
        {
            byShaped_Base_MapsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Shaped_Base_MapsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShaped_Base_MapsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShaped_Base_MapsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShaped_Base_MapsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byShaped_Base_MapsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByShaped_Base_MapsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShaped_Base_MapsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Shaped_AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShaped_AreaLevel(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShaped_AreaLevel(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Shaped_AreaLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShaped_AreaLevel(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byShaped_AreaLevel is null)
        {
            byShaped_AreaLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Shaped_AreaLevel;

                if (!byShaped_AreaLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShaped_AreaLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShaped_AreaLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byShaped_AreaLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByShaped_AreaLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShaped_AreaLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.UpgradedFrom_MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUpgradedFrom_MapsKey(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUpgradedFrom_MapsKey(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.UpgradedFrom_MapsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUpgradedFrom_MapsKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byUpgradedFrom_MapsKey is null)
        {
            byUpgradedFrom_MapsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.UpgradedFrom_MapsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUpgradedFrom_MapsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUpgradedFrom_MapsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUpgradedFrom_MapsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byUpgradedFrom_MapsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByUpgradedFrom_MapsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUpgradedFrom_MapsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapsKey2(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapsKey2(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapsKey2(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byMapsKey2 is null)
        {
            byMapsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byMapsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByMapsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapsKey3(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapsKey3(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapsKey3(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byMapsKey3 is null)
        {
            byMapsKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapsKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMapsKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMapsKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMapsKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byMapsKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByMapsKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapsKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapSeriesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapSeriesKey(int? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapSeriesKey(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.MapSeriesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapSeriesKey(int? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byMapSeriesKey is null)
        {
            byMapSeriesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapSeriesKey;

                if (!byMapSeriesKey.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapSeriesKey.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapSeriesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byMapSeriesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MapsDat>> GetManyToManyByMapSeriesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MapsDat>>();
        }

        var items = new List<ResultItem<int, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapSeriesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown156(bool? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown156(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown156(bool? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byUnknown156 is null)
        {
            byUnknown156 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown156;

                if (!byUnknown156.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown156.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown156.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byUnknown156"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapsDat>> GetManyToManyByUnknown156(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapsDat>>();
        }

        var items = new List<ResultItem<bool, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown156(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unknown157"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown157(bool? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown157(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unknown157"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown157(bool? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byUnknown157 is null)
        {
            byUnknown157 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown157;

                if (!byUnknown157.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown157.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown157.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byUnknown157"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapsDat>> GetManyToManyByUnknown157(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapsDat>>();
        }

        var items = new List<ResultItem<bool, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown157(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unknown158"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown158(bool? key, out MapsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown158(key, out var items))
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
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.Unknown158"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown158(bool? key, out IReadOnlyList<MapsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        if (byUnknown158 is null)
        {
            byUnknown158 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown158;

                if (!byUnknown158.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown158.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown158.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MapsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MapsDat"/> with <see cref="MapsDat.byUnknown158"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MapsDat>> GetManyToManyByUnknown158(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MapsDat>>();
        }

        var items = new List<ResultItem<bool, MapsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown158(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MapsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MapsDat[] Load()
    {
        const string filePath = "Data/Maps.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Regular_WorldAreasKey
            (var regular_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unique_WorldAreasKey
            (var unique_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MapUpgrade_BaseItemTypesKey
            (var mapupgrade_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            // loading AchievementItem
            (var achievementitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Regular_GuildCharacter
            (var regular_guildcharacterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_GuildCharacter
            (var unique_guildcharacterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Shaped_Base_MapsKey
            (var shaped_base_mapskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Shaped_AreaLevel
            (var shaped_arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UpgradedFrom_MapsKey
            (var upgradedfrom_mapskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading MapsKey2
            (var mapskey2Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading MapsKey3
            (var mapskey3Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading MapSeriesKey
            (var mapserieskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown156
            (var unknown156Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown157
            (var unknown157Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown158
            (var unknown158Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Regular_WorldAreasKey = regular_worldareaskeyLoading,
                Unique_WorldAreasKey = unique_worldareaskeyLoading,
                MapUpgrade_BaseItemTypesKey = mapupgrade_baseitemtypeskeyLoading,
                MonsterPacksKeys = monsterpackskeysLoading,
                AchievementItem = achievementitemLoading,
                Regular_GuildCharacter = regular_guildcharacterLoading,
                Unique_GuildCharacter = unique_guildcharacterLoading,
                Tier = tierLoading,
                Shaped_Base_MapsKey = shaped_base_mapskeyLoading,
                Shaped_AreaLevel = shaped_arealevelLoading,
                UpgradedFrom_MapsKey = upgradedfrom_mapskeyLoading,
                MapsKey2 = mapskey2Loading,
                MapsKey3 = mapskey3Loading,
                MapSeriesKey = mapserieskeyLoading,
                Unknown156 = unknown156Loading,
                Unknown157 = unknown157Loading,
                Unknown158 = unknown158Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
