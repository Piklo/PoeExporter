using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LegionMonsterVarietiesDat"/> related data and helper methods.
/// </summary>
public sealed class LegionMonsterVarietiesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LegionMonsterVarietiesDat> Items { get; }

    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byLegionFactionsKey;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byLegionRanksKey;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown48;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byMiscAnimatedKey;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown68;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown72;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown76;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown92;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown108;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown124;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown140;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown156;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown172;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown176;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byUnknown180;
    private Dictionary<int, List<LegionMonsterVarietiesDat>>? byMonsterVarietiesKey2;

    /// <summary>
    /// Initializes a new instance of the <see cref="LegionMonsterVarietiesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LegionMonsterVarietiesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out LegionMonsterVarietiesDat? item)
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
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
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.LegionFactionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLegionFactionsKey(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLegionFactionsKey(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.LegionFactionsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLegionFactionsKey(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byLegionFactionsKey is null)
        {
            byLegionFactionsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.LegionFactionsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLegionFactionsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLegionFactionsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLegionFactionsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byLegionFactionsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByLegionFactionsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLegionFactionsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.LegionRanksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLegionRanksKey(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLegionRanksKey(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.LegionRanksKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLegionRanksKey(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byLegionRanksKey is null)
        {
            byLegionRanksKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.LegionRanksKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLegionRanksKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLegionRanksKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLegionRanksKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byLegionRanksKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByLegionRanksKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLegionRanksKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out LegionMonsterVarietiesDat? item)
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
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
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscAnimatedKey(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byMiscAnimatedKey is null)
        {
            byMiscAnimatedKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey;
                foreach (var listKey in itemKey)
                {
                    if (!byMiscAnimatedKey.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byMiscAnimatedKey.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byMiscAnimatedKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byMiscAnimatedKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByMiscAnimatedKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown76(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown76.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown76.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out LegionMonsterVarietiesDat? item)
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown92.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown92.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out LegionMonsterVarietiesDat? item)
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown108.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown108.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out LegionMonsterVarietiesDat? item)
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown124.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown124.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown140(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown140(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown140(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown140 is null)
        {
            byUnknown140 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown140;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown140.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown140.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown140.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown140"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown140(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown140(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown156(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown156(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown156(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown156 is null)
        {
            byUnknown156 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown156;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown156.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown156.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown156.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown156"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown156(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown156(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown172(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown172(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown172(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown172 is null)
        {
            byUnknown172 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown172;

                if (!byUnknown172.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown172.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown172.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown172"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown172(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown172(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown176(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown176(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown176(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown176 is null)
        {
            byUnknown176 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown176;

                if (!byUnknown176.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown176.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown176.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown176"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown176(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown176(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown180(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown180(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.Unknown180"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown180(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byUnknown180 is null)
        {
            byUnknown180 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown180;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown180.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown180.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown180.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byUnknown180"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByUnknown180(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown180(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.MonsterVarietiesKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey2(int? key, out LegionMonsterVarietiesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonsterVarietiesKey2(key, out var items))
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
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.MonsterVarietiesKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey2(int? key, out IReadOnlyList<LegionMonsterVarietiesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        if (byMonsterVarietiesKey2 is null)
        {
            byMonsterVarietiesKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.MonsterVarietiesKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonsterVarietiesKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonsterVarietiesKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonsterVarietiesKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LegionMonsterVarietiesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LegionMonsterVarietiesDat"/> with <see cref="LegionMonsterVarietiesDat.byMonsterVarietiesKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LegionMonsterVarietiesDat>> GetManyToManyByMonsterVarietiesKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LegionMonsterVarietiesDat>>();
        }

        var items = new List<ResultItem<int, LegionMonsterVarietiesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LegionMonsterVarietiesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LegionMonsterVarietiesDat[] Load()
    {
        const string filePath = "Data/LegionMonsterVarieties.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionMonsterVarietiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LegionFactionsKey
            (var legionfactionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LegionRanksKey
            (var legionrankskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimatedKey
            (var tempmiscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var miscanimatedkeyLoading = tempmiscanimatedkeyLoading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading Unknown92
            (var tempunknown92Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown92Loading = tempunknown92Loading.AsReadOnly();

            // loading Unknown108
            (var tempunknown108Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown108Loading = tempunknown108Loading.AsReadOnly();

            // loading Unknown124
            (var tempunknown124Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown124Loading = tempunknown124Loading.AsReadOnly();

            // loading Unknown140
            (var tempunknown140Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown140Loading = tempunknown140Loading.AsReadOnly();

            // loading Unknown156
            (var tempunknown156Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown156Loading = tempunknown156Loading.AsReadOnly();

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown180
            (var tempunknown180Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown180Loading = tempunknown180Loading.AsReadOnly();

            // loading MonsterVarietiesKey2
            (var monstervarietieskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionMonsterVarietiesDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                LegionFactionsKey = legionfactionskeyLoading,
                LegionRanksKey = legionrankskeyLoading,
                Unknown48 = unknown48Loading,
                MiscAnimatedKey = miscanimatedkeyLoading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown92 = unknown92Loading,
                Unknown108 = unknown108Loading,
                Unknown124 = unknown124Loading,
                Unknown140 = unknown140Loading,
                Unknown156 = unknown156Loading,
                Unknown172 = unknown172Loading,
                Unknown176 = unknown176Loading,
                Unknown180 = unknown180Loading,
                MonsterVarietiesKey2 = monstervarietieskey2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
