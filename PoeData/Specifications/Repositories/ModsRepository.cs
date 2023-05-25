using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ModsDat"/> related data and helper methods.
/// </summary>
public sealed class ModsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ModsDat> Items { get; }

    private Dictionary<string, List<ModsDat>>? byId;
    private Dictionary<int, List<ModsDat>>? byHASH16;
    private Dictionary<int, List<ModsDat>>? byModTypeKey;
    private Dictionary<int, List<ModsDat>>? byLevel;
    private Dictionary<int, List<ModsDat>>? byStatsKey1;
    private Dictionary<int, List<ModsDat>>? byStatsKey2;
    private Dictionary<int, List<ModsDat>>? byStatsKey3;
    private Dictionary<int, List<ModsDat>>? byStatsKey4;
    private Dictionary<int, List<ModsDat>>? byDomain;
    private Dictionary<string, List<ModsDat>>? byName;
    private Dictionary<int, List<ModsDat>>? byGenerationType;
    private Dictionary<int, List<ModsDat>>? byFamilies;
    private Dictionary<int, List<ModsDat>>? byStat1Min;
    private Dictionary<int, List<ModsDat>>? byStat1Max;
    private Dictionary<int, List<ModsDat>>? byStat2Min;
    private Dictionary<int, List<ModsDat>>? byStat2Max;
    private Dictionary<int, List<ModsDat>>? byStat3Min;
    private Dictionary<int, List<ModsDat>>? byStat3Max;
    private Dictionary<int, List<ModsDat>>? byStat4Min;
    private Dictionary<int, List<ModsDat>>? byStat4Max;
    private Dictionary<int, List<ModsDat>>? bySpawnWeight_TagsKeys;
    private Dictionary<int, List<ModsDat>>? bySpawnWeight_Values;
    private Dictionary<int, List<ModsDat>>? byTagsKeys;
    private Dictionary<int, List<ModsDat>>? byGrantedEffectsPerLevelKeys;
    private Dictionary<int, List<ModsDat>>? byUnknown224;
    private Dictionary<string, List<ModsDat>>? byMonsterMetadata;
    private Dictionary<int, List<ModsDat>>? byMonsterKillAchievements;
    private Dictionary<int, List<ModsDat>>? byChestModType;
    private Dictionary<int, List<ModsDat>>? byStat5Min;
    private Dictionary<int, List<ModsDat>>? byStat5Max;
    private Dictionary<int, List<ModsDat>>? byStatsKey5;
    private Dictionary<int, List<ModsDat>>? byFullAreaClear_AchievementItemsKey;
    private Dictionary<int, List<ModsDat>>? byAchievementItemsKey;
    private Dictionary<int, List<ModsDat>>? byGenerationWeight_TagsKeys;
    private Dictionary<int, List<ModsDat>>? byGenerationWeight_Values;
    private Dictionary<int, List<ModsDat>>? byModifyMapsAchievements;
    private Dictionary<bool, List<ModsDat>>? byIsEssenceOnlyModifier;
    private Dictionary<int, List<ModsDat>>? byStat6Min;
    private Dictionary<int, List<ModsDat>>? byStat6Max;
    private Dictionary<int, List<ModsDat>>? byStatsKey6;
    private Dictionary<int, List<ModsDat>>? byMaxLevel;
    private Dictionary<bool, List<ModsDat>>? byUnknown413;
    private Dictionary<int, List<ModsDat>>? byCraftingItemClassRestrictions;
    private Dictionary<string, List<ModsDat>>? byMonsterOnDeath;
    private Dictionary<int, List<ModsDat>>? byUnknown438;
    private Dictionary<int, List<ModsDat>>? byUnknown442;
    private Dictionary<int, List<ModsDat>>? byHeist_SubStatValue1;
    private Dictionary<int, List<ModsDat>>? byHeist_SubStatValue2;
    private Dictionary<int, List<ModsDat>>? byHeist_StatsKey0;
    private Dictionary<int, List<ModsDat>>? byHeist_StatsKey1;
    private Dictionary<int, List<ModsDat>>? byHeist_AddStatValue1;
    private Dictionary<int, List<ModsDat>>? byHeist_AddStatValue2;
    private Dictionary<int, List<ModsDat>>? byInfluenceTypes;
    private Dictionary<int, List<ModsDat>>? byImplicitTagsKeys;
    private Dictionary<bool, List<ModsDat>>? byUnknown526;
    private Dictionary<int, List<ModsDat>>? byUnknown527;
    private Dictionary<int, List<ModsDat>>? byUnknown531;
    private Dictionary<int, List<ModsDat>>? byUnknown535;
    private Dictionary<int, List<ModsDat>>? byUnknown539;
    private Dictionary<int, List<ModsDat>>? byUnknown543;
    private Dictionary<int, List<ModsDat>>? byUnknown547;
    private Dictionary<int, List<ModsDat>>? byUnknown551;
    private Dictionary<int, List<ModsDat>>? byUnknown555;
    private Dictionary<int, List<ModsDat>>? byUnknown559;
    private Dictionary<int, List<ModsDat>>? byUnknown563;
    private Dictionary<int, List<ModsDat>>? byUnknown567;
    private Dictionary<int, List<ModsDat>>? byUnknown571;
    private Dictionary<int, List<ModsDat>>? byUnknown575;
    private Dictionary<int, List<ModsDat>>? byUnknown579;
    private Dictionary<int, List<ModsDat>>? byUnknown583;
    private Dictionary<int, List<ModsDat>>? byUnknown587;
    private Dictionary<int, List<ModsDat>>? byBuffTemplate;
    private Dictionary<int, List<ModsDat>>? byArchnemesisMinionMod;
    private Dictionary<int, List<ModsDat>>? byHASH32;
    private Dictionary<int, List<ModsDat>>? byUnknown619;
    private Dictionary<int, List<ModsDat>>? byUnknown635;
    private Dictionary<int, List<ModsDat>>? byUnknown639;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ModsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ModsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ModsDat>>();
        }

        var items = new List<ResultItem<string, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH16(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.HASH16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH16(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHASH16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHASH16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ModTypeKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModTypeKey(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModTypeKey(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ModTypeKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModTypeKey(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byModTypeKey is null)
        {
            byModTypeKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModTypeKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byModTypeKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byModTypeKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byModTypeKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byModTypeKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByModTypeKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModTypeKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLevel(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byLevel is null)
        {
            byLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.Level;

                if (!byLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey1(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey1(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStatsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStatsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey2(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey2(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStatsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStatsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey3(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey3(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStatsKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStatsKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey4(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey4(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStatsKey4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStatsKey4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Domain"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDomain(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDomain(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Domain"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDomain(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byDomain is null)
        {
            byDomain = new();
            foreach (var item in Items)
            {
                var itemKey = item.Domain;

                if (!byDomain.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDomain.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDomain.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byDomain"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByDomain(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDomain(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ModsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ModsDat>>();
        }

        var items = new List<ResultItem<string, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GenerationType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGenerationType(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGenerationType(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GenerationType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGenerationType(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byGenerationType is null)
        {
            byGenerationType = new();
            foreach (var item in Items)
            {
                var itemKey = item.GenerationType;

                if (!byGenerationType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGenerationType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGenerationType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byGenerationType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByGenerationType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGenerationType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Families"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFamilies(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFamilies(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Families"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFamilies(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byFamilies is null)
        {
            byFamilies = new();
            foreach (var item in Items)
            {
                var itemKey = item.Families;
                foreach (var listKey in itemKey)
                {
                    if (!byFamilies.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFamilies.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFamilies.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byFamilies"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByFamilies(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFamilies(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat1Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Min(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat1Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Min(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat1Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat1Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat1Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat1Max(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat1Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat1Max(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat1Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat1Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat1Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat2Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Min(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat2Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Min(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat2Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat2Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat2Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat2Max(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat2Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat2Max(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat2Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat2Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat2Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat3Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat3Min(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat3Min(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat3Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat3Min(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat3Min is null)
        {
            byStat3Min = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat3Min;

                if (!byStat3Min.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat3Min.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat3Min.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat3Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat3Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat3Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat3Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat3Max(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat3Max(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat3Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat3Max(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat3Max is null)
        {
            byStat3Max = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat3Max;

                if (!byStat3Max.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat3Max.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat3Max.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat3Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat3Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat3Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat4Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat4Min(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat4Min(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat4Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat4Min(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat4Min is null)
        {
            byStat4Min = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat4Min;

                if (!byStat4Min.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat4Min.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat4Min.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat4Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat4Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat4Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat4Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat4Max(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat4Max(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat4Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat4Max(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat4Max is null)
        {
            byStat4Max = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat4Max;

                if (!byStat4Max.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat4Max.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat4Max.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat4Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat4Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat4Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_TagsKeys(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.SpawnWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_TagsKeys(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.bySpawnWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyBySpawnWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpawnWeight_Values(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.SpawnWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpawnWeight_Values(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.bySpawnWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyBySpawnWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpawnWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTagsKeys(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTagsKeys(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTagsKeys(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byTagsKeys is null)
        {
            byTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GrantedEffectsPerLevelKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffectsPerLevelKeys(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffectsPerLevelKeys(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GrantedEffectsPerLevelKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffectsPerLevelKeys(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byGrantedEffectsPerLevelKeys is null)
        {
            byGrantedEffectsPerLevelKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffectsPerLevelKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byGrantedEffectsPerLevelKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGrantedEffectsPerLevelKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGrantedEffectsPerLevelKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byGrantedEffectsPerLevelKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByGrantedEffectsPerLevelKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffectsPerLevelKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown224"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown224(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown224(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown224"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown224(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown224 is null)
        {
            byUnknown224 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown224;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown224.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown224.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown224.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown224"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown224(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown224(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MonsterMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterMetadata(string? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterMetadata(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MonsterMetadata"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterMetadata(string? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byMonsterMetadata is null)
        {
            byMonsterMetadata = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterMetadata;

                if (!byMonsterMetadata.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterMetadata.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterMetadata.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byMonsterMetadata"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ModsDat>> GetManyToManyByMonsterMetadata(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ModsDat>>();
        }

        var items = new List<ResultItem<string, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterMetadata(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MonsterKillAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterKillAchievements(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterKillAchievements(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MonsterKillAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterKillAchievements(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byMonsterKillAchievements is null)
        {
            byMonsterKillAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterKillAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byMonsterKillAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMonsterKillAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMonsterKillAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byMonsterKillAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByMonsterKillAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterKillAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ChestModType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChestModType(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChestModType(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ChestModType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChestModType(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byChestModType is null)
        {
            byChestModType = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChestModType;
                foreach (var listKey in itemKey)
                {
                    if (!byChestModType.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byChestModType.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byChestModType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byChestModType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByChestModType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChestModType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat5Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat5Min(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat5Min(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat5Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat5Min(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat5Min is null)
        {
            byStat5Min = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat5Min;

                if (!byStat5Min.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat5Min.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat5Min.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat5Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat5Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat5Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat5Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat5Max(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat5Max(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat5Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat5Max(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat5Max is null)
        {
            byStat5Max = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat5Max;

                if (!byStat5Max.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat5Max.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat5Max.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat5Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat5Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat5Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey5(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey5(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStatsKey5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStatsKey5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.FullAreaClear_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFullAreaClear_AchievementItemsKey(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFullAreaClear_AchievementItemsKey(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.FullAreaClear_AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFullAreaClear_AchievementItemsKey(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byFullAreaClear_AchievementItemsKey is null)
        {
            byFullAreaClear_AchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.FullAreaClear_AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byFullAreaClear_AchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFullAreaClear_AchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFullAreaClear_AchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byFullAreaClear_AchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByFullAreaClear_AchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFullAreaClear_AchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKey(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.AchievementItemsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKey(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byAchievementItemsKey is null)
        {
            byAchievementItemsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.AchievementItemsKey;
                foreach (var listKey in itemKey)
                {
                    if (!byAchievementItemsKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAchievementItemsKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAchievementItemsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byAchievementItemsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByAchievementItemsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GenerationWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGenerationWeight_TagsKeys(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGenerationWeight_TagsKeys(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GenerationWeight_TagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGenerationWeight_TagsKeys(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byGenerationWeight_TagsKeys is null)
        {
            byGenerationWeight_TagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.GenerationWeight_TagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byGenerationWeight_TagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGenerationWeight_TagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGenerationWeight_TagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byGenerationWeight_TagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByGenerationWeight_TagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGenerationWeight_TagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GenerationWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGenerationWeight_Values(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGenerationWeight_Values(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.GenerationWeight_Values"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGenerationWeight_Values(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byGenerationWeight_Values is null)
        {
            byGenerationWeight_Values = new();
            foreach (var item in Items)
            {
                var itemKey = item.GenerationWeight_Values;
                foreach (var listKey in itemKey)
                {
                    if (!byGenerationWeight_Values.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGenerationWeight_Values.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGenerationWeight_Values.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byGenerationWeight_Values"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByGenerationWeight_Values(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGenerationWeight_Values(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ModifyMapsAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByModifyMapsAchievements(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByModifyMapsAchievements(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ModifyMapsAchievements"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByModifyMapsAchievements(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byModifyMapsAchievements is null)
        {
            byModifyMapsAchievements = new();
            foreach (var item in Items)
            {
                var itemKey = item.ModifyMapsAchievements;
                foreach (var listKey in itemKey)
                {
                    if (!byModifyMapsAchievements.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byModifyMapsAchievements.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byModifyMapsAchievements.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byModifyMapsAchievements"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByModifyMapsAchievements(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByModifyMapsAchievements(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.IsEssenceOnlyModifier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsEssenceOnlyModifier(bool? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsEssenceOnlyModifier(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.IsEssenceOnlyModifier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsEssenceOnlyModifier(bool? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byIsEssenceOnlyModifier is null)
        {
            byIsEssenceOnlyModifier = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsEssenceOnlyModifier;

                if (!byIsEssenceOnlyModifier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsEssenceOnlyModifier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsEssenceOnlyModifier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byIsEssenceOnlyModifier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ModsDat>> GetManyToManyByIsEssenceOnlyModifier(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ModsDat>>();
        }

        var items = new List<ResultItem<bool, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsEssenceOnlyModifier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat6Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat6Min(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat6Min(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat6Min"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat6Min(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat6Min is null)
        {
            byStat6Min = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat6Min;

                if (!byStat6Min.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat6Min.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat6Min.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat6Min"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat6Min(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat6Min(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat6Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStat6Max(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStat6Max(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Stat6Max"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStat6Max(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStat6Max is null)
        {
            byStat6Max = new();
            foreach (var item in Items)
            {
                var itemKey = item.Stat6Max;

                if (!byStat6Max.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStat6Max.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStat6Max.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStat6Max"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStat6Max(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStat6Max(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey6(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey6(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.StatsKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey6(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byStatsKey6 is null)
        {
            byStatsKey6 = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey6;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey6.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey6.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey6.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byStatsKey6"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByStatsKey6(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey6(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaxLevel(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MaxLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaxLevel(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byMaxLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByMaxLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaxLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown413"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown413(bool? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown413(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown413"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown413(bool? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown413 is null)
        {
            byUnknown413 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown413;

                if (!byUnknown413.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown413.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown413.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown413"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ModsDat>> GetManyToManyByUnknown413(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ModsDat>>();
        }

        var items = new List<ResultItem<bool, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown413(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.CraftingItemClassRestrictions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCraftingItemClassRestrictions(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCraftingItemClassRestrictions(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.CraftingItemClassRestrictions"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCraftingItemClassRestrictions(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byCraftingItemClassRestrictions is null)
        {
            byCraftingItemClassRestrictions = new();
            foreach (var item in Items)
            {
                var itemKey = item.CraftingItemClassRestrictions;
                foreach (var listKey in itemKey)
                {
                    if (!byCraftingItemClassRestrictions.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCraftingItemClassRestrictions.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCraftingItemClassRestrictions.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byCraftingItemClassRestrictions"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByCraftingItemClassRestrictions(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCraftingItemClassRestrictions(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MonsterOnDeath"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterOnDeath(string? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterOnDeath(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.MonsterOnDeath"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterOnDeath(string? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byMonsterOnDeath is null)
        {
            byMonsterOnDeath = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterOnDeath;

                if (!byMonsterOnDeath.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterOnDeath.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterOnDeath.TryGetValue(key, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byMonsterOnDeath"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ModsDat>> GetManyToManyByMonsterOnDeath(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ModsDat>>();
        }

        var items = new List<ResultItem<string, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterOnDeath(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown438"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown438(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown438(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown438"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown438(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown438 is null)
        {
            byUnknown438 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown438;

                if (!byUnknown438.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown438.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown438.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown438"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown438(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown438(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown442"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown442(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown442(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown442"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown442(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown442 is null)
        {
            byUnknown442 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown442;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown442.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown442.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown442.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown442"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown442(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown442(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_SubStatValue1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeist_SubStatValue1(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeist_SubStatValue1(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_SubStatValue1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeist_SubStatValue1(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHeist_SubStatValue1 is null)
        {
            byHeist_SubStatValue1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Heist_SubStatValue1;

                if (!byHeist_SubStatValue1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeist_SubStatValue1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeist_SubStatValue1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHeist_SubStatValue1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHeist_SubStatValue1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeist_SubStatValue1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_SubStatValue2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeist_SubStatValue2(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeist_SubStatValue2(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_SubStatValue2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeist_SubStatValue2(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHeist_SubStatValue2 is null)
        {
            byHeist_SubStatValue2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Heist_SubStatValue2;

                if (!byHeist_SubStatValue2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeist_SubStatValue2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeist_SubStatValue2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHeist_SubStatValue2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHeist_SubStatValue2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeist_SubStatValue2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_StatsKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeist_StatsKey0(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeist_StatsKey0(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_StatsKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeist_StatsKey0(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHeist_StatsKey0 is null)
        {
            byHeist_StatsKey0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Heist_StatsKey0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeist_StatsKey0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeist_StatsKey0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeist_StatsKey0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHeist_StatsKey0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHeist_StatsKey0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeist_StatsKey0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_StatsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeist_StatsKey1(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeist_StatsKey1(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_StatsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeist_StatsKey1(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHeist_StatsKey1 is null)
        {
            byHeist_StatsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Heist_StatsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeist_StatsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeist_StatsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeist_StatsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHeist_StatsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHeist_StatsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeist_StatsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_AddStatValue1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeist_AddStatValue1(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeist_AddStatValue1(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_AddStatValue1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeist_AddStatValue1(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHeist_AddStatValue1 is null)
        {
            byHeist_AddStatValue1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Heist_AddStatValue1;

                if (!byHeist_AddStatValue1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeist_AddStatValue1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeist_AddStatValue1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHeist_AddStatValue1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHeist_AddStatValue1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeist_AddStatValue1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_AddStatValue2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeist_AddStatValue2(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeist_AddStatValue2(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Heist_AddStatValue2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeist_AddStatValue2(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHeist_AddStatValue2 is null)
        {
            byHeist_AddStatValue2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Heist_AddStatValue2;

                if (!byHeist_AddStatValue2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHeist_AddStatValue2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHeist_AddStatValue2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHeist_AddStatValue2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHeist_AddStatValue2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeist_AddStatValue2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.InfluenceTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInfluenceTypes(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInfluenceTypes(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.InfluenceTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInfluenceTypes(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byInfluenceTypes is null)
        {
            byInfluenceTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.InfluenceTypes;

                if (!byInfluenceTypes.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInfluenceTypes.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInfluenceTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byInfluenceTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByInfluenceTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInfluenceTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ImplicitTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByImplicitTagsKeys(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByImplicitTagsKeys(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ImplicitTagsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByImplicitTagsKeys(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byImplicitTagsKeys is null)
        {
            byImplicitTagsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.ImplicitTagsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byImplicitTagsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byImplicitTagsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byImplicitTagsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byImplicitTagsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByImplicitTagsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByImplicitTagsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown526"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown526(bool? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown526"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown526(bool? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown526"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ModsDat>> GetManyToManyByUnknown526(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ModsDat>>();
        }

        var items = new List<ResultItem<bool, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown526(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown527"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown527(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown527(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown527"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown527(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown527 is null)
        {
            byUnknown527 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown527;

                if (!byUnknown527.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown527.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown527.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown527"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown527(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown527(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown531"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown531(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown531(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown531"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown531(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown531 is null)
        {
            byUnknown531 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown531;

                if (!byUnknown531.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown531.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown531.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown531"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown531(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown531(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown535"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown535(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown535(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown535"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown535(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown535 is null)
        {
            byUnknown535 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown535;

                if (!byUnknown535.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown535.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown535.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown535"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown535(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown535(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown539"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown539(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown539(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown539"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown539(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown539 is null)
        {
            byUnknown539 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown539;

                if (!byUnknown539.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown539.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown539.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown539"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown539(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown539(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown543"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown543(int? key, out ModsDat? item)
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown543"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown543(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
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
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown543"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown543(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown543(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown547"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown547(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown547(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown547"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown547(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown547 is null)
        {
            byUnknown547 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown547;

                if (!byUnknown547.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown547.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown547.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown547"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown547(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown547(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown551"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown551(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown551(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown551"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown551(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown551 is null)
        {
            byUnknown551 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown551;

                if (!byUnknown551.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown551.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown551.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown551"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown551(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown551(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown555"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown555(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown555(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown555"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown555(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown555 is null)
        {
            byUnknown555 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown555;

                if (!byUnknown555.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown555.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown555.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown555"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown555(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown555(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown559"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown559(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown559(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown559"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown559(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown559 is null)
        {
            byUnknown559 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown559;

                if (!byUnknown559.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown559.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown559.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown559"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown559(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown559(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown563"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown563(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown563(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown563"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown563(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown563 is null)
        {
            byUnknown563 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown563;

                if (!byUnknown563.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown563.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown563.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown563"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown563(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown563(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown567"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown567(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown567(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown567"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown567(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown567 is null)
        {
            byUnknown567 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown567;

                if (!byUnknown567.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown567.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown567.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown567"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown567(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown567(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown571"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown571(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown571(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown571"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown571(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown571 is null)
        {
            byUnknown571 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown571;

                if (!byUnknown571.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown571.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown571.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown571"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown571(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown571(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown575"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown575(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown575(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown575"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown575(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown575 is null)
        {
            byUnknown575 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown575;

                if (!byUnknown575.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown575.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown575.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown575"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown575(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown575(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown579"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown579(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown579(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown579"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown579(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown579 is null)
        {
            byUnknown579 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown579;

                if (!byUnknown579.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown579.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown579.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown579"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown579(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown579(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown583"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown583(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown583(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown583"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown583(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown583 is null)
        {
            byUnknown583 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown583;

                if (!byUnknown583.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown583.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown583.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown583"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown583(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown583(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown587"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown587(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown587(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown587"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown587(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown587 is null)
        {
            byUnknown587 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown587;

                if (!byUnknown587.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown587.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown587.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown587"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown587(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown587(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.BuffTemplate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffTemplate(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffTemplate(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.BuffTemplate"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffTemplate(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byBuffTemplate is null)
        {
            byBuffTemplate = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffTemplate;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffTemplate.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffTemplate.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffTemplate.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byBuffTemplate"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByBuffTemplate(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffTemplate(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ArchnemesisMinionMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArchnemesisMinionMod(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArchnemesisMinionMod(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.ArchnemesisMinionMod"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArchnemesisMinionMod(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byArchnemesisMinionMod is null)
        {
            byArchnemesisMinionMod = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArchnemesisMinionMod;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byArchnemesisMinionMod.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byArchnemesisMinionMod.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byArchnemesisMinionMod.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byArchnemesisMinionMod"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByArchnemesisMinionMod(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArchnemesisMinionMod(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHASH32(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHASH32(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.HASH32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHASH32(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byHASH32 is null)
        {
            byHASH32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HASH32;

                if (!byHASH32.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byHASH32.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byHASH32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byHASH32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByHASH32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHASH32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown619"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown619(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown619(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown619"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown619(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown619 is null)
        {
            byUnknown619 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown619;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown619.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown619.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown619.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown619"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown619(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown619(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown635"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown635(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown635(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown635"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown635(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown635 is null)
        {
            byUnknown635 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown635;

                if (!byUnknown635.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown635.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown635.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown635"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown635(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown635(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown639"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown639(int? key, out ModsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown639(key, out var items))
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
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.Unknown639"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown639(int? key, out IReadOnlyList<ModsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        if (byUnknown639 is null)
        {
            byUnknown639 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown639;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown639.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown639.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown639.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ModsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ModsDat"/> with <see cref="ModsDat.byUnknown639"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ModsDat>> GetManyToManyByUnknown639(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ModsDat>>();
        }

        var items = new List<ResultItem<int, ModsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown639(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ModsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ModsDat[] Load()
    {
        const string filePath = "Data/Mods.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModTypeKey
            (var modtypekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey1
            (var statskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey2
            (var statskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey3
            (var statskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey4
            (var statskey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Domain
            (var domainLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GenerationType
            (var generationtypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Families
            (var tempfamiliesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var familiesLoading = tempfamiliesLoading.AsReadOnly();

            // loading Stat1Min
            (var stat1minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat1Max
            (var stat1maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Min
            (var stat2minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Max
            (var stat2maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Min
            (var stat3minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Max
            (var stat3maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat4Min
            (var stat4minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat4Max
            (var stat4maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading GrantedEffectsPerLevelKeys
            (var tempgrantedeffectsperlevelkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectsperlevelkeysLoading = tempgrantedeffectsperlevelkeysLoading.AsReadOnly();

            // loading Unknown224
            (var tempunknown224Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown224Loading = tempunknown224Loading.AsReadOnly();

            // loading MonsterMetadata
            (var monstermetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterKillAchievements
            (var tempmonsterkillachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterkillachievementsLoading = tempmonsterkillachievementsLoading.AsReadOnly();

            // loading ChestModType
            (var tempchestmodtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var chestmodtypeLoading = tempchestmodtypeLoading.AsReadOnly();

            // loading Stat5Min
            (var stat5minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat5Max
            (var stat5maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey5
            (var statskey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FullAreaClear_AchievementItemsKey
            (var tempfullareaclear_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var fullareaclear_achievementitemskeyLoading = tempfullareaclear_achievementitemskeyLoading.AsReadOnly();

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            // loading GenerationWeight_TagsKeys
            (var tempgenerationweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var generationweight_tagskeysLoading = tempgenerationweight_tagskeysLoading.AsReadOnly();

            // loading GenerationWeight_Values
            (var tempgenerationweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var generationweight_valuesLoading = tempgenerationweight_valuesLoading.AsReadOnly();

            // loading ModifyMapsAchievements
            (var tempmodifymapsachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modifymapsachievementsLoading = tempmodifymapsachievementsLoading.AsReadOnly();

            // loading IsEssenceOnlyModifier
            (var isessenceonlymodifierLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Stat6Min
            (var stat6minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat6Max
            (var stat6maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey6
            (var statskey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown413
            (var unknown413Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CraftingItemClassRestrictions
            (var tempcraftingitemclassrestrictionsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclassrestrictionsLoading = tempcraftingitemclassrestrictionsLoading.AsReadOnly();

            // loading MonsterOnDeath
            (var monsterondeathLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown438
            (var unknown438Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown442
            (var tempunknown442Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown442Loading = tempunknown442Loading.AsReadOnly();

            // loading Heist_SubStatValue1
            (var heist_substatvalue1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Heist_SubStatValue2
            (var heist_substatvalue2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Heist_StatsKey0
            (var heist_statskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Heist_StatsKey1
            (var heist_statskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Heist_AddStatValue1
            (var heist_addstatvalue1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Heist_AddStatValue2
            (var heist_addstatvalue2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InfluenceTypes
            (var influencetypesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ImplicitTagsKeys
            (var tempimplicittagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var implicittagskeysLoading = tempimplicittagskeysLoading.AsReadOnly();

            // loading Unknown526
            (var unknown526Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown527
            (var unknown527Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown531
            (var unknown531Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown535
            (var unknown535Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown539
            (var unknown539Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown543
            (var unknown543Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown547
            (var unknown547Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown551
            (var unknown551Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown555
            (var unknown555Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown559
            (var unknown559Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown563
            (var unknown563Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown567
            (var unknown567Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown571
            (var unknown571Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown575
            (var unknown575Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown579
            (var unknown579Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown583
            (var unknown583Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown587
            (var unknown587Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffTemplate
            (var bufftemplateLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ArchnemesisMinionMod
            (var archnemesisminionmodLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown619
            (var tempunknown619Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown619Loading = tempunknown619Loading.AsReadOnly();

            // loading Unknown635
            (var unknown635Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown639
            (var tempunknown639Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown639Loading = tempunknown639Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ModsDat()
            {
                Id = idLoading,
                HASH16 = hash16Loading,
                ModTypeKey = modtypekeyLoading,
                Level = levelLoading,
                StatsKey1 = statskey1Loading,
                StatsKey2 = statskey2Loading,
                StatsKey3 = statskey3Loading,
                StatsKey4 = statskey4Loading,
                Domain = domainLoading,
                Name = nameLoading,
                GenerationType = generationtypeLoading,
                Families = familiesLoading,
                Stat1Min = stat1minLoading,
                Stat1Max = stat1maxLoading,
                Stat2Min = stat2minLoading,
                Stat2Max = stat2maxLoading,
                Stat3Min = stat3minLoading,
                Stat3Max = stat3maxLoading,
                Stat4Min = stat4minLoading,
                Stat4Max = stat4maxLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                TagsKeys = tagskeysLoading,
                GrantedEffectsPerLevelKeys = grantedeffectsperlevelkeysLoading,
                Unknown224 = unknown224Loading,
                MonsterMetadata = monstermetadataLoading,
                MonsterKillAchievements = monsterkillachievementsLoading,
                ChestModType = chestmodtypeLoading,
                Stat5Min = stat5minLoading,
                Stat5Max = stat5maxLoading,
                StatsKey5 = statskey5Loading,
                FullAreaClear_AchievementItemsKey = fullareaclear_achievementitemskeyLoading,
                AchievementItemsKey = achievementitemskeyLoading,
                GenerationWeight_TagsKeys = generationweight_tagskeysLoading,
                GenerationWeight_Values = generationweight_valuesLoading,
                ModifyMapsAchievements = modifymapsachievementsLoading,
                IsEssenceOnlyModifier = isessenceonlymodifierLoading,
                Stat6Min = stat6minLoading,
                Stat6Max = stat6maxLoading,
                StatsKey6 = statskey6Loading,
                MaxLevel = maxlevelLoading,
                Unknown413 = unknown413Loading,
                CraftingItemClassRestrictions = craftingitemclassrestrictionsLoading,
                MonsterOnDeath = monsterondeathLoading,
                Unknown438 = unknown438Loading,
                Unknown442 = unknown442Loading,
                Heist_SubStatValue1 = heist_substatvalue1Loading,
                Heist_SubStatValue2 = heist_substatvalue2Loading,
                Heist_StatsKey0 = heist_statskey0Loading,
                Heist_StatsKey1 = heist_statskey1Loading,
                Heist_AddStatValue1 = heist_addstatvalue1Loading,
                Heist_AddStatValue2 = heist_addstatvalue2Loading,
                InfluenceTypes = influencetypesLoading,
                ImplicitTagsKeys = implicittagskeysLoading,
                Unknown526 = unknown526Loading,
                Unknown527 = unknown527Loading,
                Unknown531 = unknown531Loading,
                Unknown535 = unknown535Loading,
                Unknown539 = unknown539Loading,
                Unknown543 = unknown543Loading,
                Unknown547 = unknown547Loading,
                Unknown551 = unknown551Loading,
                Unknown555 = unknown555Loading,
                Unknown559 = unknown559Loading,
                Unknown563 = unknown563Loading,
                Unknown567 = unknown567Loading,
                Unknown571 = unknown571Loading,
                Unknown575 = unknown575Loading,
                Unknown579 = unknown579Loading,
                Unknown583 = unknown583Loading,
                Unknown587 = unknown587Loading,
                BuffTemplate = bufftemplateLoading,
                ArchnemesisMinionMod = archnemesisminionmodLoading,
                HASH32 = hash32Loading,
                Unknown619 = unknown619Loading,
                Unknown635 = unknown635Loading,
                Unknown639 = unknown639Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
