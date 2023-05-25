using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GeometryAttackDat"/> related data and helper methods.
/// </summary>
public sealed class GeometryAttackRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GeometryAttackDat> Items { get; }

    private Dictionary<int, List<GeometryAttackDat>>? byUnknown0;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown4;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown20;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown36;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown40;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown44;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown48;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown52;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown56;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown57;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown61;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown65;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown69;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown70;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown71;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown87;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown91;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown95;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown99;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown103;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown107;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown111;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown112;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown113;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown117;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown121;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown122;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown126;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown127;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown143;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown159;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown163;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown164;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown165;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown181;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown182;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown198;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown199;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown200;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown216;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown217;
    private Dictionary<int, List<GeometryAttackDat>>? byUnknown218;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown234;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown235;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown236;
    private Dictionary<bool, List<GeometryAttackDat>>? byUnknown237;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeometryAttackRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GeometryAttackRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown4.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown4.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown20.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown20.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(bool? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown56(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown57(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown57(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown57"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown57(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown57 is null)
        {
            byUnknown57 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown57;

                if (!byUnknown57.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown57.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown57.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown57"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown57(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown57(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown61(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown61 is null)
        {
            byUnknown61 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown61;

                if (!byUnknown61.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown61.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown61.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown61(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown65(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown65(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown65"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown65(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown65 is null)
        {
            byUnknown65 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown65;

                if (!byUnknown65.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown65.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown65.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown65"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown65(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown65(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown69(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown69(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown69"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown69(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown69 is null)
        {
            byUnknown69 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown69;

                if (!byUnknown69.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown69.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown69.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown69"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown69(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown69(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown70(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown70(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown70"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown70(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown70 is null)
        {
            byUnknown70 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown70;

                if (!byUnknown70.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown70.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown70.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown70"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown70(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown70(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown71(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown71(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown71"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown71(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown71 is null)
        {
            byUnknown71 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown71;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown71.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown71.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown71.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown71"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown71(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown71(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown87(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown87"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown87(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown87"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown87(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown87(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown91(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown91(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown91"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown91(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown91 is null)
        {
            byUnknown91 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown91;

                if (!byUnknown91.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown91.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown91.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown91"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown91(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown91(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown95(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown95(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown95"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown95(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown95 is null)
        {
            byUnknown95 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown95;

                if (!byUnknown95.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown95.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown95.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown95"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown95(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown95(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown99(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown99(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown99"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown99(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown99 is null)
        {
            byUnknown99 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown99;

                if (!byUnknown99.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown99.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown99.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown99"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown99(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown99(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown103(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown103"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown103(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown103"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown103(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown103(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown107(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown107"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown107(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown107"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown107(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown107(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown111(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown111(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown111(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown111 is null)
        {
            byUnknown111 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown111;

                if (!byUnknown111.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown111.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown111.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown111"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown111(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown111(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(bool? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown112(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown113(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown113(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown113"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown113(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown113 is null)
        {
            byUnknown113 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown113;

                if (!byUnknown113.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown113.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown113.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown113"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown113(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown113(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown117(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown117(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown117(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown117 is null)
        {
            byUnknown117 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown117;

                if (!byUnknown117.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown117.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown117.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown117"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown117(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown117(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown121"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown121(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown121(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown121"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown121(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown121 is null)
        {
            byUnknown121 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown121;

                if (!byUnknown121.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown121.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown121.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown121"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown121(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown121(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown122(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown122(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown122"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown122(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown122 is null)
        {
            byUnknown122 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown122;

                if (!byUnknown122.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown122.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown122.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown122"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown122(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown122(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown126(bool? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown126"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown126(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown126 is null)
        {
            byUnknown126 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown126;

                if (!byUnknown126.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown126.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown126.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown126"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown126(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown126(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown127"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown127(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown127"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown127(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown127 is null)
        {
            byUnknown127 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown127;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown127.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown127.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown127.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown127"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown127(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown127(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown143(int? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown143"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown143(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown143 is null)
        {
            byUnknown143 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown143;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown143.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown143.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown143.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown143"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown143(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown143(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown159"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown159(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown159(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown159"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown159(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown159 is null)
        {
            byUnknown159 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown159;

                if (!byUnknown159.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown159.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown159.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown159"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown159(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown159(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown163"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown163(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown163(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown163"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown163(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown163 is null)
        {
            byUnknown163 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown163;

                if (!byUnknown163.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown163.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown163.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown163"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown163(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown163(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown164(bool? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown164(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
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
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown164"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown164(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown164(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown165(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown165(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown165(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown165 is null)
        {
            byUnknown165 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown165;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown165.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown165.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown165.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown165"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown165(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown165(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown181(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown181(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown181(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown181 is null)
        {
            byUnknown181 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown181;

                if (!byUnknown181.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown181.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown181.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown181"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown181(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown181(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown182"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown182(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown182(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown182"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown182(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown182 is null)
        {
            byUnknown182 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown182;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown182.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown182.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown182.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown182"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown182(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown182(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown198"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown198(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown198(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown198"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown198(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown198 is null)
        {
            byUnknown198 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown198;

                if (!byUnknown198.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown198.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown198.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown198"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown198(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown198(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown199"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown199(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown199(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown199"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown199(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown199 is null)
        {
            byUnknown199 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown199;

                if (!byUnknown199.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown199.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown199.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown199"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown199(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown199(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown200"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown200(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown200(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown200"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown200(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown200 is null)
        {
            byUnknown200 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown200;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown200.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown200.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown200.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown200"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown200(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown200(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown216(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown216(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown216(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown216 is null)
        {
            byUnknown216 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown216;

                if (!byUnknown216.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown216.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown216.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown216"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown216(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown216(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown217"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown217(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown217(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown217"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown217(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown217 is null)
        {
            byUnknown217 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown217;

                if (!byUnknown217.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown217.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown217.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown217"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown217(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown217(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown218"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown218(int? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown218(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown218"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown218(int? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown218 is null)
        {
            byUnknown218 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown218;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown218.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown218.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown218.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown218"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryAttackDat>> GetManyToManyByUnknown218(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<int, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown218(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown234"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown234(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown234(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown234"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown234(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown234 is null)
        {
            byUnknown234 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown234;

                if (!byUnknown234.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown234.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown234.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown234"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown234(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown234(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown235"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown235(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown235(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown235"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown235(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown235 is null)
        {
            byUnknown235 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown235;

                if (!byUnknown235.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown235.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown235.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown235"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown235(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown235(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown236"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown236(bool? key, out GeometryAttackDat? item)
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown236"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown236(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown236 is null)
        {
            byUnknown236 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown236;

                if (!byUnknown236.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown236.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown236.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown236"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown236(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown236(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown237"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown237(bool? key, out GeometryAttackDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown237(key, out var items))
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
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.Unknown237"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown237(bool? key, out IReadOnlyList<GeometryAttackDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        if (byUnknown237 is null)
        {
            byUnknown237 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown237;

                if (!byUnknown237.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown237.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown237.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryAttackDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryAttackDat"/> with <see cref="GeometryAttackDat.byUnknown237"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryAttackDat>> GetManyToManyByUnknown237(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryAttackDat>>();
        }

        var items = new List<ResultItem<bool, GeometryAttackDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown237(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryAttackDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GeometryAttackDat[] Load()
    {
        const string filePath = "Data/GeometryAttack.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GeometryAttackDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var tempunknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown4Loading = tempunknown4Loading.AsReadOnly();

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

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
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown71
            (var tempunknown71Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown71Loading = tempunknown71Loading.AsReadOnly();

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown121
            (var unknown121Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown127
            (var unknown127Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown143
            (var tempunknown143Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown143Loading = tempunknown143Loading.AsReadOnly();

            // loading Unknown159
            (var unknown159Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown163
            (var unknown163Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown182
            (var tempunknown182Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown182Loading = tempunknown182Loading.AsReadOnly();

            // loading Unknown198
            (var unknown198Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown199
            (var unknown199Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown200
            (var unknown200Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown217
            (var unknown217Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown218
            (var tempunknown218Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown218Loading = tempunknown218Loading.AsReadOnly();

            // loading Unknown234
            (var unknown234Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown235
            (var unknown235Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown236
            (var unknown236Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown237
            (var unknown237Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GeometryAttackDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown57 = unknown57Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown71 = unknown71Loading,
                Unknown87 = unknown87Loading,
                Unknown91 = unknown91Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown103 = unknown103Loading,
                Unknown107 = unknown107Loading,
                Unknown111 = unknown111Loading,
                Unknown112 = unknown112Loading,
                Unknown113 = unknown113Loading,
                Unknown117 = unknown117Loading,
                Unknown121 = unknown121Loading,
                Unknown122 = unknown122Loading,
                Unknown126 = unknown126Loading,
                Unknown127 = unknown127Loading,
                Unknown143 = unknown143Loading,
                Unknown159 = unknown159Loading,
                Unknown163 = unknown163Loading,
                Unknown164 = unknown164Loading,
                Unknown165 = unknown165Loading,
                Unknown181 = unknown181Loading,
                Unknown182 = unknown182Loading,
                Unknown198 = unknown198Loading,
                Unknown199 = unknown199Loading,
                Unknown200 = unknown200Loading,
                Unknown216 = unknown216Loading,
                Unknown217 = unknown217Loading,
                Unknown218 = unknown218Loading,
                Unknown234 = unknown234Loading,
                Unknown235 = unknown235Loading,
                Unknown236 = unknown236Loading,
                Unknown237 = unknown237Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
