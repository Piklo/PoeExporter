using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistNPCsDat"/> related data and helper methods.
/// </summary>
public sealed class HeistNPCsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistNPCsDat> Items { get; }

    private Dictionary<int, List<HeistNPCsDat>>? byNPCsKey;
    private Dictionary<int, List<HeistNPCsDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<HeistNPCsDat>>? bySkillLevel_HeistJobsKeys;
    private Dictionary<string, List<HeistNPCsDat>>? byPortraitFile;
    private Dictionary<int, List<HeistNPCsDat>>? byHeistNPCStatsKeys;
    private Dictionary<float, List<HeistNPCsDat>>? byStatValues;
    private Dictionary<float, List<HeistNPCsDat>>? byUnknown88;
    private Dictionary<int, List<HeistNPCsDat>>? bySkillLevel_Values;
    private Dictionary<string, List<HeistNPCsDat>>? byName;
    private Dictionary<string, List<HeistNPCsDat>>? bySilhouetteFile;
    private Dictionary<int, List<HeistNPCsDat>>? byUnknown124;
    private Dictionary<int, List<HeistNPCsDat>>? byUnknown128;
    private Dictionary<int, List<HeistNPCsDat>>? byHeistNPCsKey;
    private Dictionary<float, List<HeistNPCsDat>>? byStatValues2;
    private Dictionary<int, List<HeistNPCsDat>>? byAlly_NPCsKey;
    private Dictionary<string, List<HeistNPCsDat>>? byActiveNPCIcon;
    private Dictionary<int, List<HeistNPCsDat>>? byHeistJobsKey;
    private Dictionary<int, List<HeistNPCsDat>>? byEquip_AchievementItemsKeys;
    private Dictionary<string, List<HeistNPCsDat>>? byAOFile;
    private Dictionary<int, List<HeistNPCsDat>>? byUnknown220;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistNPCsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistNPCsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByNPCsKey(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByNPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByNPCsKey(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byNPCsKey is null)
        {
            byNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.NPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byNPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byNPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out HeistNPCsDat? item)
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
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
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.SkillLevel_HeistJobsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillLevel_HeistJobsKeys(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillLevel_HeistJobsKeys(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.SkillLevel_HeistJobsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillLevel_HeistJobsKeys(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (bySkillLevel_HeistJobsKeys is null)
        {
            bySkillLevel_HeistJobsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillLevel_HeistJobsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!bySkillLevel_HeistJobsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySkillLevel_HeistJobsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySkillLevel_HeistJobsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.bySkillLevel_HeistJobsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyBySkillLevel_HeistJobsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillLevel_HeistJobsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.PortraitFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPortraitFile(string? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPortraitFile(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.PortraitFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPortraitFile(string? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byPortraitFile is null)
        {
            byPortraitFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.PortraitFile;

                if (!byPortraitFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPortraitFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPortraitFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byPortraitFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistNPCsDat>> GetManyToManyByPortraitFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPortraitFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.HeistNPCStatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistNPCStatsKeys(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistNPCStatsKeys(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.HeistNPCStatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistNPCStatsKeys(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byHeistNPCStatsKeys is null)
        {
            byHeistNPCStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistNPCStatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byHeistNPCStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byHeistNPCStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byHeistNPCStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byHeistNPCStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByHeistNPCStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistNPCStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValues(float? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValues(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValues(float? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byStatValues is null)
        {
            byStatValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValues;
                foreach (var listKey in itemKey)
                {
                    if (!byStatValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byStatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistNPCsDat>> GetManyToManyByStatValues(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<float, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(float? key, out HeistNPCsDat? item)
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(float? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
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
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistNPCsDat>> GetManyToManyByUnknown88(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<float, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.SkillLevel_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySkillLevel_Values(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySkillLevel_Values(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.SkillLevel_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySkillLevel_Values(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (bySkillLevel_Values is null)
        {
            bySkillLevel_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.SkillLevel_Values;
                foreach (var listKey in itemKey)
                {
                    if (!bySkillLevel_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        bySkillLevel_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!bySkillLevel_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.bySkillLevel_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyBySkillLevel_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySkillLevel_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out HeistNPCsDat? item)
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
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
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistNPCsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.SilhouetteFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySilhouetteFile(string? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySilhouetteFile(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.SilhouetteFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySilhouetteFile(string? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (bySilhouetteFile is null)
        {
            bySilhouetteFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.SilhouetteFile;

                if (!bySilhouetteFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySilhouetteFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySilhouetteFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.bySilhouetteFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistNPCsDat>> GetManyToManyBySilhouetteFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySilhouetteFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown124(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;

                if (!byUnknown124.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown124.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown128(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown128(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown128(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byUnknown128 is null)
        {
            byUnknown128 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown128;

                if (!byUnknown128.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown128.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown128.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byUnknown128"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByUnknown128(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown128(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.HeistNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistNPCsKey(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistNPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.HeistNPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistNPCsKey(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byHeistNPCsKey is null)
        {
            byHeistNPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistNPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistNPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistNPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistNPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byHeistNPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByHeistNPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistNPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.StatValues2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValues2(float? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValues2(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.StatValues2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValues2(float? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byStatValues2 is null)
        {
            byStatValues2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValues2;
                foreach (var listKey in itemKey)
                {
                    if (!byStatValues2.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatValues2.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatValues2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byStatValues2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistNPCsDat>> GetManyToManyByStatValues2(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<float, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValues2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Ally_NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAlly_NPCsKey(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAlly_NPCsKey(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Ally_NPCsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAlly_NPCsKey(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byAlly_NPCsKey is null)
        {
            byAlly_NPCsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Ally_NPCsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byAlly_NPCsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byAlly_NPCsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byAlly_NPCsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byAlly_NPCsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByAlly_NPCsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAlly_NPCsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.ActiveNPCIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByActiveNPCIcon(string? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByActiveNPCIcon(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.ActiveNPCIcon"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByActiveNPCIcon(string? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byActiveNPCIcon is null)
        {
            byActiveNPCIcon = new();
            foreach (var item in Items)
            {
                var itemKey = item.ActiveNPCIcon;

                if (!byActiveNPCIcon.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byActiveNPCIcon.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byActiveNPCIcon.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byActiveNPCIcon"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistNPCsDat>> GetManyToManyByActiveNPCIcon(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByActiveNPCIcon(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistJobsKey(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistJobsKey(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.HeistJobsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistJobsKey(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byHeistJobsKey is null)
        {
            byHeistJobsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistJobsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistJobsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistJobsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistJobsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byHeistJobsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByHeistJobsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistJobsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Equip_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEquip_AchievementItemsKeys(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEquip_AchievementItemsKeys(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Equip_AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEquip_AchievementItemsKeys(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byEquip_AchievementItemsKeys is null)
        {
            byEquip_AchievementItemsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Equip_AchievementItemsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byEquip_AchievementItemsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byEquip_AchievementItemsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byEquip_AchievementItemsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byEquip_AchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByEquip_AchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEquip_AchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;

                if (!byAOFile.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAOFile.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, HeistNPCsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<string, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown220(int? key, out HeistNPCsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown220(key, out var items))
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
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown220(int? key, out IReadOnlyList<HeistNPCsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        if (byUnknown220 is null)
        {
            byUnknown220 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown220;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown220.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown220.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown220.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistNPCsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistNPCsDat"/> with <see cref="HeistNPCsDat.byUnknown220"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistNPCsDat>> GetManyToManyByUnknown220(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistNPCsDat>>();
        }

        var items = new List<ResultItem<int, HeistNPCsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown220(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistNPCsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistNPCsDat[] Load()
    {
        const string filePath = "Data/HeistNPCs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCsKey
            (var npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SkillLevel_HeistJobsKeys
            (var tempskilllevel_heistjobskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skilllevel_heistjobskeysLoading = tempskilllevel_heistjobskeysLoading.AsReadOnly();

            // loading PortraitFile
            (var portraitfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistNPCStatsKeys
            (var tempheistnpcstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistnpcstatskeysLoading = tempheistnpcstatskeysLoading.AsReadOnly();

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading SkillLevel_Values
            (var tempskilllevel_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var skilllevel_valuesLoading = tempskilllevel_valuesLoading.AsReadOnly();

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SilhouetteFile
            (var silhouettefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistNPCsKey
            (var heistnpcskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading StatValues2
            (var tempstatvalues2Loading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var statvalues2Loading = tempstatvalues2Loading.AsReadOnly();

            // loading Ally_NPCsKey
            (var ally_npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ActiveNPCIcon
            (var activenpciconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Equip_AchievementItemsKeys
            (var tempequip_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var equip_achievementitemskeysLoading = tempequip_achievementitemskeysLoading.AsReadOnly();

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCsDat()
            {
                NPCsKey = npcskeyLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                SkillLevel_HeistJobsKeys = skilllevel_heistjobskeysLoading,
                PortraitFile = portraitfileLoading,
                HeistNPCStatsKeys = heistnpcstatskeysLoading,
                StatValues = statvaluesLoading,
                Unknown88 = unknown88Loading,
                SkillLevel_Values = skilllevel_valuesLoading,
                Name = nameLoading,
                SilhouetteFile = silhouettefileLoading,
                Unknown124 = unknown124Loading,
                Unknown128 = unknown128Loading,
                HeistNPCsKey = heistnpcskeyLoading,
                StatValues2 = statvalues2Loading,
                Ally_NPCsKey = ally_npcskeyLoading,
                ActiveNPCIcon = activenpciconLoading,
                HeistJobsKey = heistjobskeyLoading,
                Equip_AchievementItemsKeys = equip_achievementitemskeysLoading,
                AOFile = aofileLoading,
                Unknown220 = unknown220Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
