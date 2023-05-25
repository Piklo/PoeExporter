using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="BuffDefinitionsDat"/> related data and helper methods.
/// </summary>
public sealed class BuffDefinitionsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<BuffDefinitionsDat> Items { get; }

    private Dictionary<string, List<BuffDefinitionsDat>>? byId;
    private Dictionary<string, List<BuffDefinitionsDat>>? byDescription;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byInvisible;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byRemovable;
    private Dictionary<string, List<BuffDefinitionsDat>>? byName;
    private Dictionary<int, List<BuffDefinitionsDat>>? byStatsKeys;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown42;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown43;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown47;
    private Dictionary<int, List<BuffDefinitionsDat>>? byMaximum_StatsKey;
    private Dictionary<int, List<BuffDefinitionsDat>>? byCurrent_StatsKey;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown80;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown81;
    private Dictionary<int, List<BuffDefinitionsDat>>? byBuffVisualsKey;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown101;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown102;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown103;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown107;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown108;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown109;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown110;
    private Dictionary<int, List<BuffDefinitionsDat>>? byBuffLimit;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown115;
    private Dictionary<string, List<BuffDefinitionsDat>>? byId2;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byIsRecovery;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown125;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown126;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown142;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown143;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown147;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown148;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown149;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown153;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown169;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown170;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown171;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown187;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown188;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown204;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown220;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown236;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown252;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown253;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown254;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown255;
    private Dictionary<bool, List<BuffDefinitionsDat>>? byUnknown256;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown257;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown273;
    private Dictionary<string, List<BuffDefinitionsDat>>? byUnknown289;
    private Dictionary<int, List<BuffDefinitionsDat>>? byUnknown297;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuffDefinitionsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal BuffDefinitionsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffDefinitionsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<string, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByDescription(string? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Description"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByDescription(string? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byDescription is null)
        {
            byDescription = new();
            foreach (var item in Items)
            {
                var itemKey = item.Description;

                if (!byDescription.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byDescription.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byDescription.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byDescription"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffDefinitionsDat>> GetManyToManyByDescription(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<string, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByDescription(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Invisible"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByInvisible(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByInvisible(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Invisible"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByInvisible(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byInvisible is null)
        {
            byInvisible = new();
            foreach (var item in Items)
            {
                var itemKey = item.Invisible;

                if (!byInvisible.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byInvisible.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byInvisible.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byInvisible"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByInvisible(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByInvisible(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Removable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByRemovable(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByRemovable(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Removable"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByRemovable(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byRemovable is null)
        {
            byRemovable = new();
            foreach (var item in Items)
            {
                var itemKey = item.Removable;

                if (!byRemovable.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byRemovable.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byRemovable.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byRemovable"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByRemovable(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByRemovable(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByName(string? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Name"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByName(string? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byName is null)
        {
            byName = new();
            foreach (var item in Items)
            {
                var itemKey = item.Name;

                if (!byName.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byName.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byName.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byName"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffDefinitionsDat>> GetManyToManyByName(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<string, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByName(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatsKeys(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatsKeys(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.StatsKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatsKeys(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byStatsKeys is null)
        {
            byStatsKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatsKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byStatsKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatsKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatsKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byStatsKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByStatsKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatsKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown42(bool? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown42"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown42(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown42"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown42(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown42(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown43"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown43(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown43(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown43"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown43(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown43 is null)
        {
            byUnknown43 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown43;

                if (!byUnknown43.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown43.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown43.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown43"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown43(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown43(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown47(bool? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown47(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown47"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown47(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown47(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Maximum_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMaximum_StatsKey(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMaximum_StatsKey(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Maximum_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMaximum_StatsKey(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byMaximum_StatsKey is null)
        {
            byMaximum_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Maximum_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMaximum_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMaximum_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMaximum_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byMaximum_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByMaximum_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMaximum_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Current_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByCurrent_StatsKey(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByCurrent_StatsKey(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Current_StatsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByCurrent_StatsKey(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byCurrent_StatsKey is null)
        {
            byCurrent_StatsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Current_StatsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byCurrent_StatsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byCurrent_StatsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byCurrent_StatsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byCurrent_StatsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByCurrent_StatsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByCurrent_StatsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown80(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;

                if (!byUnknown80.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown80(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown81"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown81(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown81(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown81"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown81(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown81 is null)
        {
            byUnknown81 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown81;

                if (!byUnknown81.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown81.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown81.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown81"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown81(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown81(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.BuffVisualsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualsKey(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffVisualsKey(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.BuffVisualsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualsKey(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byBuffVisualsKey is null)
        {
            byBuffVisualsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffVisualsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffVisualsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffVisualsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffVisualsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byBuffVisualsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByBuffVisualsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown101(bool? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown101(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown101"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown101(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown101(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown102(bool? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown102(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown102"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown102(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown102(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown103(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown103(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown103(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown103 is null)
        {
            byUnknown103 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown103;

                if (!byUnknown103.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown103.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown103.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown103"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown103(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown103(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown107(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown107(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown107(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown107 is null)
        {
            byUnknown107 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown107;

                if (!byUnknown107.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown107.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown107.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown107"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown107(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown107(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown108(bool? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown108"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown108(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown108"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown108(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown108(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown109(bool? key, out BuffDefinitionsDat? item)
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown109"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown109(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
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
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown109"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown109(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown109(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown110(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown110(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown110(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown110 is null)
        {
            byUnknown110 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown110;

                if (!byUnknown110.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown110.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown110.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown110"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown110(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown110(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.BuffLimit"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffLimit(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffLimit(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.BuffLimit"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffLimit(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byBuffLimit is null)
        {
            byBuffLimit = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffLimit;

                if (!byBuffLimit.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byBuffLimit.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffLimit.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byBuffLimit"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByBuffLimit(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffLimit(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown115(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown115 is null)
        {
            byUnknown115 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown115;

                if (!byUnknown115.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown115.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown115.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown115(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Id2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById2(string? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyById2(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Id2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById2(string? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byId2 is null)
        {
            byId2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Id2;

                if (!byId2.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byId2.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byId2.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byId2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffDefinitionsDat>> GetManyToManyById2(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<string, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.IsRecovery"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsRecovery(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsRecovery(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.IsRecovery"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsRecovery(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byIsRecovery is null)
        {
            byIsRecovery = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsRecovery;

                if (!byIsRecovery.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsRecovery.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsRecovery.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byIsRecovery"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByIsRecovery(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsRecovery(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown125(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown125(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown125"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown125(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown125 is null)
        {
            byUnknown125 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown125;

                if (!byUnknown125.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown125.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown125.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown125"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown125(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown125(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown126(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown126(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown126(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown126 is null)
        {
            byUnknown126 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown126;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown126.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown126.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown126.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown126"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown126(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown126(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown142(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown142 is null)
        {
            byUnknown142 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown142;

                if (!byUnknown142.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown142.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown142.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown142(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown143(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown143(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown143(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown143 is null)
        {
            byUnknown143 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown143;

                if (!byUnknown143.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown143.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown143.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown143"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown143(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown143(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown147"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown147(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown147(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown147"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown147(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown147 is null)
        {
            byUnknown147 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown147;

                if (!byUnknown147.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown147.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown147.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown147"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown147(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown147(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown148(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;

                if (!byUnknown148.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown148(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown149(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown149(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown149(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown149 is null)
        {
            byUnknown149 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown149;

                if (!byUnknown149.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown149.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown149.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown149"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown149(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown149(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown153"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown153(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown153(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown153"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown153(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown153 is null)
        {
            byUnknown153 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown153;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown153.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown153.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown153.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown153"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown153(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown153(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown169(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown169(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown169"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown169(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown169 is null)
        {
            byUnknown169 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown169;

                if (!byUnknown169.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown169.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown169.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown169"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown169(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown169(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown170"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown170(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown170(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown170"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown170(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown170 is null)
        {
            byUnknown170 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown170;

                if (!byUnknown170.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown170.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown170.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown170"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown170(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown170(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown171"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown171(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown171(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown171"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown171(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown171 is null)
        {
            byUnknown171 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown171;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown171.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown171.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown171.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown171"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown171(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown171(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown187"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown187(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown187(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown187"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown187(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown187 is null)
        {
            byUnknown187 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown187;

                if (!byUnknown187.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown187.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown187.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown187"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown187(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown187(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown188"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown188(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown188(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown188"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown188(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown188 is null)
        {
            byUnknown188 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown188;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown188.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown188.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown188.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown188"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown188(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown188(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown204"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown204(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown204(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown204"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown204(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown204 is null)
        {
            byUnknown204 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown204;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown204.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown204.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown204.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown204"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown204(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown204(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown220(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown220(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown220"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown220(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown220 is null)
        {
            byUnknown220 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown220;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown220.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown220.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown220.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown220"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown220(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown220(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown236"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown236(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown236(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown236"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown236(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown236 is null)
        {
            byUnknown236 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown236;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown236.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown236.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown236.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown236"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown236(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown236(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown252"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown252(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown252(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown252"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown252(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown252 is null)
        {
            byUnknown252 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown252;

                if (!byUnknown252.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown252.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown252.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown252"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown252(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown252(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown253"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown253(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown253(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown253"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown253(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown253 is null)
        {
            byUnknown253 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown253;

                if (!byUnknown253.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown253.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown253.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown253"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown253(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown253(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown254"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown254(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown254(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown254"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown254(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown254 is null)
        {
            byUnknown254 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown254;

                if (!byUnknown254.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown254.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown254.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown254"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown254(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown254(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown255"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown255(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown255(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown255"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown255(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown255 is null)
        {
            byUnknown255 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown255;

                if (!byUnknown255.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown255.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown255.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown255"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown255(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown255(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown256"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown256(bool? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown256(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown256"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown256(bool? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown256 is null)
        {
            byUnknown256 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown256;

                if (!byUnknown256.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown256.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown256.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown256"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, BuffDefinitionsDat>> GetManyToManyByUnknown256(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<bool, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown256(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown257"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown257(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown257(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown257"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown257(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown257 is null)
        {
            byUnknown257 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown257;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown257.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown257.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown257.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown257"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown257(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown257(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown273"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown273(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown273(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown273"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown273(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown273 is null)
        {
            byUnknown273 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown273;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown273.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown273.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown273.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown273"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown273(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown273(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown289"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown289(string? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown289(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown289"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown289(string? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown289 is null)
        {
            byUnknown289 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown289;

                if (!byUnknown289.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown289.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown289.TryGetValue(key, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown289"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, BuffDefinitionsDat>> GetManyToManyByUnknown289(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<string, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown289(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown297"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown297(int? key, out BuffDefinitionsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown297(key, out var items))
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
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.Unknown297"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown297(int? key, out IReadOnlyList<BuffDefinitionsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        if (byUnknown297 is null)
        {
            byUnknown297 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown297;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown297.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown297.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown297.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<BuffDefinitionsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="BuffDefinitionsDat"/> with <see cref="BuffDefinitionsDat.byUnknown297"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, BuffDefinitionsDat>> GetManyToManyByUnknown297(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, BuffDefinitionsDat>>();
        }

        var items = new List<ResultItem<int, BuffDefinitionsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown297(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, BuffDefinitionsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private BuffDefinitionsDat[] Load()
    {
        const string filePath = "Data/BuffDefinitions.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BuffDefinitionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Invisible
            (var invisibleLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Removable
            (var removableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading Unknown42
            (var unknown42Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Maximum_StatsKey
            (var maximum_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Current_StatsKey
            (var current_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BuffLimit
            (var bufflimitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Id2
            (var id2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsRecovery
            (var isrecoveryLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown147
            (var unknown147Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown153
            (var tempunknown153Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown153Loading = tempunknown153Loading.AsReadOnly();

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown170
            (var unknown170Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown171
            (var tempunknown171Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown171Loading = tempunknown171Loading.AsReadOnly();

            // loading Unknown187
            (var unknown187Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown188
            (var tempunknown188Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown188Loading = tempunknown188Loading.AsReadOnly();

            // loading Unknown204
            (var tempunknown204Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown204Loading = tempunknown204Loading.AsReadOnly();

            // loading Unknown220
            (var tempunknown220Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown220Loading = tempunknown220Loading.AsReadOnly();

            // loading Unknown236
            (var tempunknown236Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown236Loading = tempunknown236Loading.AsReadOnly();

            // loading Unknown252
            (var unknown252Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown253
            (var unknown253Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown254
            (var unknown254Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown255
            (var unknown255Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown256
            (var unknown256Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown257
            (var unknown257Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown273
            (var tempunknown273Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown273Loading = tempunknown273Loading.AsReadOnly();

            // loading Unknown289
            (var unknown289Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown297
            (var tempunknown297Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown297Loading = tempunknown297Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BuffDefinitionsDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                Invisible = invisibleLoading,
                Removable = removableLoading,
                Name = nameLoading,
                StatsKeys = statskeysLoading,
                Unknown42 = unknown42Loading,
                Unknown43 = unknown43Loading,
                Unknown47 = unknown47Loading,
                Maximum_StatsKey = maximum_statskeyLoading,
                Current_StatsKey = current_statskeyLoading,
                Unknown80 = unknown80Loading,
                Unknown81 = unknown81Loading,
                BuffVisualsKey = buffvisualskeyLoading,
                Unknown101 = unknown101Loading,
                Unknown102 = unknown102Loading,
                Unknown103 = unknown103Loading,
                Unknown107 = unknown107Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Unknown110 = unknown110Loading,
                BuffLimit = bufflimitLoading,
                Unknown115 = unknown115Loading,
                Id2 = id2Loading,
                IsRecovery = isrecoveryLoading,
                Unknown125 = unknown125Loading,
                Unknown126 = unknown126Loading,
                Unknown142 = unknown142Loading,
                Unknown143 = unknown143Loading,
                Unknown147 = unknown147Loading,
                Unknown148 = unknown148Loading,
                Unknown149 = unknown149Loading,
                Unknown153 = unknown153Loading,
                Unknown169 = unknown169Loading,
                Unknown170 = unknown170Loading,
                Unknown171 = unknown171Loading,
                Unknown187 = unknown187Loading,
                Unknown188 = unknown188Loading,
                Unknown204 = unknown204Loading,
                Unknown220 = unknown220Loading,
                Unknown236 = unknown236Loading,
                Unknown252 = unknown252Loading,
                Unknown253 = unknown253Loading,
                Unknown254 = unknown254Loading,
                Unknown255 = unknown255Loading,
                Unknown256 = unknown256Loading,
                Unknown257 = unknown257Loading,
                Unknown273 = unknown273Loading,
                Unknown289 = unknown289Loading,
                Unknown297 = unknown297Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
