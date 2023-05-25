using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="HeistBalancePerLevelDat"/> related data and helper methods.
/// </summary>
public sealed class HeistBalancePerLevelRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<HeistBalancePerLevelDat> Items { get; }

    private Dictionary<int, List<HeistBalancePerLevelDat>>? byLevel;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown4;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown8;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byUnknown12;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byUnknown16;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown20;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown24;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey1;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey2;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey3;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey4;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey5;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown108;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown112;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown116;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown120;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey6;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byHeistValueScalingKey7;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown156;
    private Dictionary<float, List<HeistBalancePerLevelDat>>? byUnknown160;
    private Dictionary<int, List<HeistBalancePerLevelDat>>? byUnknown164;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistBalancePerLevelRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal HeistBalancePerLevelRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLevel(int? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Level"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLevel(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
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
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byLevel"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByLevel(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLevel(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(float? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown4(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;

                if (!byUnknown4.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown4.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown4(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown8(float? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown8"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown8(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
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
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown8"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown8(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown8(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown12(int? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown12"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown12(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
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
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown12"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByUnknown12(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown12(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
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
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(float? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
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
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown20(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(float? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
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
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown24(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey1(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey1(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey1(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey1 is null)
        {
            byHeistValueScalingKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey2(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey2(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey2(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey2 is null)
        {
            byHeistValueScalingKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey3(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey3(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey3"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey3(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey3 is null)
        {
            byHeistValueScalingKey3 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey3;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey3.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey3.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey3.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey3"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey3(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey3(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey4(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey4(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey4(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey4 is null)
        {
            byHeistValueScalingKey4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey5(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey5(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey5"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey5(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey5 is null)
        {
            byHeistValueScalingKey5 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey5;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey5.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey5.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey5.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey5"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey5(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey5(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(float? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown108 is null)
        {
            byUnknown108 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown108;

                if (!byUnknown108.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown108.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown108.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown108(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(float? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown112(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;

                if (!byUnknown112.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown112(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown116(float? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown116(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown116"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown116(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown116 is null)
        {
            byUnknown116 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown116;

                if (!byUnknown116.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown116.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown116.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown116"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown116(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown116(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown120(float? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown120(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown120"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown120(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown120 is null)
        {
            byUnknown120 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown120;

                if (!byUnknown120.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown120.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown120.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown120"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown120(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown120(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey6(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey6(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey6"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey6(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey6 is null)
        {
            byHeistValueScalingKey6 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey6;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey6.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey6.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey6.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey6"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey6(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey6(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey7"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByHeistValueScalingKey7(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByHeistValueScalingKey7(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.HeistValueScalingKey7"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByHeistValueScalingKey7(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byHeistValueScalingKey7 is null)
        {
            byHeistValueScalingKey7 = new();
            foreach (var item in Items)
            {
                var itemKey = item.HeistValueScalingKey7;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byHeistValueScalingKey7.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byHeistValueScalingKey7.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byHeistValueScalingKey7.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byHeistValueScalingKey7"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByHeistValueScalingKey7(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByHeistValueScalingKey7(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown156(float? key, out HeistBalancePerLevelDat? item)
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown156"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown156(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown156 is null)
        {
            byUnknown156 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown156;

                if (!byUnknown156.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown156.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown156.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown156"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown156(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown156(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown160(float? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown160(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown160(float? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown160 is null)
        {
            byUnknown160 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown160;

                if (!byUnknown160.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown160.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown160.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown160"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, HeistBalancePerLevelDat>> GetManyToManyByUnknown160(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<float, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown160(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown164(int? key, out HeistBalancePerLevelDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown164(key, out var items))
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
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown164(int? key, out IReadOnlyList<HeistBalancePerLevelDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        if (byUnknown164 is null)
        {
            byUnknown164 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown164;

                if (!byUnknown164.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown164.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown164.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<HeistBalancePerLevelDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="HeistBalancePerLevelDat"/> with <see cref="HeistBalancePerLevelDat.byUnknown164"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, HeistBalancePerLevelDat>> GetManyToManyByUnknown164(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, HeistBalancePerLevelDat>>();
        }

        var items = new List<ResultItem<int, HeistBalancePerLevelDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown164(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, HeistBalancePerLevelDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private HeistBalancePerLevelDat[] Load()
    {
        const string filePath = "Data/HeistBalancePerLevel.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistBalancePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading HeistValueScalingKey1
            (var heistvaluescalingkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistValueScalingKey2
            (var heistvaluescalingkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistValueScalingKey3
            (var heistvaluescalingkey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistValueScalingKey4
            (var heistvaluescalingkey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistValueScalingKey5
            (var heistvaluescalingkey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading HeistValueScalingKey6
            (var heistvaluescalingkey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistValueScalingKey7
            (var heistvaluescalingkey7Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown156
            (var unknown156Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistBalancePerLevelDat()
            {
                Level = levelLoading,
                Unknown4 = unknown4Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                HeistValueScalingKey1 = heistvaluescalingkey1Loading,
                HeistValueScalingKey2 = heistvaluescalingkey2Loading,
                HeistValueScalingKey3 = heistvaluescalingkey3Loading,
                HeistValueScalingKey4 = heistvaluescalingkey4Loading,
                HeistValueScalingKey5 = heistvaluescalingkey5Loading,
                Unknown108 = unknown108Loading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown120 = unknown120Loading,
                HeistValueScalingKey6 = heistvaluescalingkey6Loading,
                HeistValueScalingKey7 = heistvaluescalingkey7Loading,
                Unknown156 = unknown156Loading,
                Unknown160 = unknown160Loading,
                Unknown164 = unknown164Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
