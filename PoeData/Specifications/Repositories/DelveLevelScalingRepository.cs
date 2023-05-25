using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="DelveLevelScalingDat"/> related data and helper methods.
/// </summary>
public sealed class DelveLevelScalingRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<DelveLevelScalingDat> Items { get; }

    private Dictionary<int, List<DelveLevelScalingDat>>? byDepth;
    private Dictionary<int, List<DelveLevelScalingDat>>? byMonsterLevel;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown8;
    private Dictionary<int, List<DelveLevelScalingDat>>? bySulphiteCost;
    private Dictionary<int, List<DelveLevelScalingDat>>? byMonsterLevel2;
    private Dictionary<int, List<DelveLevelScalingDat>>? byMoreMonsterDamage;
    private Dictionary<int, List<DelveLevelScalingDat>>? byMoreMonsterLife;
    private Dictionary<int, List<DelveLevelScalingDat>>? byDarknessResistance;
    private Dictionary<int, List<DelveLevelScalingDat>>? byLightRadius;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown36;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown40;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown44;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown48;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown52;
    private Dictionary<int, List<DelveLevelScalingDat>>? byUnknown56;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveLevelScalingRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal DelveLevelScalingRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Depth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDepth(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDepth(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Depth"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDepth(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byDepth is null)
        {
            byDepth = new();
            foreach (var item in Items)
            {
                var itemKey = item.Depth;

                if (!byDepth.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDepth.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDepth.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byDepth"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByDepth(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDepth(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MonsterLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterLevel(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterLevel(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MonsterLevel"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterLevel(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byMonsterLevel is null)
        {
            byMonsterLevel = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterLevel;

                if (!byMonsterLevel.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterLevel.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterLevel.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byMonsterLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByMonsterLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out DelveLevelScalingDat? item)
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
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
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.SulphiteCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySulphiteCost(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySulphiteCost(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.SulphiteCost"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySulphiteCost(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (bySulphiteCost is null)
        {
            bySulphiteCost = new();
            foreach (var item in Items)
            {
                var itemKey = item.SulphiteCost;

                if (!bySulphiteCost.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    bySulphiteCost.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!bySulphiteCost.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.bySulphiteCost"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyBySulphiteCost(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySulphiteCost(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MonsterLevel2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterLevel2(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterLevel2(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MonsterLevel2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterLevel2(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byMonsterLevel2 is null)
        {
            byMonsterLevel2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterLevel2;

                if (!byMonsterLevel2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMonsterLevel2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterLevel2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byMonsterLevel2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByMonsterLevel2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterLevel2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MoreMonsterDamage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMoreMonsterDamage(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMoreMonsterDamage(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MoreMonsterDamage"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMoreMonsterDamage(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byMoreMonsterDamage is null)
        {
            byMoreMonsterDamage = new();
            foreach (var item in Items)
            {
                var itemKey = item.MoreMonsterDamage;

                if (!byMoreMonsterDamage.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMoreMonsterDamage.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMoreMonsterDamage.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byMoreMonsterDamage"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByMoreMonsterDamage(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMoreMonsterDamage(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MoreMonsterLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMoreMonsterLife(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMoreMonsterLife(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.MoreMonsterLife"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMoreMonsterLife(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byMoreMonsterLife is null)
        {
            byMoreMonsterLife = new();
            foreach (var item in Items)
            {
                var itemKey = item.MoreMonsterLife;

                if (!byMoreMonsterLife.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byMoreMonsterLife.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byMoreMonsterLife.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byMoreMonsterLife"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByMoreMonsterLife(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMoreMonsterLife(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.DarknessResistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDarknessResistance(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDarknessResistance(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.DarknessResistance"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDarknessResistance(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byDarknessResistance is null)
        {
            byDarknessResistance = new();
            foreach (var item in Items)
            {
                var itemKey = item.DarknessResistance;

                if (!byDarknessResistance.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDarknessResistance.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDarknessResistance.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byDarknessResistance"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByDarknessResistance(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDarknessResistance(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.LightRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLightRadius(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLightRadius(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.LightRadius"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLightRadius(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byLightRadius is null)
        {
            byLightRadius = new();
            foreach (var item in Items)
            {
                var itemKey = item.LightRadius;

                if (!byLightRadius.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byLightRadius.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byLightRadius.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byLightRadius"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByLightRadius(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLightRadius(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown36(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byUnknown36 is null)
        {
            byUnknown36 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown36;

                if (!byUnknown36.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown36.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown36.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown40(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byUnknown40 is null)
        {
            byUnknown40 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown40;

                if (!byUnknown40.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown40.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown40.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown44(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;

                if (!byUnknown44.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown44.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out DelveLevelScalingDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown48(key, out var items))
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;

                if (!byUnknown48.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out DelveLevelScalingDat? item)
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
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
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out DelveLevelScalingDat? item)
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
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<DelveLevelScalingDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<DelveLevelScalingDat>();
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
            items = Array.Empty<DelveLevelScalingDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="DelveLevelScalingDat"/> with <see cref="DelveLevelScalingDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, DelveLevelScalingDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, DelveLevelScalingDat>>();
        }

        var items = new List<ResultItem<int, DelveLevelScalingDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, DelveLevelScalingDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private DelveLevelScalingDat[] Load()
    {
        const string filePath = "Data/DelveLevelScaling.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveLevelScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Depth
            (var depthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterLevel
            (var monsterlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SulphiteCost
            (var sulphitecostLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterLevel2
            (var monsterlevel2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MoreMonsterDamage
            (var moremonsterdamageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MoreMonsterLife
            (var moremonsterlifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DarknessResistance
            (var darknessresistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LightRadius
            (var lightradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveLevelScalingDat()
            {
                Depth = depthLoading,
                MonsterLevel = monsterlevelLoading,
                Unknown8 = unknown8Loading,
                SulphiteCost = sulphitecostLoading,
                MonsterLevel2 = monsterlevel2Loading,
                MoreMonsterDamage = moremonsterdamageLoading,
                MoreMonsterLife = moremonsterlifeLoading,
                DarknessResistance = darknessresistanceLoading,
                LightRadius = lightradiusLoading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
