using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AlternatePassiveSkillsDat"/> related data and helper methods.
/// </summary>
public sealed class AlternatePassiveSkillsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AlternatePassiveSkillsDat> Items { get; }

    private Dictionary<string, List<AlternatePassiveSkillsDat>>? byId;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byAlternateTreeVersionsKey;
    private Dictionary<string, List<AlternatePassiveSkillsDat>>? byName;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byPassiveType;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byStatsKeys;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byStat1Min;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byStat1Max;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byStat2Min;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byStat2Max;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown80;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown84;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown88;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown92;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown96;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown100;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown104;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown108;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? bySpawnWeight;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown116;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byRandomMin;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byRandomMax;
    private Dictionary<string, List<AlternatePassiveSkillsDat>>? byFlavourText;
    private Dictionary<string, List<AlternatePassiveSkillsDat>>? byDDSIcon;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byAchievementItemsKeys;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown160;
    private Dictionary<int, List<AlternatePassiveSkillsDat>>? byUnknown164;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlternatePassiveSkillsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AlternatePassiveSkillsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternatePassiveSkillsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.AlternateTreeVersionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlternateTreeVersionsKey(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlternateTreeVersionsKey(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.AlternateTreeVersionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlternateTreeVersionsKey(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byAlternateTreeVersionsKey is null)
        {
            byAlternateTreeVersionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AlternateTreeVersionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAlternateTreeVersionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAlternateTreeVersionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAlternateTreeVersionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byAlternateTreeVersionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByAlternateTreeVersionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlternateTreeVersionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternatePassiveSkillsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.PassiveType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPassiveType(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPassiveType(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.PassiveType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPassiveType(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byPassiveType is null)
        {
            byPassiveType = new();
            foreach (var item in Items)
            {
                var itemKey = item.PassiveType;
                foreach (var listKey in itemKey)
                {
                    if (!byPassiveType.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byPassiveType.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byPassiveType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byPassiveType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByPassiveType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPassiveType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byStatsKeys is null)
        {
            byStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat1Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Min(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat1Min(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat1Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Min(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byStat1Min is null)
        {
            byStat1Min = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat1Min;

                if (!byStat1Min.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat1Min.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat1Min.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byStat1Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByStat1Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat1Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Max(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat1Max(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat1Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Max(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byStat1Max is null)
        {
            byStat1Max = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat1Max;

                if (!byStat1Max.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat1Max.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat1Max.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byStat1Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByStat1Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat2Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Min(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat2Min(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat2Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Min(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byStat2Min is null)
        {
            byStat2Min = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat2Min;

                if (!byStat2Min.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat2Min.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat2Min.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byStat2Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByStat2Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat2Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Max(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat2Max(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Stat2Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Max(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byStat2Max is null)
        {
            byStat2Max = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat2Max;

                if (!byStat2Max.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat2Max.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat2Max.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byStat2Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByStat2Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;

                if (!byUnknown80.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown84(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;

                if (!byUnknown84.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown84.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown88(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown92(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;

                if (!byUnknown92.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;

                if (!byUnknown96.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown100(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown108(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;

                if (!byUnknown108.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight(int? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.SpawnWeight"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.bySpawnWeight"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyBySpawnWeight(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(int? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown116(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.RandomMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRandomMin(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRandomMin(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.RandomMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRandomMin(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byRandomMin is null)
        {
            byRandomMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.RandomMin;

                if (!byRandomMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRandomMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRandomMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byRandomMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByRandomMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRandomMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.RandomMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRandomMax(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRandomMax(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.RandomMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRandomMax(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byRandomMax is null)
        {
            byRandomMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.RandomMax;

                if (!byRandomMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRandomMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRandomMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byRandomMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByRandomMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRandomMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFlavourText(string? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.FlavourText"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFlavourText(string? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byFlavourText"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternatePassiveSkillsDat>> GetManyToManyByFlavourText(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFlavourText(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.DDSIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDDSIcon(string? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDDSIcon(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.DDSIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDDSIcon(string? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byDDSIcon is null)
        {
            byDDSIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.DDSIcon;

                if (!byDDSIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDDSIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDDSIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byDDSIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AlternatePassiveSkillsDat>> GetManyToManyByDDSIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<string, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDDSIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown160(int? key, out AlternatePassiveSkillsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown160(key, out var items))
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown160(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        if (byUnknown160 is null)
        {
            byUnknown160 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown160;

                if (!byUnknown160.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown160.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown160.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown160"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown160(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown160(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown164(int? key, out AlternatePassiveSkillsDat? item)
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
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown164(int? key, out IReadOnlyList<AlternatePassiveSkillsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
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

        if (!byUnknown164.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AlternatePassiveSkillsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AlternatePassiveSkillsDat"/> with <see cref="AlternatePassiveSkillsDat.byUnknown164"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AlternatePassiveSkillsDat>> GetManyToManyByUnknown164(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AlternatePassiveSkillsDat>>();
        }

        var items = new List<ResultItem<int, AlternatePassiveSkillsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown164(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AlternatePassiveSkillsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AlternatePassiveSkillsDat[] Load()
    {
        const string filePath = "Data/AlternatePassiveSkills.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternatePassiveSkillsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AlternateTreeVersionsKey
            (var alternatetreeversionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PassiveType
            (var temppassivetypeLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var passivetypeLoading = temppassivetypeLoading.AsReadOnly();

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Stat1Min
            (var stat1minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat1Max
            (var stat1maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Min
            (var stat2minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Max
            (var stat2maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RandomMin
            (var randomminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RandomMax
            (var randommaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DDSIcon
            (var ddsiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternatePassiveSkillsDat()
            {
                Id = idLoading,
                AlternateTreeVersionsKey = alternatetreeversionskeyLoading,
                Name = nameLoading,
                PassiveType = passivetypeLoading,
                StatsKeys = statskeysLoading,
                Stat1Min = stat1minLoading,
                Stat1Max = stat1maxLoading,
                Stat2Min = stat2minLoading,
                Stat2Max = stat2maxLoading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown100 = unknown100Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                SpawnWeight = spawnweightLoading,
                Unknown116 = unknown116Loading,
                RandomMin = randomminLoading,
                RandomMax = randommaxLoading,
                FlavourText = flavourtextLoading,
                DDSIcon = ddsiconLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown160 = unknown160Loading,
                Unknown164 = unknown164Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
