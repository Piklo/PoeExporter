using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterTypesDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterTypesDat> Items { get; }

    private Dictionary<string, List<MonsterTypesDat>>? byId;
    private Dictionary<int, List<MonsterTypesDat>>? byUnknown8;
    private Dictionary<bool, List<MonsterTypesDat>>? byIsSummoned;
    private Dictionary<int, List<MonsterTypesDat>>? byArmour;
    private Dictionary<int, List<MonsterTypesDat>>? byEvasion;
    private Dictionary<int, List<MonsterTypesDat>>? byEnergyShieldFromLife;
    private Dictionary<int, List<MonsterTypesDat>>? byDamageSpread;
    private Dictionary<int, List<MonsterTypesDat>>? byMonsterResistancesKey;
    private Dictionary<bool, List<MonsterTypesDat>>? byIsLargeAbyssMonster;
    private Dictionary<bool, List<MonsterTypesDat>>? byIsSmallAbyssMonster;
    private Dictionary<bool, List<MonsterTypesDat>>? byUnknown47;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out MonsterTypesDat? item)
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
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
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterTypesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<string, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown8(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;

                if (!byUnknown8.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown8.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterTypesDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<int, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.IsSummoned"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsSummoned(bool? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsSummoned(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.IsSummoned"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsSummoned(bool? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byIsSummoned is null)
        {
            byIsSummoned = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsSummoned;

                if (!byIsSummoned.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsSummoned.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsSummoned.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byIsSummoned"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterTypesDat>> GetManyToManyByIsSummoned(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsSummoned(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Armour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArmour(int? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArmour(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Armour"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArmour(int? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byArmour is null)
        {
            byArmour = new();
            foreach (var item in Items)
            {
                var itemKey = item.Armour;

                if (!byArmour.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArmour.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArmour.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byArmour"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterTypesDat>> GetManyToManyByArmour(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<int, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArmour(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Evasion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEvasion(int? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEvasion(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Evasion"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEvasion(int? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byEvasion is null)
        {
            byEvasion = new();
            foreach (var item in Items)
            {
                var itemKey = item.Evasion;

                if (!byEvasion.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEvasion.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEvasion.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byEvasion"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterTypesDat>> GetManyToManyByEvasion(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<int, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEvasion(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.EnergyShieldFromLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnergyShieldFromLife(int? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnergyShieldFromLife(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.EnergyShieldFromLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnergyShieldFromLife(int? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byEnergyShieldFromLife is null)
        {
            byEnergyShieldFromLife = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnergyShieldFromLife;

                if (!byEnergyShieldFromLife.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEnergyShieldFromLife.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEnergyShieldFromLife.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byEnergyShieldFromLife"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterTypesDat>> GetManyToManyByEnergyShieldFromLife(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<int, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnergyShieldFromLife(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.DamageSpread"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDamageSpread(int? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDamageSpread(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.DamageSpread"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDamageSpread(int? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byDamageSpread is null)
        {
            byDamageSpread = new();
            foreach (var item in Items)
            {
                var itemKey = item.DamageSpread;

                if (!byDamageSpread.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDamageSpread.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDamageSpread.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byDamageSpread"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterTypesDat>> GetManyToManyByDamageSpread(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<int, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDamageSpread(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.MonsterResistancesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterResistancesKey(int? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterResistancesKey(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.MonsterResistancesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterResistancesKey(int? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byMonsterResistancesKey is null)
        {
            byMonsterResistancesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterResistancesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterResistancesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterResistancesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterResistancesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byMonsterResistancesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterTypesDat>> GetManyToManyByMonsterResistancesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<int, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterResistancesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.IsLargeAbyssMonster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLargeAbyssMonster(bool? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLargeAbyssMonster(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.IsLargeAbyssMonster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLargeAbyssMonster(bool? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byIsLargeAbyssMonster is null)
        {
            byIsLargeAbyssMonster = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLargeAbyssMonster;

                if (!byIsLargeAbyssMonster.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLargeAbyssMonster.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLargeAbyssMonster.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byIsLargeAbyssMonster"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterTypesDat>> GetManyToManyByIsLargeAbyssMonster(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLargeAbyssMonster(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.IsSmallAbyssMonster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsSmallAbyssMonster(bool? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsSmallAbyssMonster(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.IsSmallAbyssMonster"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsSmallAbyssMonster(bool? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byIsSmallAbyssMonster is null)
        {
            byIsSmallAbyssMonster = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsSmallAbyssMonster;

                if (!byIsSmallAbyssMonster.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsSmallAbyssMonster.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsSmallAbyssMonster.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byIsSmallAbyssMonster"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterTypesDat>> GetManyToManyByIsSmallAbyssMonster(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsSmallAbyssMonster(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown47(bool? key, out MonsterTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown47(key, out var items))
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
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown47(bool? key, out IReadOnlyList<MonsterTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        if (byUnknown47 is null)
        {
            byUnknown47 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown47;

                if (!byUnknown47.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown47.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown47.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterTypesDat"/> with <see cref="MonsterTypesDat.byUnknown47"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterTypesDat>> GetManyToManyByUnknown47(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterTypesDat>>();
        }

        var items = new List<ResultItem<bool, MonsterTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown47(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterTypesDat[] Load()
    {
        const string filePath = "Data/MonsterTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsSummoned
            (var issummonedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Armour
            (var armourLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Evasion
            (var evasionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnergyShieldFromLife
            (var energyshieldfromlifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageSpread
            (var damagespreadLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterResistancesKey
            (var monsterresistanceskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsLargeAbyssMonster
            (var islargeabyssmonsterLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsSmallAbyssMonster
            (var issmallabyssmonsterLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterTypesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                IsSummoned = issummonedLoading,
                Armour = armourLoading,
                Evasion = evasionLoading,
                EnergyShieldFromLife = energyshieldfromlifeLoading,
                DamageSpread = damagespreadLoading,
                MonsterResistancesKey = monsterresistanceskeyLoading,
                IsLargeAbyssMonster = islargeabyssmonsterLoading,
                IsSmallAbyssMonster = issmallabyssmonsterLoading,
                Unknown47 = unknown47Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
