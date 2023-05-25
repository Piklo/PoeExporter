using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="SummonedSpecificMonstersDat"/> related data and helper methods.
/// </summary>
public sealed class SummonedSpecificMonstersRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<SummonedSpecificMonstersDat> Items { get; }

    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byId;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byMonsterVarietiesKey;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown20;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown24;
    private Dictionary<bool, List<SummonedSpecificMonstersDat>>? byUnknown40;
    private Dictionary<bool, List<SummonedSpecificMonstersDat>>? byUnknown41;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown42;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown46;
    private Dictionary<bool, List<SummonedSpecificMonstersDat>>? byUnknown50;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown51;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown67;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown83;
    private Dictionary<bool, List<SummonedSpecificMonstersDat>>? byUnknown87;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown88;
    private Dictionary<string, List<SummonedSpecificMonstersDat>>? byUnknown92;
    private Dictionary<bool, List<SummonedSpecificMonstersDat>>? byUnknown100;
    private Dictionary<bool, List<SummonedSpecificMonstersDat>>? byUnknown101;
    private Dictionary<int, List<SummonedSpecificMonstersDat>>? byUnknown102;

    /// <summary>
    /// Initializes a new instance of the <see cref="SummonedSpecificMonstersRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal SummonedSpecificMonstersRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
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

        if (!byId.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyById(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMonsterVarietiesKey(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMonsterVarietiesKey(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
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
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byMonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByMonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown20(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;

                if (!byUnknown20.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown24 is null)
        {
            byUnknown24 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown24;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown24.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown24.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown24.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(bool? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(bool? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
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
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SummonedSpecificMonstersDat>> GetManyToManyByUnknown40(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<bool, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown41(bool? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown41(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown41"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown41(bool? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown41 is null)
        {
            byUnknown41 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown41;

                if (!byUnknown41.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown41.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown41.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown41"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SummonedSpecificMonstersDat>> GetManyToManyByUnknown41(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<bool, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown41(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown42(int? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown42(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown42(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown42 is null)
        {
            byUnknown42 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown42;

                if (!byUnknown42.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown42.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown42.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown42"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown42(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown42(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown46(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown46"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown46(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
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
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown46"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown46(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown46(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown50(bool? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown50"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown50(bool? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown50 is null)
        {
            byUnknown50 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown50;

                if (!byUnknown50.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown50.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown50.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown50"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SummonedSpecificMonstersDat>> GetManyToManyByUnknown50(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<bool, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown50(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown51(int? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown51(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown51"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown51(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown51 is null)
        {
            byUnknown51 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown51;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown51.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown51.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown51.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown51"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown51(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown51(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown67(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown67"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown67(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown67 is null)
        {
            byUnknown67 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown67;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown67.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown67.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown67.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown67"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown67(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown67(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown83"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown83(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown83"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown83(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
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
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown83"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown83(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown83(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown87(bool? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown87(bool? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
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
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown87"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SummonedSpecificMonstersDat>> GetManyToManyByUnknown87(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<bool, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown87(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown88 is null)
        {
            byUnknown88 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown88;

                if (!byUnknown88.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown88.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown88.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(string? key, out SummonedSpecificMonstersDat? item)
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(string? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown92 is null)
        {
            byUnknown92 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown92;

                if (!byUnknown92.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown92.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown92.TryGetValue(key, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, SummonedSpecificMonstersDat>> GetManyToManyByUnknown92(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<string, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown100(bool? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown100(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown100"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown100(bool? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown100 is null)
        {
            byUnknown100 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown100;

                if (!byUnknown100.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown100.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown100.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown100"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SummonedSpecificMonstersDat>> GetManyToManyByUnknown100(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<bool, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown100(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown101(bool? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown101(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown101(bool? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown101 is null)
        {
            byUnknown101 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown101;

                if (!byUnknown101.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown101.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown101.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown101"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, SummonedSpecificMonstersDat>> GetManyToManyByUnknown101(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<bool, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown101(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown102(int? key, out SummonedSpecificMonstersDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown102(key, out var items))
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
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown102(int? key, out IReadOnlyList<SummonedSpecificMonstersDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        if (byUnknown102 is null)
        {
            byUnknown102 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown102;

                if (!byUnknown102.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown102.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown102.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<SummonedSpecificMonstersDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="SummonedSpecificMonstersDat"/> with <see cref="SummonedSpecificMonstersDat.byUnknown102"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, SummonedSpecificMonstersDat>> GetManyToManyByUnknown102(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, SummonedSpecificMonstersDat>>();
        }

        var items = new List<ResultItem<int, SummonedSpecificMonstersDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown102(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, SummonedSpecificMonstersDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private SummonedSpecificMonstersDat[] Load()
    {
        const string filePath = "Data/SummonedSpecificMonsters.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SummonedSpecificMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown51
            (var unknown51Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SummonedSpecificMonstersDat()
            {
                Id = idLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown41 = unknown41Loading,
                Unknown42 = unknown42Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                Unknown51 = unknown51Loading,
                Unknown67 = unknown67Loading,
                Unknown83 = unknown83Loading,
                Unknown87 = unknown87Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown100 = unknown100Loading,
                Unknown101 = unknown101Loading,
                Unknown102 = unknown102Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
