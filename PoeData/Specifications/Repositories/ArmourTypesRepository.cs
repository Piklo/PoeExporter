using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ArmourTypesDat"/> related data and helper methods.
/// </summary>
public sealed class ArmourTypesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ArmourTypesDat> Items { get; }

    private Dictionary<int, List<ArmourTypesDat>>? byBaseItemTypesKey;
    private Dictionary<int, List<ArmourTypesDat>>? byArmourMin;
    private Dictionary<int, List<ArmourTypesDat>>? byArmourMax;
    private Dictionary<int, List<ArmourTypesDat>>? byEvasionMin;
    private Dictionary<int, List<ArmourTypesDat>>? byEvasionMax;
    private Dictionary<int, List<ArmourTypesDat>>? byEnergyShieldMin;
    private Dictionary<int, List<ArmourTypesDat>>? byEnergyShieldMax;
    private Dictionary<int, List<ArmourTypesDat>>? byIncreasedMovementSpeed;
    private Dictionary<int, List<ArmourTypesDat>>? byWardMin;
    private Dictionary<int, List<ArmourTypesDat>>? byWardMax;
    private Dictionary<int, List<ArmourTypesDat>>? byUnknown52;
    private Dictionary<int, List<ArmourTypesDat>>? byUnknown56;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArmourTypesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ArmourTypesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBaseItemTypesKey(int? key, out ArmourTypesDat? item)
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.BaseItemTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBaseItemTypesKey(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
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
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byBaseItemTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByBaseItemTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBaseItemTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.ArmourMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArmourMin(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArmourMin(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.ArmourMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArmourMin(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byArmourMin is null)
        {
            byArmourMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArmourMin;

                if (!byArmourMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArmourMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArmourMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byArmourMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByArmourMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArmourMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.ArmourMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByArmourMax(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByArmourMax(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.ArmourMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByArmourMax(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byArmourMax is null)
        {
            byArmourMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.ArmourMax;

                if (!byArmourMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byArmourMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byArmourMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byArmourMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByArmourMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByArmourMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EvasionMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEvasionMin(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEvasionMin(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EvasionMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEvasionMin(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byEvasionMin is null)
        {
            byEvasionMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.EvasionMin;

                if (!byEvasionMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEvasionMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEvasionMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byEvasionMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByEvasionMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEvasionMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EvasionMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEvasionMax(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEvasionMax(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EvasionMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEvasionMax(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byEvasionMax is null)
        {
            byEvasionMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.EvasionMax;

                if (!byEvasionMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEvasionMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEvasionMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byEvasionMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByEvasionMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEvasionMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EnergyShieldMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnergyShieldMin(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnergyShieldMin(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EnergyShieldMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnergyShieldMin(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byEnergyShieldMin is null)
        {
            byEnergyShieldMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnergyShieldMin;

                if (!byEnergyShieldMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEnergyShieldMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEnergyShieldMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byEnergyShieldMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByEnergyShieldMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnergyShieldMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EnergyShieldMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnergyShieldMax(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnergyShieldMax(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.EnergyShieldMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnergyShieldMax(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byEnergyShieldMax is null)
        {
            byEnergyShieldMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.EnergyShieldMax;

                if (!byEnergyShieldMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEnergyShieldMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEnergyShieldMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byEnergyShieldMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByEnergyShieldMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnergyShieldMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.IncreasedMovementSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIncreasedMovementSpeed(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIncreasedMovementSpeed(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.IncreasedMovementSpeed"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIncreasedMovementSpeed(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byIncreasedMovementSpeed is null)
        {
            byIncreasedMovementSpeed = new();
            foreach (var item in Items)
            {
                var itemKey = item.IncreasedMovementSpeed;

                if (!byIncreasedMovementSpeed.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIncreasedMovementSpeed.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIncreasedMovementSpeed.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byIncreasedMovementSpeed"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByIncreasedMovementSpeed(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIncreasedMovementSpeed(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.WardMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWardMin(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWardMin(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.WardMin"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWardMin(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byWardMin is null)
        {
            byWardMin = new();
            foreach (var item in Items)
            {
                var itemKey = item.WardMin;

                if (!byWardMin.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWardMin.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWardMin.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byWardMin"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByWardMin(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWardMin(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.WardMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByWardMax(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByWardMax(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.WardMax"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByWardMax(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byWardMax is null)
        {
            byWardMax = new();
            foreach (var item in Items)
            {
                var itemKey = item.WardMax;

                if (!byWardMax.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byWardMax.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byWardMax.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byWardMax"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByWardMax(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByWardMax(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out ArmourTypesDat? item)
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
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
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out ArmourTypesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown56(key, out var items))
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
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<ArmourTypesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        if (byUnknown56 is null)
        {
            byUnknown56 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown56;

                if (!byUnknown56.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown56.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown56.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ArmourTypesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ArmourTypesDat"/> with <see cref="ArmourTypesDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ArmourTypesDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ArmourTypesDat>>();
        }

        var items = new List<ResultItem<int, ArmourTypesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ArmourTypesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ArmourTypesDat[] Load()
    {
        const string filePath = "Data/ArmourTypes.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArmourTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ArmourMin
            (var armourminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ArmourMax
            (var armourmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EvasionMin
            (var evasionminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EvasionMax
            (var evasionmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnergyShieldMin
            (var energyshieldminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnergyShieldMax
            (var energyshieldmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IncreasedMovementSpeed
            (var increasedmovementspeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WardMin
            (var wardminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WardMax
            (var wardmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArmourTypesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                ArmourMin = armourminLoading,
                ArmourMax = armourmaxLoading,
                EvasionMin = evasionminLoading,
                EvasionMax = evasionmaxLoading,
                EnergyShieldMin = energyshieldminLoading,
                EnergyShieldMax = energyshieldmaxLoading,
                IncreasedMovementSpeed = increasedmovementspeedLoading,
                WardMin = wardminLoading,
                WardMax = wardmaxLoading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
