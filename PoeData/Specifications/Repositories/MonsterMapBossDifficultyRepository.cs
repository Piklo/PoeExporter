using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterMapBossDifficultyDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterMapBossDifficultyRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterMapBossDifficultyDat> Items { get; }

    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byMapLevel;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStat1Value;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStat2Value;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStatsKey1;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStatsKey2;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStatsKey3;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStat3Value;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStatsKey4;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStat4Value;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStatsKey5;
    private Dictionary<int, List<MonsterMapBossDifficultyDat>>? byStat5Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterMapBossDifficultyRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterMapBossDifficultyRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.MapLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMapLevel(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMapLevel(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.MapLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMapLevel(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byMapLevel is null)
        {
            byMapLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MapLevel;

                if (!byMapLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMapLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMapLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byMapLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByMapLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMapLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat1Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Value(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat1Value(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat1Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Value(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStat1Value is null)
        {
            byStat1Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat1Value;

                if (!byStat1Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat1Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat1Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStat1Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStat1Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat2Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Value(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat2Value(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat2Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Value(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStat2Value is null)
        {
            byStat2Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat2Value;

                if (!byStat2Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat2Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat2Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStat2Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStat2Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey1(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey1(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey1(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStatsKey1 is null)
        {
            byStatsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStatsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStatsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey2(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey2(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey2(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStatsKey2 is null)
        {
            byStatsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStatsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStatsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey3(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey3(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey3(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStatsKey3 is null)
        {
            byStatsKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStatsKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStatsKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat3Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat3Value(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat3Value(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat3Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat3Value(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStat3Value is null)
        {
            byStat3Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat3Value;

                if (!byStat3Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat3Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat3Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStat3Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStat3Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat3Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey4(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey4(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey4(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStatsKey4 is null)
        {
            byStatsKey4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStatsKey4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStatsKey4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat4Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat4Value(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat4Value(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat4Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat4Value(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStat4Value is null)
        {
            byStat4Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat4Value;

                if (!byStat4Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat4Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat4Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStat4Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStat4Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat4Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey5(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey5(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.StatsKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey5(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStatsKey5 is null)
        {
            byStatsKey5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey5;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey5.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey5.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStatsKey5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStatsKey5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat5Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat5Value(int? key, out MonsterMapBossDifficultyDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat5Value(key, out var items))
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
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.Stat5Value"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat5Value(int? key, out IReadOnlyList<MonsterMapBossDifficultyDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        if (byStat5Value is null)
        {
            byStat5Value = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat5Value;

                if (!byStat5Value.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat5Value.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat5Value.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterMapBossDifficultyDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterMapBossDifficultyDat"/> with <see cref="MonsterMapBossDifficultyDat.byStat5Value"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterMapBossDifficultyDat>> GetManyToManyByStat5Value(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterMapBossDifficultyDat>>();
        }

        var items = new List<ResultItem<int, MonsterMapBossDifficultyDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat5Value(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterMapBossDifficultyDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterMapBossDifficultyDat[] Load()
    {
        const string filePath = "Data/MonsterMapBossDifficulty.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterMapBossDifficultyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapLevel
            (var maplevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat1Value
            (var stat1valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Value
            (var stat2valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey1
            (var statskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey2
            (var statskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey3
            (var statskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stat3Value
            (var stat3valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey4
            (var statskey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stat4Value
            (var stat4valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey5
            (var statskey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Stat5Value
            (var stat5valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterMapBossDifficultyDat()
            {
                MapLevel = maplevelLoading,
                Stat1Value = stat1valueLoading,
                Stat2Value = stat2valueLoading,
                StatsKey1 = statskey1Loading,
                StatsKey2 = statskey2Loading,
                StatsKey3 = statskey3Loading,
                Stat3Value = stat3valueLoading,
                StatsKey4 = statskey4Loading,
                Stat4Value = stat4valueLoading,
                StatsKey5 = statskey5Loading,
                Stat5Value = stat5valueLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
