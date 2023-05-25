using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="WarbandsPackMonstersDat"/> related data and helper methods.
/// </summary>
public sealed class WarbandsPackMonstersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<WarbandsPackMonstersDat> Items { get; }

    private Dictionary<string, List<WarbandsPackMonstersDat>>? byId;
    private Dictionary<int, List<WarbandsPackMonstersDat>>? byUnknown8;
    private Dictionary<bool, List<WarbandsPackMonstersDat>>? byUnknown12;
    private Dictionary<bool, List<WarbandsPackMonstersDat>>? byUnknown13;
    private Dictionary<bool, List<WarbandsPackMonstersDat>>? byUnknown14;
    private Dictionary<bool, List<WarbandsPackMonstersDat>>? byUnknown15;
    private Dictionary<int, List<WarbandsPackMonstersDat>>? byUnknown16;
    private Dictionary<int, List<WarbandsPackMonstersDat>>? byTier4_MonsterVarietiesKeys;
    private Dictionary<int, List<WarbandsPackMonstersDat>>? byTier3_MonsterVarietiesKeys;
    private Dictionary<int, List<WarbandsPackMonstersDat>>? byTier2_MonsterVarietiesKeys;
    private Dictionary<int, List<WarbandsPackMonstersDat>>? byTier1_MonsterVarietiesKeys;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier1Name;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier2Name;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier3Name;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier4Name;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier1Art;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier2Art;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier3Art;
    private Dictionary<string, List<WarbandsPackMonstersDat>>? byTier4Art;

    /// <summary>
    /// Initializes a new instance of the <see cref="WarbandsPackMonstersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal WarbandsPackMonstersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out WarbandsPackMonstersDat? item)
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
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
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out WarbandsPackMonstersDat? item)
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
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
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackMonstersDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(bool? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown12(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(bool? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byUnknown12 is null)
        {
            byUnknown12 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown12;

                if (!byUnknown12.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown12.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown12.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WarbandsPackMonstersDat>> GetManyToManyByUnknown12(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<bool, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown13"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown13(bool? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown13(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown13"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown13(bool? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byUnknown13 is null)
        {
            byUnknown13 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown13;

                if (!byUnknown13.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown13.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown13.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byUnknown13"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WarbandsPackMonstersDat>> GetManyToManyByUnknown13(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<bool, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown13(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown14"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown14(bool? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown14(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown14"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown14(bool? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byUnknown14 is null)
        {
            byUnknown14 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown14;

                if (!byUnknown14.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown14.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown14.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byUnknown14"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WarbandsPackMonstersDat>> GetManyToManyByUnknown14(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<bool, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown14(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown15"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown15(bool? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown15(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown15"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown15(bool? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byUnknown15 is null)
        {
            byUnknown15 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown15;

                if (!byUnknown15.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown15.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown15.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byUnknown15"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, WarbandsPackMonstersDat>> GetManyToManyByUnknown15(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<bool, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown15(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown16(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;

                if (!byUnknown16.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackMonstersDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier4_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier4_MonsterVarietiesKeys(int? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier4_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier4_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier4_MonsterVarietiesKeys(int? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier4_MonsterVarietiesKeys is null)
        {
            byTier4_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier4_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTier4_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTier4_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTier4_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier4_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackMonstersDat>> GetManyToManyByTier4_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier4_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier3_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier3_MonsterVarietiesKeys(int? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier3_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier3_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier3_MonsterVarietiesKeys(int? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier3_MonsterVarietiesKeys is null)
        {
            byTier3_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier3_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTier3_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTier3_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTier3_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier3_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackMonstersDat>> GetManyToManyByTier3_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier3_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier2_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier2_MonsterVarietiesKeys(int? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier2_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier2_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier2_MonsterVarietiesKeys(int? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier2_MonsterVarietiesKeys is null)
        {
            byTier2_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier2_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTier2_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTier2_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTier2_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier2_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackMonstersDat>> GetManyToManyByTier2_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier2_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier1_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier1_MonsterVarietiesKeys(int? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier1_MonsterVarietiesKeys(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier1_MonsterVarietiesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier1_MonsterVarietiesKeys(int? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier1_MonsterVarietiesKeys is null)
        {
            byTier1_MonsterVarietiesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier1_MonsterVarietiesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byTier1_MonsterVarietiesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byTier1_MonsterVarietiesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byTier1_MonsterVarietiesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier1_MonsterVarietiesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, WarbandsPackMonstersDat>> GetManyToManyByTier1_MonsterVarietiesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<int, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier1_MonsterVarietiesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier1Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier1Name(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier1Name(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier1Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier1Name(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier1Name is null)
        {
            byTier1Name = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier1Name;

                if (!byTier1Name.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier1Name.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier1Name.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier1Name"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier1Name(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier1Name(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier2Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier2Name(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier2Name(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier2Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier2Name(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier2Name is null)
        {
            byTier2Name = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier2Name;

                if (!byTier2Name.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier2Name.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier2Name.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier2Name"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier2Name(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier2Name(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier3Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier3Name(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier3Name(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier3Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier3Name(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier3Name is null)
        {
            byTier3Name = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier3Name;

                if (!byTier3Name.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier3Name.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier3Name.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier3Name"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier3Name(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier3Name(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier4Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier4Name(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier4Name(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier4Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier4Name(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier4Name is null)
        {
            byTier4Name = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier4Name;

                if (!byTier4Name.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier4Name.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier4Name.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier4Name"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier4Name(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier4Name(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier1Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier1Art(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier1Art(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier1Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier1Art(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier1Art is null)
        {
            byTier1Art = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier1Art;

                if (!byTier1Art.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier1Art.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier1Art.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier1Art"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier1Art(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier1Art(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier2Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier2Art(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier2Art(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier2Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier2Art(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier2Art is null)
        {
            byTier2Art = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier2Art;

                if (!byTier2Art.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier2Art.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier2Art.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier2Art"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier2Art(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier2Art(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier3Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier3Art(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier3Art(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier3Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier3Art(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier3Art is null)
        {
            byTier3Art = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier3Art;

                if (!byTier3Art.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier3Art.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier3Art.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier3Art"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier3Art(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier3Art(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier4Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByTier4Art(string? key, out WarbandsPackMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByTier4Art(key, out var items))
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
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.Tier4Art"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByTier4Art(string? key, out IReadOnlyList<WarbandsPackMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        if (byTier4Art is null)
        {
            byTier4Art = new();
            foreach (var item in Items)
            {
                var itemKey = item.Tier4Art;

                if (!byTier4Art.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byTier4Art.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byTier4Art.TryGetValue(key, out var temp))
        {
            items = Array.Empty<WarbandsPackMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="WarbandsPackMonstersDat"/> with <see cref="WarbandsPackMonstersDat.byTier4Art"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, WarbandsPackMonstersDat>> GetManyToManyByTier4Art(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, WarbandsPackMonstersDat>>();
        }

        var items = new List<ResultItem<string, WarbandsPackMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByTier4Art(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, WarbandsPackMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private WarbandsPackMonstersDat[] Load()
    {
        const string filePath = "Data/WarbandsPackMonsters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WarbandsPackMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown14
            (var unknown14Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown15
            (var unknown15Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier4_MonsterVarietiesKeys
            (var temptier4_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier4_monstervarietieskeysLoading = temptier4_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier3_MonsterVarietiesKeys
            (var temptier3_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier3_monstervarietieskeysLoading = temptier3_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier2_MonsterVarietiesKeys
            (var temptier2_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier2_monstervarietieskeysLoading = temptier2_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier1_MonsterVarietiesKeys
            (var temptier1_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier1_monstervarietieskeysLoading = temptier1_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier1Name
            (var tier1nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier2Name
            (var tier2nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier3Name
            (var tier3nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier4Name
            (var tier4nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier1Art
            (var tier1artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier2Art
            (var tier2artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier3Art
            (var tier3artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier4Art
            (var tier4artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WarbandsPackMonstersDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown13 = unknown13Loading,
                Unknown14 = unknown14Loading,
                Unknown15 = unknown15Loading,
                Unknown16 = unknown16Loading,
                Tier4_MonsterVarietiesKeys = tier4_monstervarietieskeysLoading,
                Tier3_MonsterVarietiesKeys = tier3_monstervarietieskeysLoading,
                Tier2_MonsterVarietiesKeys = tier2_monstervarietieskeysLoading,
                Tier1_MonsterVarietiesKeys = tier1_monstervarietieskeysLoading,
                Tier1Name = tier1nameLoading,
                Tier2Name = tier2nameLoading,
                Tier3Name = tier3nameLoading,
                Tier4Name = tier4nameLoading,
                Tier1Art = tier1artLoading,
                Tier2Art = tier2artLoading,
                Tier3Art = tier3artLoading,
                Tier4Art = tier4artLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
