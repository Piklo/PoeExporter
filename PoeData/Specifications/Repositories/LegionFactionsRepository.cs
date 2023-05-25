using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LegionFactionsDat"/> related data and helper methods.
/// </summary>
public sealed class LegionFactionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LegionFactionsDat> Items { get; }

    private Dictionary<string, List<LegionFactionsDat>>? byId;
    private Dictionary<int, List<LegionFactionsDat>>? bySpawnWeight;
    private Dictionary<int, List<LegionFactionsDat>>? byLegionBalancePerLevelKey;
    private Dictionary<float, List<LegionFactionsDat>>? byUnknown28;
    private Dictionary<float, List<LegionFactionsDat>>? byUnknown32;
    private Dictionary<int, List<LegionFactionsDat>>? byBuffVisualsKey;
    private Dictionary<int, List<LegionFactionsDat>>? byMiscAnimatedKey1;
    private Dictionary<int, List<LegionFactionsDat>>? byMiscAnimatedKey2;
    private Dictionary<int, List<LegionFactionsDat>>? byMiscAnimatedKey3;
    private Dictionary<int, List<LegionFactionsDat>>? byAchievementItemsKeys1;
    private Dictionary<int, List<LegionFactionsDat>>? byMiscAnimatedKey4;
    private Dictionary<int, List<LegionFactionsDat>>? byMiscAnimatedKey5;
    private Dictionary<float, List<LegionFactionsDat>>? byUnknown148;
    private Dictionary<float, List<LegionFactionsDat>>? byUnknown152;
    private Dictionary<int, List<LegionFactionsDat>>? byAchievementItemsKeys2;
    private Dictionary<int, List<LegionFactionsDat>>? byStatsKey;
    private Dictionary<string, List<LegionFactionsDat>>? byShard;
    private Dictionary<string, List<LegionFactionsDat>>? byRewardJewelArt;

    /// <summary>
    /// Initializes a new instance of the <see cref="LegionFactionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LegionFactionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out LegionFactionsDat? item)
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
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
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LegionFactionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<string, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpawnWeight(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (bySpawnWeight is null)
        {
            bySpawnWeight = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpawnWeight;

                if (!bySpawnWeight.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpawnWeight.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpawnWeight.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.LegionBalancePerLevelKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLegionBalancePerLevelKey(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLegionBalancePerLevelKey(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.LegionBalancePerLevelKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLegionBalancePerLevelKey(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byLegionBalancePerLevelKey is null)
        {
            byLegionBalancePerLevelKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.LegionBalancePerLevelKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLegionBalancePerLevelKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLegionBalancePerLevelKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLegionBalancePerLevelKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byLegionBalancePerLevelKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByLegionBalancePerLevelKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLegionBalancePerLevelKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown28(float? key, out LegionFactionsDat? item)
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown28"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown28(float? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
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
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byUnknown28"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LegionFactionsDat>> GetManyToManyByUnknown28(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<float, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown28(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(float? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(float? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;

                if (!byUnknown32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LegionFactionsDat>> GetManyToManyByUnknown32(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<float, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.BuffVisualsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualsKey(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisualsKey(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.BuffVisualsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualsKey(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byBuffVisualsKey is null)
        {
            byBuffVisualsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisualsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffVisualsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffVisualsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffVisualsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byBuffVisualsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByBuffVisualsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey1(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey1(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey1(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byMiscAnimatedKey1 is null)
        {
            byMiscAnimatedKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byMiscAnimatedKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByMiscAnimatedKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey2(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey2(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey2(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byMiscAnimatedKey2 is null)
        {
            byMiscAnimatedKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byMiscAnimatedKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByMiscAnimatedKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey3(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey3(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey3(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byMiscAnimatedKey3 is null)
        {
            byMiscAnimatedKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byMiscAnimatedKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByMiscAnimatedKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.AchievementItemsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys1(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys1(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.AchievementItemsKeys1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys1(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byAchievementItemsKeys1 is null)
        {
            byAchievementItemsKeys1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys1;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys1.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys1.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byAchievementItemsKeys1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByAchievementItemsKeys1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey4(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey4(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey4(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byMiscAnimatedKey4 is null)
        {
            byMiscAnimatedKey4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byMiscAnimatedKey4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByMiscAnimatedKey4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey5(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey5(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.MiscAnimatedKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey5(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byMiscAnimatedKey5 is null)
        {
            byMiscAnimatedKey5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey5;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey5.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey5.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byMiscAnimatedKey5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByMiscAnimatedKey5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(float? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown148(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(float? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;

                if (!byUnknown148.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LegionFactionsDat>> GetManyToManyByUnknown148(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<float, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown152(float? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown152(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Unknown152"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown152(float? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byUnknown152 is null)
        {
            byUnknown152 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown152;

                if (!byUnknown152.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown152.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown152.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byUnknown152"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LegionFactionsDat>> GetManyToManyByUnknown152(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<float, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown152(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.AchievementItemsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys2(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAchievementItemsKeys2(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.AchievementItemsKeys2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys2(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byAchievementItemsKeys2 is null)
        {
            byAchievementItemsKeys2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKeys2;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKeys2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKeys2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKeys2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byAchievementItemsKeys2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByAchievementItemsKeys2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionFactionsDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<int, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Shard"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShard(string? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShard(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.Shard"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShard(string? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byShard is null)
        {
            byShard = new();
            foreach (var item in Items)
            {
                var itemKey = item.Shard;

                if (!byShard.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShard.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShard.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byShard"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LegionFactionsDat>> GetManyToManyByShard(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<string, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShard(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.RewardJewelArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRewardJewelArt(string? key, out LegionFactionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRewardJewelArt(key, out var items))
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
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.RewardJewelArt"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRewardJewelArt(string? key, out IReadOnlyList<LegionFactionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        if (byRewardJewelArt is null)
        {
            byRewardJewelArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.RewardJewelArt;

                if (!byRewardJewelArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRewardJewelArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRewardJewelArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<LegionFactionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionFactionsDat"/> with <see cref="LegionFactionsDat.byRewardJewelArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, LegionFactionsDat>> GetManyToManyByRewardJewelArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, LegionFactionsDat>>();
        }

        var items = new List<ResultItem<string, LegionFactionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRewardJewelArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, LegionFactionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LegionFactionsDat[] Load()
    {
        const string filePath = "Data/LegionFactions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionFactionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LegionBalancePerLevelKey
            (var legionbalanceperlevelkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey2
            (var miscanimatedkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey3
            (var miscanimatedkey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading AchievementItemsKeys1
            (var tempachievementitemskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeys1Loading = tempachievementitemskeys1Loading.AsReadOnly();

            // loading MiscAnimatedKey4
            (var miscanimatedkey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey5
            (var miscanimatedkey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading AchievementItemsKeys2
            (var tempachievementitemskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeys2Loading = tempachievementitemskeys2Loading.AsReadOnly();

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Shard
            (var shardLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardJewelArt
            (var rewardjewelartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionFactionsDat()
            {
                Id = idLoading,
                SpawnWeight = spawnweightLoading,
                LegionBalancePerLevelKey = legionbalanceperlevelkeyLoading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                BuffVisualsKey = buffvisualskeyLoading,
                MiscAnimatedKey1 = miscanimatedkey1Loading,
                MiscAnimatedKey2 = miscanimatedkey2Loading,
                MiscAnimatedKey3 = miscanimatedkey3Loading,
                AchievementItemsKeys1 = achievementitemskeys1Loading,
                MiscAnimatedKey4 = miscanimatedkey4Loading,
                MiscAnimatedKey5 = miscanimatedkey5Loading,
                Unknown148 = unknown148Loading,
                Unknown152 = unknown152Loading,
                AchievementItemsKeys2 = achievementitemskeys2Loading,
                StatsKey = statskeyLoading,
                Shard = shardLoading,
                RewardJewelArt = rewardjewelartLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
