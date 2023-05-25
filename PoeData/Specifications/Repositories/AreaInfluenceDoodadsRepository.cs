using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AreaInfluenceDoodadsDat"/> related data and helper methods.
/// </summary>
public sealed class AreaInfluenceDoodadsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AreaInfluenceDoodadsDat> Items { get; }

    private Dictionary<int, List<AreaInfluenceDoodadsDat>>? byStatsKey;
    private Dictionary<int, List<AreaInfluenceDoodadsDat>>? byStatValue;
    private Dictionary<float, List<AreaInfluenceDoodadsDat>>? byUnknown20;
    private Dictionary<string, List<AreaInfluenceDoodadsDat>>? byAOFiles;
    private Dictionary<int, List<AreaInfluenceDoodadsDat>>? byUnknown40;
    private Dictionary<bool, List<AreaInfluenceDoodadsDat>>? byUnknown44;
    private Dictionary<string, List<AreaInfluenceDoodadsDat>>? byUnknown45;
    private Dictionary<int, List<AreaInfluenceDoodadsDat>>? byUnknown53;

    /// <summary>
    /// Initializes a new instance of the <see cref="AreaInfluenceDoodadsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AreaInfluenceDoodadsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKey(int? key, out AreaInfluenceDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKey(key, out var items))
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKey(int? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        if (byStatsKey is null)
        {
            byStatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byStatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byStatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byStatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byStatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaInfluenceDoodadsDat>> GetManyToManyByStatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<int, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.StatValue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValue(int? key, out AreaInfluenceDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValue(key, out var items))
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.StatValue"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValue(int? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        if (byStatValue is null)
        {
            byStatValue = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValue;

                if (!byStatValue.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byStatValue.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byStatValue.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byStatValue"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaInfluenceDoodadsDat>> GetManyToManyByStatValue(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<int, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValue(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(float? key, out AreaInfluenceDoodadsDat? item)
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(float? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
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
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, AreaInfluenceDoodadsDat>> GetManyToManyByUnknown20(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<float, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFiles(string? key, out AreaInfluenceDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFiles(key, out var items))
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.AOFiles"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFiles(string? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        if (byAOFiles is null)
        {
            byAOFiles = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFiles;
                foreach (var listKey in itemKey)
                {
                    if (!byAOFiles.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAOFiles.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAOFiles.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byAOFiles"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AreaInfluenceDoodadsDat>> GetManyToManyByAOFiles(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<string, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFiles(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out AreaInfluenceDoodadsDat? item)
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
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
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaInfluenceDoodadsDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<int, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(bool? key, out AreaInfluenceDoodadsDat? item)
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(bool? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
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
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, AreaInfluenceDoodadsDat>> GetManyToManyByUnknown44(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<bool, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown45(string? key, out AreaInfluenceDoodadsDat? item)
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown45"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown45(string? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
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

        if (!byUnknown45.TryGetValue(key, out var temp))
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byUnknown45"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, AreaInfluenceDoodadsDat>> GetManyToManyByUnknown45(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<string, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown45(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown53"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown53(int? key, out AreaInfluenceDoodadsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown53(key, out var items))
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
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.Unknown53"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown53(int? key, out IReadOnlyList<AreaInfluenceDoodadsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        if (byUnknown53 is null)
        {
            byUnknown53 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown53;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown53.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown53.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown53.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaInfluenceDoodadsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaInfluenceDoodadsDat"/> with <see cref="AreaInfluenceDoodadsDat.byUnknown53"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaInfluenceDoodadsDat>> GetManyToManyByUnknown53(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaInfluenceDoodadsDat>>();
        }

        var items = new List<ResultItem<int, AreaInfluenceDoodadsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown53(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaInfluenceDoodadsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AreaInfluenceDoodadsDat[] Load()
    {
        const string filePath = "Data/AreaInfluenceDoodads.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AreaInfluenceDoodadsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown53
            (var unknown53Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AreaInfluenceDoodadsDat()
            {
                StatsKey = statskeyLoading,
                StatValue = statvalueLoading,
                Unknown20 = unknown20Loading,
                AOFiles = aofilesLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown53 = unknown53Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
