using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BetrayalTargetsDat"/> related data and helper methods.
/// </summary>
public sealed class BetrayalTargetsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BetrayalTargetsDat> Items { get; }

    private Dictionary<string, List<BetrayalTargetsDat>>? byId;
    private Dictionary<int, List<BetrayalTargetsDat>>? byBetrayalRanksKey;
    private Dictionary<int, List<BetrayalTargetsDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<BetrayalTargetsDat>>? byBetrayalJobsKey;
    private Dictionary<string, List<BetrayalTargetsDat>>? byArt;
    private Dictionary<bool, List<BetrayalTargetsDat>>? byUnknown64;
    private Dictionary<int, List<BetrayalTargetsDat>>? byItemClasses;
    private Dictionary<string, List<BetrayalTargetsDat>>? byFullName;
    private Dictionary<string, List<BetrayalTargetsDat>>? bySafehouse_ARMFile;
    private Dictionary<string, List<BetrayalTargetsDat>>? byShortName;
    private Dictionary<int, List<BetrayalTargetsDat>>? byUnknown105;
    private Dictionary<int, List<BetrayalTargetsDat>>? bySafehouseLeader_AcheivementItemsKey;
    private Dictionary<int, List<BetrayalTargetsDat>>? byLevel3_AchievementItemsKey;
    private Dictionary<int, List<BetrayalTargetsDat>>? byUnknown141;
    private Dictionary<int, List<BetrayalTargetsDat>>? byUnknown145;
    private Dictionary<int, List<BetrayalTargetsDat>>? byUnknown149;
    private Dictionary<int, List<BetrayalTargetsDat>>? byUnknown153;
    private Dictionary<string, List<BetrayalTargetsDat>>? byScriptArgument;

    /// <summary>
    /// Initializes a new instance of the <see cref="BetrayalTargetsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BetrayalTargetsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BetrayalTargetsDat? item)
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
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
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTargetsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.BetrayalRanksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalRanksKey(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalRanksKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.BetrayalRanksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalRanksKey(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byBetrayalRanksKey is null)
        {
            byBetrayalRanksKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalRanksKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalRanksKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalRanksKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalRanksKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byBetrayalRanksKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByBetrayalRanksKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalRanksKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byMonsterVarietiesKey is null)
        {
            byMonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBetrayalJobsKey(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBetrayalJobsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.BetrayalJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBetrayalJobsKey(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byBetrayalJobsKey is null)
        {
            byBetrayalJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BetrayalJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBetrayalJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBetrayalJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBetrayalJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byBetrayalJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByBetrayalJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBetrayalJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArt(string? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArt(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArt(string? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byArt is null)
        {
            byArt = new();
            foreach (var item in Items)
            {
                var itemKey = item.Art;

                if (!byArt.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArt.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArt.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byArt"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTargetsDat>> GetManyToManyByArt(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArt(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(bool? key, out BetrayalTargetsDat? item)
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(bool? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
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
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BetrayalTargetsDat>> GetManyToManyByUnknown64(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<bool, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.ItemClasses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByItemClasses(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByItemClasses(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.ItemClasses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByItemClasses(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byItemClasses is null)
        {
            byItemClasses = new();
            foreach (var item in Items)
            {
                var itemKey = item.ItemClasses;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byItemClasses.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byItemClasses.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byItemClasses.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byItemClasses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByItemClasses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByItemClasses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.FullName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFullName(string? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFullName(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.FullName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFullName(string? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byFullName is null)
        {
            byFullName = new();
            foreach (var item in Items)
            {
                var itemKey = item.FullName;

                if (!byFullName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFullName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFullName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byFullName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTargetsDat>> GetManyToManyByFullName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFullName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Safehouse_ARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySafehouse_ARMFile(string? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySafehouse_ARMFile(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Safehouse_ARMFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySafehouse_ARMFile(string? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (bySafehouse_ARMFile is null)
        {
            bySafehouse_ARMFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.Safehouse_ARMFile;

                if (!bySafehouse_ARMFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySafehouse_ARMFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySafehouse_ARMFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.bySafehouse_ARMFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTargetsDat>> GetManyToManyBySafehouse_ARMFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySafehouse_ARMFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.ShortName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShortName(string? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShortName(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.ShortName"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShortName(string? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byShortName is null)
        {
            byShortName = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShortName;

                if (!byShortName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byShortName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byShortName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byShortName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTargetsDat>> GetManyToManyByShortName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShortName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown105(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown105(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown105"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown105(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byUnknown105 is null)
        {
            byUnknown105 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown105;

                if (!byUnknown105.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown105.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown105.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byUnknown105"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByUnknown105(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown105(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.SafehouseLeader_AcheivementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySafehouseLeader_AcheivementItemsKey(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySafehouseLeader_AcheivementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.SafehouseLeader_AcheivementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySafehouseLeader_AcheivementItemsKey(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (bySafehouseLeader_AcheivementItemsKey is null)
        {
            bySafehouseLeader_AcheivementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SafehouseLeader_AcheivementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySafehouseLeader_AcheivementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySafehouseLeader_AcheivementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySafehouseLeader_AcheivementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.bySafehouseLeader_AcheivementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyBySafehouseLeader_AcheivementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySafehouseLeader_AcheivementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Level3_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel3_AchievementItemsKey(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel3_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Level3_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel3_AchievementItemsKey(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byLevel3_AchievementItemsKey is null)
        {
            byLevel3_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level3_AchievementItemsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLevel3_AchievementItemsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLevel3_AchievementItemsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel3_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byLevel3_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByLevel3_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel3_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown141(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown141(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown141(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byUnknown141 is null)
        {
            byUnknown141 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown141;

                if (!byUnknown141.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown141.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown141.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byUnknown141"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByUnknown141(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown141(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown145(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown145(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown145"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown145(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byUnknown145 is null)
        {
            byUnknown145 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown145;

                if (!byUnknown145.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown145.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown145.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byUnknown145"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByUnknown145(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown145(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown149(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown149(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown149(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byUnknown149 is null)
        {
            byUnknown149 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown149;

                if (!byUnknown149.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown149.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown149.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byUnknown149"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByUnknown149(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown149(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown153"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown153(int? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown153(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.Unknown153"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown153(int? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byUnknown153 is null)
        {
            byUnknown153 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown153;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown153.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown153.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown153.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byUnknown153"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BetrayalTargetsDat>> GetManyToManyByUnknown153(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<int, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown153(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.ScriptArgument"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByScriptArgument(string? key, out BetrayalTargetsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByScriptArgument(key, out var items))
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
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.ScriptArgument"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByScriptArgument(string? key, out IReadOnlyList<BetrayalTargetsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        if (byScriptArgument is null)
        {
            byScriptArgument = new();
            foreach (var item in Items)
            {
                var itemKey = item.ScriptArgument;

                if (!byScriptArgument.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byScriptArgument.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byScriptArgument.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BetrayalTargetsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BetrayalTargetsDat"/> with <see cref="BetrayalTargetsDat.byScriptArgument"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BetrayalTargetsDat>> GetManyToManyByScriptArgument(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BetrayalTargetsDat>>();
        }

        var items = new List<ResultItem<string, BetrayalTargetsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByScriptArgument(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BetrayalTargetsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BetrayalTargetsDat[] Load()
    {
        const string filePath = "Data/BetrayalTargets.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalTargetsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BetrayalRanksKey
            (var betrayalrankskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Art
            (var artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ItemClasses
            (var itemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FullName
            (var fullnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Safehouse_ARMFile
            (var safehouse_armfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShortName
            (var shortnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SafehouseLeader_AcheivementItemsKey
            (var safehouseleader_acheivementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level3_AchievementItemsKey
            (var level3_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown153
            (var unknown153Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ScriptArgument
            (var scriptargumentLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalTargetsDat()
            {
                Id = idLoading,
                BetrayalRanksKey = betrayalrankskeyLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                BetrayalJobsKey = betrayaljobskeyLoading,
                Art = artLoading,
                Unknown64 = unknown64Loading,
                ItemClasses = itemclassesLoading,
                FullName = fullnameLoading,
                Safehouse_ARMFile = safehouse_armfileLoading,
                ShortName = shortnameLoading,
                Unknown105 = unknown105Loading,
                SafehouseLeader_AcheivementItemsKey = safehouseleader_acheivementitemskeyLoading,
                Level3_AchievementItemsKey = level3_achievementitemskeyLoading,
                Unknown141 = unknown141Loading,
                Unknown145 = unknown145Loading,
                Unknown149 = unknown149Loading,
                Unknown153 = unknown153Loading,
                ScriptArgument = scriptargumentLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
