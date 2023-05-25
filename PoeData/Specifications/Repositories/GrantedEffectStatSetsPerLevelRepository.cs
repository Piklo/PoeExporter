using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GrantedEffectStatSetsPerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class GrantedEffectStatSetsPerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GrantedEffectStatSetsPerLevelDat> Items { get; }

    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byStatSet;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byGemLevel;
    private Dictionary<float, List<GrantedEffectStatSetsPerLevelDat>>? byPlayerLevelReq;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? bySpellCritChance;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byAttackCritChance;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byBaseMultiplier;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byDamageEffectiveness;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byAdditionalFlags;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byFloatStats;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byInterpolationBases;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byAdditionalStats;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byStatInterpolations;
    private Dictionary<float, List<GrantedEffectStatSetsPerLevelDat>>? byFloatStatsValues;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byBaseResolvedValues;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byAdditionalStatsValues;
    private Dictionary<int, List<GrantedEffectStatSetsPerLevelDat>>? byGrantedEffects;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrantedEffectStatSetsPerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GrantedEffectStatSetsPerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.StatSet"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatSet(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatSet(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.StatSet"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatSet(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byStatSet is null)
        {
            byStatSet = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatSet;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatSet.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatSet.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatSet.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byStatSet"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByStatSet(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatSet(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.GemLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGemLevel(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGemLevel(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.GemLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGemLevel(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byGemLevel is null)
        {
            byGemLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.GemLevel;

                if (!byGemLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byGemLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byGemLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byGemLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByGemLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGemLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.PlayerLevelReq"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayerLevelReq(float? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayerLevelReq(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.PlayerLevelReq"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayerLevelReq(float? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byPlayerLevelReq is null)
        {
            byPlayerLevelReq = new();
            foreach (var item in Items)
            {
                var itemKey = item.PlayerLevelReq;

                if (!byPlayerLevelReq.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byPlayerLevelReq.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byPlayerLevelReq.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byPlayerLevelReq"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByPlayerLevelReq(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<float, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayerLevelReq(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.SpellCritChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySpellCritChance(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySpellCritChance(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.SpellCritChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySpellCritChance(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (bySpellCritChance is null)
        {
            bySpellCritChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.SpellCritChance;

                if (!bySpellCritChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySpellCritChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySpellCritChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.bySpellCritChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyBySpellCritChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySpellCritChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AttackCritChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttackCritChance(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttackCritChance(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AttackCritChance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttackCritChance(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byAttackCritChance is null)
        {
            byAttackCritChance = new();
            foreach (var item in Items)
            {
                var itemKey = item.AttackCritChance;

                if (!byAttackCritChance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttackCritChance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttackCritChance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byAttackCritChance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByAttackCritChance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttackCritChance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.BaseMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseMultiplier(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseMultiplier(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.BaseMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseMultiplier(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byBaseMultiplier is null)
        {
            byBaseMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseMultiplier;

                if (!byBaseMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBaseMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBaseMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byBaseMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByBaseMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.DamageEffectiveness"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamageEffectiveness(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamageEffectiveness(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.DamageEffectiveness"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamageEffectiveness(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byDamageEffectiveness is null)
        {
            byDamageEffectiveness = new();
            foreach (var item in Items)
            {
                var itemKey = item.DamageEffectiveness;

                if (!byDamageEffectiveness.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamageEffectiveness.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamageEffectiveness.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byDamageEffectiveness"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByDamageEffectiveness(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamageEffectiveness(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AdditionalFlags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAdditionalFlags(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAdditionalFlags(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AdditionalFlags"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAdditionalFlags(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byAdditionalFlags is null)
        {
            byAdditionalFlags = new();
            foreach (var item in Items)
            {
                var itemKey = item.AdditionalFlags;
                foreach (var listKey in itemKey)
                {
                    if (!byAdditionalFlags.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAdditionalFlags.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAdditionalFlags.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byAdditionalFlags"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByAdditionalFlags(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAdditionalFlags(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.FloatStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFloatStats(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFloatStats(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.FloatStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFloatStats(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byFloatStats is null)
        {
            byFloatStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.FloatStats;
                foreach (var listKey in itemKey)
                {
                    if (!byFloatStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFloatStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFloatStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byFloatStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByFloatStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFloatStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.InterpolationBases"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInterpolationBases(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInterpolationBases(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.InterpolationBases"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInterpolationBases(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byInterpolationBases is null)
        {
            byInterpolationBases = new();
            foreach (var item in Items)
            {
                var itemKey = item.InterpolationBases;
                foreach (var listKey in itemKey)
                {
                    if (!byInterpolationBases.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byInterpolationBases.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byInterpolationBases.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byInterpolationBases"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByInterpolationBases(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInterpolationBases(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AdditionalStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAdditionalStats(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAdditionalStats(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AdditionalStats"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAdditionalStats(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byAdditionalStats is null)
        {
            byAdditionalStats = new();
            foreach (var item in Items)
            {
                var itemKey = item.AdditionalStats;
                foreach (var listKey in itemKey)
                {
                    if (!byAdditionalStats.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAdditionalStats.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAdditionalStats.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byAdditionalStats"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByAdditionalStats(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAdditionalStats(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.StatInterpolations"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatInterpolations(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatInterpolations(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.StatInterpolations"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatInterpolations(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byStatInterpolations is null)
        {
            byStatInterpolations = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatInterpolations;
                foreach (var listKey in itemKey)
                {
                    if (!byStatInterpolations.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatInterpolations.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatInterpolations.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byStatInterpolations"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByStatInterpolations(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatInterpolations(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.FloatStatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFloatStatsValues(float? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFloatStatsValues(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.FloatStatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFloatStatsValues(float? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byFloatStatsValues is null)
        {
            byFloatStatsValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.FloatStatsValues;
                foreach (var listKey in itemKey)
                {
                    if (!byFloatStatsValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byFloatStatsValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byFloatStatsValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byFloatStatsValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByFloatStatsValues(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<float, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFloatStatsValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.BaseResolvedValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseResolvedValues(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBaseResolvedValues(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.BaseResolvedValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseResolvedValues(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byBaseResolvedValues is null)
        {
            byBaseResolvedValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.BaseResolvedValues;
                foreach (var listKey in itemKey)
                {
                    if (!byBaseResolvedValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byBaseResolvedValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byBaseResolvedValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byBaseResolvedValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByBaseResolvedValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseResolvedValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AdditionalStatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAdditionalStatsValues(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAdditionalStatsValues(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.AdditionalStatsValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAdditionalStatsValues(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byAdditionalStatsValues is null)
        {
            byAdditionalStatsValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.AdditionalStatsValues;
                foreach (var listKey in itemKey)
                {
                    if (!byAdditionalStatsValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAdditionalStatsValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAdditionalStatsValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byAdditionalStatsValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByAdditionalStatsValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAdditionalStatsValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.GrantedEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffects(int? key, out GrantedEffectStatSetsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffects(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.GrantedEffects"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffects(int? key, out IReadOnlyList<GrantedEffectStatSetsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        if (byGrantedEffects is null)
        {
            byGrantedEffects = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffects;
                foreach (var listKey in itemKey)
                {
                    if (!byGrantedEffects.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byGrantedEffects.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byGrantedEffects.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectStatSetsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectStatSetsPerLevelDat"/> with <see cref="GrantedEffectStatSetsPerLevelDat.byGrantedEffects"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectStatSetsPerLevelDat>> GetManyToManyByGrantedEffects(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectStatSetsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffects(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectStatSetsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GrantedEffectStatSetsPerLevelDat[] Load()
    {
        const string filePath = "Data/GrantedEffectStatSetsPerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectStatSetsPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatSet
            (var statsetLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading GemLevel
            (var gemlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PlayerLevelReq
            (var playerlevelreqLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading SpellCritChance
            (var spellcritchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackCritChance
            (var attackcritchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseMultiplier
            (var basemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageEffectiveness
            (var damageeffectivenessLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AdditionalFlags
            (var tempadditionalflagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var additionalflagsLoading = tempadditionalflagsLoading.AsReadOnly();

            // loading FloatStats
            (var tempfloatstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var floatstatsLoading = tempfloatstatsLoading.AsReadOnly();

            // loading InterpolationBases
            (var tempinterpolationbasesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var interpolationbasesLoading = tempinterpolationbasesLoading.AsReadOnly();

            // loading AdditionalStats
            (var tempadditionalstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var additionalstatsLoading = tempadditionalstatsLoading.AsReadOnly();

            // loading StatInterpolations
            (var tempstatinterpolationsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statinterpolationsLoading = tempstatinterpolationsLoading.AsReadOnly();

            // loading FloatStatsValues
            (var tempfloatstatsvaluesLoading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var floatstatsvaluesLoading = tempfloatstatsvaluesLoading.AsReadOnly();

            // loading BaseResolvedValues
            (var tempbaseresolvedvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var baseresolvedvaluesLoading = tempbaseresolvedvaluesLoading.AsReadOnly();

            // loading AdditionalStatsValues
            (var tempadditionalstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var additionalstatsvaluesLoading = tempadditionalstatsvaluesLoading.AsReadOnly();

            // loading GrantedEffects
            (var tempgrantedeffectsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectsLoading = tempgrantedeffectsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectStatSetsPerLevelDat()
            {
                StatSet = statsetLoading,
                GemLevel = gemlevelLoading,
                PlayerLevelReq = playerlevelreqLoading,
                SpellCritChance = spellcritchanceLoading,
                AttackCritChance = attackcritchanceLoading,
                BaseMultiplier = basemultiplierLoading,
                DamageEffectiveness = damageeffectivenessLoading,
                AdditionalFlags = additionalflagsLoading,
                FloatStats = floatstatsLoading,
                InterpolationBases = interpolationbasesLoading,
                AdditionalStats = additionalstatsLoading,
                StatInterpolations = statinterpolationsLoading,
                FloatStatsValues = floatstatsvaluesLoading,
                BaseResolvedValues = baseresolvedvaluesLoading,
                AdditionalStatsValues = additionalstatsvaluesLoading,
                GrantedEffects = grantedeffectsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
