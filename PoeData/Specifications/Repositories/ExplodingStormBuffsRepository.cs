using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="ExplodingStormBuffsDat"/> related data and helper methods.
/// </summary>
public sealed class ExplodingStormBuffsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<ExplodingStormBuffsDat> Items { get; }

    private Dictionary<string, List<ExplodingStormBuffsDat>>? byId;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byBuffDefinitionsKey1;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown24;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byStatValues;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown56;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown60;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown76;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown80;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown84;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byFriendly_MonsterVarietiesKey;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byMiscObjectsKey;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byMiscAnimatedKey;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byBuffVisualsKey;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byEnemy_MonsterVarietiesKey;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown168;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown172;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byUnknown176;
    private Dictionary<int, List<ExplodingStormBuffsDat>>? byBuffDefinitionsKey2;
    private Dictionary<bool, List<ExplodingStormBuffsDat>>? byIsOnlySpawningNearPlayer;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExplodingStormBuffsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal ExplodingStormBuffsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetById(string? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Id"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyById(string? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byId"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, ExplodingStormBuffsDat>> GetManyToManyById(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<string, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyById(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.BuffDefinitionsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey1(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDefinitionsKey1(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.BuffDefinitionsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey1(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byBuffDefinitionsKey1 is null)
        {
            byBuffDefinitionsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDefinitionsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffDefinitionsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffDefinitionsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDefinitionsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byBuffDefinitionsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByBuffDefinitionsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByStatValues(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByStatValues(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.StatValues"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByStatValues(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byStatValues is null)
        {
            byStatValues = new();
            foreach (var item in Items)
            {
                var itemKey = item.StatValues;
                foreach (var listKey in itemKey)
                {
                    if (!byStatValues.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byStatValues.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byStatValues.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byStatValues"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByStatValues(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByStatValues(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown60(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown60.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown60.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byUnknown76 is null)
        {
            byUnknown76 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown76;

                if (!byUnknown76.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown76.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown76.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown84(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byUnknown84 is null)
        {
            byUnknown84 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown84;

                if (!byUnknown84.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown84.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown84.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Friendly_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFriendly_MonsterVarietiesKey(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFriendly_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Friendly_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFriendly_MonsterVarietiesKey(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byFriendly_MonsterVarietiesKey is null)
        {
            byFriendly_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Friendly_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byFriendly_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byFriendly_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byFriendly_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byFriendly_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByFriendly_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFriendly_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.MiscObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscObjectsKey(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByMiscObjectsKey(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.MiscObjectsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscObjectsKey(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byMiscObjectsKey is null)
        {
            byMiscObjectsKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscObjectsKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscObjectsKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscObjectsKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscObjectsKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byMiscObjectsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByMiscObjectsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscObjectsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByMiscAnimatedKey(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.MiscAnimatedKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByMiscAnimatedKey(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byMiscAnimatedKey is null)
        {
            byMiscAnimatedKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.MiscAnimatedKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byMiscAnimatedKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byMiscAnimatedKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byMiscAnimatedKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byMiscAnimatedKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByMiscAnimatedKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByMiscAnimatedKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.BuffVisualsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffVisualsKey(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.BuffVisualsKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffVisualsKey(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byBuffVisualsKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByBuffVisualsKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffVisualsKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Enemy_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEnemy_MonsterVarietiesKey(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEnemy_MonsterVarietiesKey(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Enemy_MonsterVarietiesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEnemy_MonsterVarietiesKey(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byEnemy_MonsterVarietiesKey is null)
        {
            byEnemy_MonsterVarietiesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.Enemy_MonsterVarietiesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byEnemy_MonsterVarietiesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byEnemy_MonsterVarietiesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byEnemy_MonsterVarietiesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byEnemy_MonsterVarietiesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByEnemy_MonsterVarietiesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEnemy_MonsterVarietiesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown168(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown168(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown168"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown168(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byUnknown168 is null)
        {
            byUnknown168 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown168;

                if (!byUnknown168.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown168.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown168.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown168"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown168(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown168(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown172(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown172"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown172(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown172"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown172(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown172(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown176(int? key, out ExplodingStormBuffsDat? item)
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown176(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
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
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byUnknown176"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByUnknown176(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown176(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.BuffDefinitionsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByBuffDefinitionsKey2(int? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByBuffDefinitionsKey2(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.BuffDefinitionsKey2"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByBuffDefinitionsKey2(int? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byBuffDefinitionsKey2 is null)
        {
            byBuffDefinitionsKey2 = new();
            foreach (var item in Items)
            {
                var itemKey = item.BuffDefinitionsKey2;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byBuffDefinitionsKey2.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byBuffDefinitionsKey2.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byBuffDefinitionsKey2.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byBuffDefinitionsKey2"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, ExplodingStormBuffsDat>> GetManyToManyByBuffDefinitionsKey2(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<int, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByBuffDefinitionsKey2(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.IsOnlySpawningNearPlayer"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByIsOnlySpawningNearPlayer(bool? key, out ExplodingStormBuffsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByIsOnlySpawningNearPlayer(key, out var items))
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
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.IsOnlySpawningNearPlayer"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByIsOnlySpawningNearPlayer(bool? key, out IReadOnlyList<ExplodingStormBuffsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        if (byIsOnlySpawningNearPlayer is null)
        {
            byIsOnlySpawningNearPlayer = new();
            foreach (var item in Items)
            {
                var itemKey = item.IsOnlySpawningNearPlayer;

                if (!byIsOnlySpawningNearPlayer.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byIsOnlySpawningNearPlayer.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byIsOnlySpawningNearPlayer.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<ExplodingStormBuffsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="ExplodingStormBuffsDat"/> with <see cref="ExplodingStormBuffsDat.byIsOnlySpawningNearPlayer"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, ExplodingStormBuffsDat>> GetManyToManyByIsOnlySpawningNearPlayer(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, ExplodingStormBuffsDat>>();
        }

        var items = new List<ResultItem<bool, ExplodingStormBuffsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByIsOnlySpawningNearPlayer(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, ExplodingStormBuffsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private ExplodingStormBuffsDat[] Load()
    {
        const string filePath = "Data/ExplodingStormBuffs.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExplodingStormBuffsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BuffDefinitionsKey1
            (var buffdefinitionskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var tempunknown60Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown60Loading = tempunknown60Loading.AsReadOnly();

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Friendly_MonsterVarietiesKey
            (var friendly_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscObjectsKey
            (var miscobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MiscAnimatedKey
            (var miscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Enemy_MonsterVarietiesKey
            (var enemy_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey2
            (var buffdefinitionskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IsOnlySpawningNearPlayer
            (var isonlyspawningnearplayerLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExplodingStormBuffsDat()
            {
                Id = idLoading,
                BuffDefinitionsKey1 = buffdefinitionskey1Loading,
                Unknown24 = unknown24Loading,
                StatValues = statvaluesLoading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Friendly_MonsterVarietiesKey = friendly_monstervarietieskeyLoading,
                MiscObjectsKey = miscobjectskeyLoading,
                MiscAnimatedKey = miscanimatedkeyLoading,
                BuffVisualsKey = buffvisualskeyLoading,
                Enemy_MonsterVarietiesKey = enemy_monstervarietieskeyLoading,
                Unknown168 = unknown168Loading,
                Unknown172 = unknown172Loading,
                Unknown176 = unknown176Loading,
                BuffDefinitionsKey2 = buffdefinitionskey2Loading,
                IsOnlySpawningNearPlayer = isonlyspawningnearplayerLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
