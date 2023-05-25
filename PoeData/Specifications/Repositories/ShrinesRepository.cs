using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ShrinesDat"/> related data and helper methods.
/// </summary>
public sealed class ShrinesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ShrinesDat> Items { get; }

    private Dictionary<string, List<ShrinesDat>>? byId;
    private Dictionary<int, List<ShrinesDat>>? byTimeoutInSeconds;
    private Dictionary<bool, List<ShrinesDat>>? byChargesShared;
    private Dictionary<int, List<ShrinesDat>>? byPlayer_ShrineBuffsKey;
    private Dictionary<int, List<ShrinesDat>>? byUnknown29;
    private Dictionary<int, List<ShrinesDat>>? byUnknown33;
    private Dictionary<int, List<ShrinesDat>>? byMonster_ShrineBuffsKey;
    private Dictionary<int, List<ShrinesDat>>? bySummonMonster_MonsterVarietiesKey;
    private Dictionary<int, List<ShrinesDat>>? bySummonPlayer_MonsterVarietiesKey;
    private Dictionary<int, List<ShrinesDat>>? byUnknown85;
    private Dictionary<int, List<ShrinesDat>>? byUnknown89;
    private Dictionary<int, List<ShrinesDat>>? byShrineSoundsKey;
    private Dictionary<bool, List<ShrinesDat>>? byUnknown109;
    private Dictionary<int, List<ShrinesDat>>? byAchievementItemsKeys;
    private Dictionary<bool, List<ShrinesDat>>? byIsPVPOnly;
    private Dictionary<bool, List<ShrinesDat>>? byUnknown127;
    private Dictionary<bool, List<ShrinesDat>>? byIsLesserShrine;
    private Dictionary<int, List<ShrinesDat>>? byDescription;
    private Dictionary<int, List<ShrinesDat>>? byName;
    private Dictionary<bool, List<ShrinesDat>>? byUnknown161;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShrinesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ShrinesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ShrinesDat? item)
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
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
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ShrinesDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ShrinesDat>>();
        }

        var items = new List<ResultItem<string, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.TimeoutInSeconds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTimeoutInSeconds(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTimeoutInSeconds(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.TimeoutInSeconds"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTimeoutInSeconds(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byTimeoutInSeconds is null)
        {
            byTimeoutInSeconds = new();
            foreach (var item in Items)
            {
                var itemKey = item.TimeoutInSeconds;

                if (!byTimeoutInSeconds.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTimeoutInSeconds.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTimeoutInSeconds.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byTimeoutInSeconds"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByTimeoutInSeconds(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTimeoutInSeconds(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.ChargesShared"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByChargesShared(bool? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByChargesShared(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.ChargesShared"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByChargesShared(bool? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byChargesShared is null)
        {
            byChargesShared = new();
            foreach (var item in Items)
            {
                var itemKey = item.ChargesShared;

                if (!byChargesShared.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byChargesShared.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byChargesShared.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byChargesShared"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShrinesDat>> GetManyToManyByChargesShared(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShrinesDat>>();
        }

        var items = new List<ResultItem<bool, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByChargesShared(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Player_ShrineBuffsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByPlayer_ShrineBuffsKey(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByPlayer_ShrineBuffsKey(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Player_ShrineBuffsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByPlayer_ShrineBuffsKey(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byPlayer_ShrineBuffsKey is null)
        {
            byPlayer_ShrineBuffsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Player_ShrineBuffsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byPlayer_ShrineBuffsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byPlayer_ShrineBuffsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byPlayer_ShrineBuffsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byPlayer_ShrineBuffsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByPlayer_ShrineBuffsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByPlayer_ShrineBuffsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown29(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;

                if (!byUnknown29.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown29.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByUnknown29(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown33(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown33(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown33"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown33(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown33 is null)
        {
            byUnknown33 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown33;

                if (!byUnknown33.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown33.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown33.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown33"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByUnknown33(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown33(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Monster_ShrineBuffsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonster_ShrineBuffsKey(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMonster_ShrineBuffsKey(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Monster_ShrineBuffsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonster_ShrineBuffsKey(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byMonster_ShrineBuffsKey is null)
        {
            byMonster_ShrineBuffsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Monster_ShrineBuffsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMonster_ShrineBuffsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMonster_ShrineBuffsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMonster_ShrineBuffsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byMonster_ShrineBuffsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByMonster_ShrineBuffsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonster_ShrineBuffsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.SummonMonster_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySummonMonster_MonsterVarietiesKey(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySummonMonster_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.SummonMonster_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySummonMonster_MonsterVarietiesKey(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (bySummonMonster_MonsterVarietiesKey is null)
        {
            bySummonMonster_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SummonMonster_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySummonMonster_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySummonMonster_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySummonMonster_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.bySummonMonster_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyBySummonMonster_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySummonMonster_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.SummonPlayer_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetBySummonPlayer_MonsterVarietiesKey(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyBySummonPlayer_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.SummonPlayer_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyBySummonPlayer_MonsterVarietiesKey(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (bySummonPlayer_MonsterVarietiesKey is null)
        {
            bySummonPlayer_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.SummonPlayer_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!bySummonPlayer_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    bySummonPlayer_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!bySummonPlayer_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.bySummonPlayer_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyBySummonPlayer_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyBySummonPlayer_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown85"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown85(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown85(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown85"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown85(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown85 is null)
        {
            byUnknown85 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown85;

                if (!byUnknown85.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown85.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown85.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown85"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByUnknown85(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown85(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown89(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown89(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown89"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown89(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown89 is null)
        {
            byUnknown89 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown89;

                if (!byUnknown89.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown89.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown89.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown89"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByUnknown89(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown89(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.ShrineSoundsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByShrineSoundsKey(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByShrineSoundsKey(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.ShrineSoundsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByShrineSoundsKey(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byShrineSoundsKey is null)
        {
            byShrineSoundsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.ShrineSoundsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byShrineSoundsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byShrineSoundsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byShrineSoundsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byShrineSoundsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByShrineSoundsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByShrineSoundsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(bool? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown109(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(bool? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown109 is null)
        {
            byUnknown109 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown109;

                if (!byUnknown109.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown109.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown109.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShrinesDat>> GetManyToManyByUnknown109(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShrinesDat>>();
        }

        var items = new List<ResultItem<bool, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAchievementItemsKeys(int? key, out ShrinesDat? item)
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.AchievementItemsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAchievementItemsKeys(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
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
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byAchievementItemsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByAchievementItemsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAchievementItemsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.IsPVPOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsPVPOnly(bool? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsPVPOnly(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.IsPVPOnly"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsPVPOnly(bool? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byIsPVPOnly is null)
        {
            byIsPVPOnly = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsPVPOnly;

                if (!byIsPVPOnly.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsPVPOnly.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsPVPOnly.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byIsPVPOnly"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShrinesDat>> GetManyToManyByIsPVPOnly(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShrinesDat>>();
        }

        var items = new List<ResultItem<bool, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsPVPOnly(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown127"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown127(bool? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown127(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown127"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown127(bool? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown127 is null)
        {
            byUnknown127 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown127;

                if (!byUnknown127.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown127.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown127.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown127"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShrinesDat>> GetManyToManyByUnknown127(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShrinesDat>>();
        }

        var items = new List<ResultItem<bool, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown127(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.IsLesserShrine"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsLesserShrine(bool? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsLesserShrine(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.IsLesserShrine"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsLesserShrine(bool? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byIsLesserShrine is null)
        {
            byIsLesserShrine = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsLesserShrine;

                if (!byIsLesserShrine.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsLesserShrine.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsLesserShrine.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byIsLesserShrine"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShrinesDat>> GetManyToManyByIsLesserShrine(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShrinesDat>>();
        }

        var items = new List<ResultItem<bool, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsLesserShrine(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(int? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByDescription(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byDescription.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByDescription(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(int? key, out ShrinesDat? item)
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(int? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byName.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ShrinesDat>> GetManyToManyByName(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ShrinesDat>>();
        }

        var items = new List<ResultItem<int, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown161"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown161(bool? key, out ShrinesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown161(key, out var items))
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
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.Unknown161"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown161(bool? key, out IReadOnlyList<ShrinesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        if (byUnknown161 is null)
        {
            byUnknown161 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown161;

                if (!byUnknown161.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown161.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown161.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ShrinesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ShrinesDat"/> with <see cref="ShrinesDat.byUnknown161"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ShrinesDat>> GetManyToManyByUnknown161(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ShrinesDat>>();
        }

        var items = new List<ResultItem<bool, ShrinesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown161(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ShrinesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ShrinesDat[] Load()
    {
        const string filePath = "Data/Shrines.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShrinesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TimeoutInSeconds
            (var timeoutinsecondsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChargesShared
            (var chargessharedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Player_ShrineBuffsKey
            (var player_shrinebuffskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Monster_ShrineBuffsKey
            (var monster_shrinebuffskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SummonMonster_MonsterVarietiesKey
            (var summonmonster_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SummonPlayer_MonsterVarietiesKey
            (var summonplayer_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ShrineSoundsKey
            (var shrinesoundskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading IsPVPOnly
            (var ispvponlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown127
            (var unknown127Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLesserShrine
            (var islessershrineLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown161
            (var unknown161Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShrinesDat()
            {
                Id = idLoading,
                TimeoutInSeconds = timeoutinsecondsLoading,
                ChargesShared = chargessharedLoading,
                Player_ShrineBuffsKey = player_shrinebuffskeyLoading,
                Unknown29 = unknown29Loading,
                Unknown33 = unknown33Loading,
                Monster_ShrineBuffsKey = monster_shrinebuffskeyLoading,
                SummonMonster_MonsterVarietiesKey = summonmonster_monstervarietieskeyLoading,
                SummonPlayer_MonsterVarietiesKey = summonplayer_monstervarietieskeyLoading,
                Unknown85 = unknown85Loading,
                Unknown89 = unknown89Loading,
                ShrineSoundsKey = shrinesoundskeyLoading,
                Unknown109 = unknown109Loading,
                AchievementItemsKeys = achievementitemskeysLoading,
                IsPVPOnly = ispvponlyLoading,
                Unknown127 = unknown127Loading,
                IsLesserShrine = islessershrineLoading,
                Description = descriptionLoading,
                Name = nameLoading,
                Unknown161 = unknown161Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
