using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="MonsterDeathConditionsDat"/> related data and helper methods.
/// </summary>
public sealed class MonsterDeathConditionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<MonsterDeathConditionsDat> Items { get; }

    private Dictionary<string, List<MonsterDeathConditionsDat>>? byUnknown0;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown8;
    private Dictionary<bool, List<MonsterDeathConditionsDat>>? byUnknown24;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown25;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown29;
    private Dictionary<bool, List<MonsterDeathConditionsDat>>? byUnknown45;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown46;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown50;
    private Dictionary<bool, List<MonsterDeathConditionsDat>>? byUnknown66;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown67;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown83;
    private Dictionary<bool, List<MonsterDeathConditionsDat>>? byUnknown87;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown88;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown104;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown108;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown124;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown140;
    private Dictionary<int, List<MonsterDeathConditionsDat>>? byUnknown144;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonsterDeathConditionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal MonsterDeathConditionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(string? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown0(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(string? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;

                if (!byUnknown0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, MonsterDeathConditionsDat>> GetManyToManyByUnknown0(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<string, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(int? key, out MonsterDeathConditionsDat? item)
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown8 is null)
        {
            byUnknown8 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown8;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown8.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown8.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown8.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown8(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(bool? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown24(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(bool? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;

                if (!byUnknown24.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathConditionsDat>> GetManyToManyByUnknown24(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown25(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown25 is null)
        {
            byUnknown25 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown25;

                if (!byUnknown25.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown25.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown25.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown25(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown29(int? key, out MonsterDeathConditionsDat? item)
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown29"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown29(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown29 is null)
        {
            byUnknown29 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown29;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown29.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown29.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown29.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown29"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown29(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown29(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(bool? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown45(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(bool? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown45 is null)
        {
            byUnknown45 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown45;

                if (!byUnknown45.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown45.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown45.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathConditionsDat>> GetManyToManyByUnknown45(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown46(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown46(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown46(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown46 is null)
        {
            byUnknown46 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown46;

                if (!byUnknown46.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown46.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown46.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown46"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown46(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown46(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown50(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown50(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown50(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown50 is null)
        {
            byUnknown50 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown50;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown50.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown50.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown50.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown50"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown50(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown50(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown66(bool? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown66(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown66"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown66(bool? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown66 is null)
        {
            byUnknown66 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown66;

                if (!byUnknown66.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown66.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown66.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown66"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathConditionsDat>> GetManyToManyByUnknown66(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown66(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown67(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown67(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown67(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown67 is null)
        {
            byUnknown67 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown67;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown67.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown67.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown67.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown67"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown67(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown67(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown83"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown83(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown83(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown83"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown83(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown83 is null)
        {
            byUnknown83 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown83;

                if (!byUnknown83.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown83.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown83.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown83"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown83(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown83(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown87(bool? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown87(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown87(bool? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown87 is null)
        {
            byUnknown87 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown87;

                if (!byUnknown87.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown87.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown87.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown87"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, MonsterDeathConditionsDat>> GetManyToManyByUnknown87(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<bool, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown87(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out MonsterDeathConditionsDat? item)
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown88.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown88.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown104(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown104(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown104"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown104(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown104 is null)
        {
            byUnknown104 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown104;

                if (!byUnknown104.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown104.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown104.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown104"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown104(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown104(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(int? key, out MonsterDeathConditionsDat? item)
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown108.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown108(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown124(int? key, out MonsterDeathConditionsDat? item)
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown124"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown124(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown124 is null)
        {
            byUnknown124 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown124;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown124.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown124.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown124.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown124"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown124(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown124(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown140(int? key, out MonsterDeathConditionsDat? item)
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown140(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown140 is null)
        {
            byUnknown140 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown140;

                if (!byUnknown140.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown140.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown140.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown140"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown140(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown140(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(int? key, out MonsterDeathConditionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown144(key, out var items))
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
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(int? key, out IReadOnlyList<MonsterDeathConditionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;

                if (!byUnknown144.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<MonsterDeathConditionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="MonsterDeathConditionsDat"/> with <see cref="MonsterDeathConditionsDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, MonsterDeathConditionsDat>> GetManyToManyByUnknown144(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, MonsterDeathConditionsDat>>();
        }

        var items = new List<ResultItem<int, MonsterDeathConditionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, MonsterDeathConditionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private MonsterDeathConditionsDat[] Load()
    {
        const string filePath = "Data/MonsterDeathConditions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterDeathConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown29
            (var tempunknown29Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown29Loading = tempunknown29Loading.AsReadOnly();

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown67
            (var tempunknown67Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown67Loading = tempunknown67Loading.AsReadOnly();

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown88
            (var tempunknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown88Loading = tempunknown88Loading.AsReadOnly();

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterDeathConditionsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown25 = unknown25Loading,
                Unknown29 = unknown29Loading,
                Unknown45 = unknown45Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                Unknown66 = unknown66Loading,
                Unknown67 = unknown67Loading,
                Unknown83 = unknown83Loading,
                Unknown87 = unknown87Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
                Unknown108 = unknown108Loading,
                Unknown124 = unknown124Loading,
                Unknown140 = unknown140Loading,
                Unknown144 = unknown144Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
