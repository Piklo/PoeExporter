using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GrantedEffectsPerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class GrantedEffectsPerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GrantedEffectsPerLevelDat> Items { get; }

    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byGrantedEffect;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byLevel;
    private Dictionary<float, List<GrantedEffectsPerLevelDat>>? byPlayerLevelReq;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byCostMultiplier;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byStoredUses;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byCooldown;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byCooldownBypassType;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byVaalSouls;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byVaalStoredUses;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byCooldownGroup;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byUnknown52;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? bySoulGainPreventionDuration;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byAttackSpeedMultiplier;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byUnknown64;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byCostAmounts;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byCostTypes;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byManaReservationFlat;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byManaReservationPercent;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byLifeReservationFlat;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byLifeReservationPercent;
    private Dictionary<int, List<GrantedEffectsPerLevelDat>>? byAttackTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="GrantedEffectsPerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GrantedEffectsPerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.GrantedEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGrantedEffect(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGrantedEffect(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.GrantedEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGrantedEffect(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byGrantedEffect is null)
        {
            byGrantedEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.GrantedEffect;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGrantedEffect.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGrantedEffect.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGrantedEffect.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byGrantedEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByGrantedEffect(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGrantedEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out GrantedEffectsPerLevelDat? item)
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
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
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.PlayerLevelReq"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayerLevelReq(float? key, out GrantedEffectsPerLevelDat? item)
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.PlayerLevelReq"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayerLevelReq(float? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
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
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byPlayerLevelReq"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GrantedEffectsPerLevelDat>> GetManyToManyByPlayerLevelReq(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<float, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayerLevelReq(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CostMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCostMultiplier(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCostMultiplier(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CostMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCostMultiplier(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byCostMultiplier is null)
        {
            byCostMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.CostMultiplier;

                if (!byCostMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCostMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCostMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byCostMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByCostMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCostMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.StoredUses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStoredUses(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStoredUses(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.StoredUses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStoredUses(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byStoredUses is null)
        {
            byStoredUses = new();
            foreach (var item in Items)
            {
                var itemKey = item.StoredUses;

                if (!byStoredUses.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStoredUses.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStoredUses.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byStoredUses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByStoredUses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStoredUses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Cooldown"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCooldown(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCooldown(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Cooldown"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCooldown(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byCooldown is null)
        {
            byCooldown = new();
            foreach (var item in Items)
            {
                var itemKey = item.Cooldown;

                if (!byCooldown.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCooldown.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCooldown.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byCooldown"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByCooldown(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCooldown(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CooldownBypassType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCooldownBypassType(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCooldownBypassType(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CooldownBypassType"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCooldownBypassType(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byCooldownBypassType is null)
        {
            byCooldownBypassType = new();
            foreach (var item in Items)
            {
                var itemKey = item.CooldownBypassType;

                if (!byCooldownBypassType.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCooldownBypassType.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCooldownBypassType.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byCooldownBypassType"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByCooldownBypassType(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCooldownBypassType(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.VaalSouls"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVaalSouls(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVaalSouls(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.VaalSouls"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVaalSouls(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byVaalSouls is null)
        {
            byVaalSouls = new();
            foreach (var item in Items)
            {
                var itemKey = item.VaalSouls;

                if (!byVaalSouls.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVaalSouls.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVaalSouls.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byVaalSouls"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByVaalSouls(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVaalSouls(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.VaalStoredUses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByVaalStoredUses(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByVaalStoredUses(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.VaalStoredUses"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByVaalStoredUses(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byVaalStoredUses is null)
        {
            byVaalStoredUses = new();
            foreach (var item in Items)
            {
                var itemKey = item.VaalStoredUses;

                if (!byVaalStoredUses.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byVaalStoredUses.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byVaalStoredUses.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byVaalStoredUses"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByVaalStoredUses(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByVaalStoredUses(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CooldownGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCooldownGroup(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCooldownGroup(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CooldownGroup"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCooldownGroup(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byCooldownGroup is null)
        {
            byCooldownGroup = new();
            foreach (var item in Items)
            {
                var itemKey = item.CooldownGroup;

                if (!byCooldownGroup.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byCooldownGroup.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byCooldownGroup.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byCooldownGroup"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByCooldownGroup(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCooldownGroup(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown52(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byUnknown52 is null)
        {
            byUnknown52 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown52;

                if (!byUnknown52.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown52.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown52.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.SoulGainPreventionDuration"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySoulGainPreventionDuration(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySoulGainPreventionDuration(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.SoulGainPreventionDuration"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySoulGainPreventionDuration(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (bySoulGainPreventionDuration is null)
        {
            bySoulGainPreventionDuration = new();
            foreach (var item in Items)
            {
                var itemKey = item.SoulGainPreventionDuration;

                if (!bySoulGainPreventionDuration.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySoulGainPreventionDuration.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySoulGainPreventionDuration.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.bySoulGainPreventionDuration"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyBySoulGainPreventionDuration(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySoulGainPreventionDuration(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.AttackSpeedMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttackSpeedMultiplier(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttackSpeedMultiplier(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.AttackSpeedMultiplier"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttackSpeedMultiplier(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byAttackSpeedMultiplier is null)
        {
            byAttackSpeedMultiplier = new();
            foreach (var item in Items)
            {
                var itemKey = item.AttackSpeedMultiplier;

                if (!byAttackSpeedMultiplier.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttackSpeedMultiplier.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttackSpeedMultiplier.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byAttackSpeedMultiplier"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByAttackSpeedMultiplier(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttackSpeedMultiplier(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out GrantedEffectsPerLevelDat? item)
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
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
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CostAmounts"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCostAmounts(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCostAmounts(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CostAmounts"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCostAmounts(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byCostAmounts is null)
        {
            byCostAmounts = new();
            foreach (var item in Items)
            {
                var itemKey = item.CostAmounts;
                foreach (var listKey in itemKey)
                {
                    if (!byCostAmounts.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCostAmounts.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCostAmounts.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byCostAmounts"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByCostAmounts(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCostAmounts(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CostTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCostTypes(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCostTypes(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.CostTypes"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCostTypes(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byCostTypes is null)
        {
            byCostTypes = new();
            foreach (var item in Items)
            {
                var itemKey = item.CostTypes;
                foreach (var listKey in itemKey)
                {
                    if (!byCostTypes.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byCostTypes.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byCostTypes.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byCostTypes"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByCostTypes(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCostTypes(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.ManaReservationFlat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByManaReservationFlat(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByManaReservationFlat(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.ManaReservationFlat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByManaReservationFlat(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byManaReservationFlat is null)
        {
            byManaReservationFlat = new();
            foreach (var item in Items)
            {
                var itemKey = item.ManaReservationFlat;

                if (!byManaReservationFlat.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byManaReservationFlat.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byManaReservationFlat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byManaReservationFlat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByManaReservationFlat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByManaReservationFlat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.ManaReservationPercent"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByManaReservationPercent(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByManaReservationPercent(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.ManaReservationPercent"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByManaReservationPercent(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byManaReservationPercent is null)
        {
            byManaReservationPercent = new();
            foreach (var item in Items)
            {
                var itemKey = item.ManaReservationPercent;

                if (!byManaReservationPercent.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byManaReservationPercent.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byManaReservationPercent.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byManaReservationPercent"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByManaReservationPercent(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByManaReservationPercent(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.LifeReservationFlat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeReservationFlat(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeReservationFlat(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.LifeReservationFlat"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeReservationFlat(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byLifeReservationFlat is null)
        {
            byLifeReservationFlat = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifeReservationFlat;

                if (!byLifeReservationFlat.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeReservationFlat.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeReservationFlat.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byLifeReservationFlat"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByLifeReservationFlat(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeReservationFlat(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.LifeReservationPercent"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLifeReservationPercent(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLifeReservationPercent(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.LifeReservationPercent"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLifeReservationPercent(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byLifeReservationPercent is null)
        {
            byLifeReservationPercent = new();
            foreach (var item in Items)
            {
                var itemKey = item.LifeReservationPercent;

                if (!byLifeReservationPercent.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLifeReservationPercent.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLifeReservationPercent.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byLifeReservationPercent"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByLifeReservationPercent(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLifeReservationPercent(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.AttackTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAttackTime(int? key, out GrantedEffectsPerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAttackTime(key, out var items))
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
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.AttackTime"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAttackTime(int? key, out IReadOnlyList<GrantedEffectsPerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        if (byAttackTime is null)
        {
            byAttackTime = new();
            foreach (var item in Items)
            {
                var itemKey = item.AttackTime;

                if (!byAttackTime.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byAttackTime.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byAttackTime.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GrantedEffectsPerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GrantedEffectsPerLevelDat"/> with <see cref="GrantedEffectsPerLevelDat.byAttackTime"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GrantedEffectsPerLevelDat>> GetManyToManyByAttackTime(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GrantedEffectsPerLevelDat>>();
        }

        var items = new List<ResultItem<int, GrantedEffectsPerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAttackTime(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GrantedEffectsPerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GrantedEffectsPerLevelDat[] Load()
    {
        const string filePath = "Data/GrantedEffectsPerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectsPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GrantedEffect
            (var grantedeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PlayerLevelReq
            (var playerlevelreqLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading CostMultiplier
            (var costmultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StoredUses
            (var storedusesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Cooldown
            (var cooldownLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CooldownBypassType
            (var cooldownbypasstypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VaalSouls
            (var vaalsoulsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VaalStoredUses
            (var vaalstoredusesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CooldownGroup
            (var cooldowngroupLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoulGainPreventionDuration
            (var soulgainpreventiondurationLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackSpeedMultiplier
            (var attackspeedmultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CostAmounts
            (var tempcostamountsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var costamountsLoading = tempcostamountsLoading.AsReadOnly();

            // loading CostTypes
            (var tempcosttypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var costtypesLoading = tempcosttypesLoading.AsReadOnly();

            // loading ManaReservationFlat
            (var manareservationflatLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ManaReservationPercent
            (var manareservationpercentLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeReservationFlat
            (var lifereservationflatLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeReservationPercent
            (var lifereservationpercentLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackTime
            (var attacktimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectsPerLevelDat()
            {
                GrantedEffect = grantedeffectLoading,
                Level = levelLoading,
                PlayerLevelReq = playerlevelreqLoading,
                CostMultiplier = costmultiplierLoading,
                StoredUses = storedusesLoading,
                Cooldown = cooldownLoading,
                CooldownBypassType = cooldownbypasstypeLoading,
                VaalSouls = vaalsoulsLoading,
                VaalStoredUses = vaalstoredusesLoading,
                CooldownGroup = cooldowngroupLoading,
                Unknown52 = unknown52Loading,
                SoulGainPreventionDuration = soulgainpreventiondurationLoading,
                AttackSpeedMultiplier = attackspeedmultiplierLoading,
                Unknown64 = unknown64Loading,
                CostAmounts = costamountsLoading,
                CostTypes = costtypesLoading,
                ManaReservationFlat = manareservationflatLoading,
                ManaReservationPercent = manareservationpercentLoading,
                LifeReservationFlat = lifereservationflatLoading,
                LifeReservationPercent = lifereservationpercentLoading,
                AttackTime = attacktimeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
