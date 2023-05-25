using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GeometryProjectilesDat"/> related data and helper methods.
/// </summary>
public sealed class GeometryProjectilesRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GeometryProjectilesDat> Items { get; }

    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown0;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown4;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown20;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown21;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown25;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown26;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown30;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown34;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown35;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown39;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown43;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown47;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown48;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown52;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown56;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown60;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown61;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown62;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown63;
    private Dictionary<bool, List<GeometryProjectilesDat>>? byUnknown79;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown80;
    private Dictionary<int, List<GeometryProjectilesDat>>? byUnknown96;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeometryProjectilesRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GeometryProjectilesRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown4 is null)
        {
            byUnknown4 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown4;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown4.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown4.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown4.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(bool? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown20(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown21"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown21(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown21(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown21"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown21(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown21 is null)
        {
            byUnknown21 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown21;

                if (!byUnknown21.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown21.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown21.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown21"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown21(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown21(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown25(bool? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown25"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown25(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown25"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown25(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown25(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown26(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown26(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown26"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown26(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown26 is null)
        {
            byUnknown26 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown26;

                if (!byUnknown26.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown26.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown26.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown26"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown26(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown26(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown30"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown30(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown30(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown30"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown30(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown30 is null)
        {
            byUnknown30 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown30;

                if (!byUnknown30.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown30.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown30.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown30"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown30(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown30(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown34"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown34(bool? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown34(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown34"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown34(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown34 is null)
        {
            byUnknown34 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown34;

                if (!byUnknown34.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown34.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown34.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown34"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown34(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown34(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown35"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown35(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown35(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown35"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown35(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown35 is null)
        {
            byUnknown35 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown35;

                if (!byUnknown35.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown35.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown35.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown35"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown35(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown35(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown39"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown39(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown39(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown39"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown39(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown39 is null)
        {
            byUnknown39 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown39;

                if (!byUnknown39.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown39.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown39.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown39"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown39(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown39(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown43"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown43(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown43"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown43(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown43"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown43(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown43(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown47(bool? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown47"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown47(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown47"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown47(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown47(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(bool? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown60 is null)
        {
            byUnknown60 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown60;

                if (!byUnknown60.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown60.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown60.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown60(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown61(bool? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown61"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown61(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
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
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown61"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown61(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown61(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown62(bool? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown62(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown62"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown62(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown62 is null)
        {
            byUnknown62 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown62;

                if (!byUnknown62.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown62.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown62.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown62"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown62(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown62(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown63(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown63(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown63"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown63(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown63 is null)
        {
            byUnknown63 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown63;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown63.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown63.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown63.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown63"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown63(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown63(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown79(bool? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown79(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown79"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown79(bool? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown79 is null)
        {
            byUnknown79 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown79;

                if (!byUnknown79.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown79.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown79.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown79"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryProjectilesDat>> GetManyToManyByUnknown79(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<bool, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown79(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out GeometryProjectilesDat? item)
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown80 is null)
        {
            byUnknown80 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown80;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown80.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown80.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown80.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out GeometryProjectilesDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown96(key, out var items))
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
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<GeometryProjectilesDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;

                if (!byUnknown96.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryProjectilesDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryProjectilesDat"/> with <see cref="GeometryProjectilesDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryProjectilesDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryProjectilesDat>>();
        }

        var items = new List<ResultItem<int, GeometryProjectilesDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryProjectilesDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GeometryProjectilesDat[] Load()
    {
        const string filePath = "Data/GeometryProjectiles.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GeometryProjectilesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown30
            (var unknown30Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown35
            (var unknown35Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown39
            (var unknown39Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown63
            (var unknown63Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GeometryProjectilesDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown21 = unknown21Loading,
                Unknown25 = unknown25Loading,
                Unknown26 = unknown26Loading,
                Unknown30 = unknown30Loading,
                Unknown34 = unknown34Loading,
                Unknown35 = unknown35Loading,
                Unknown39 = unknown39Loading,
                Unknown43 = unknown43Loading,
                Unknown47 = unknown47Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown62 = unknown62Loading,
                Unknown63 = unknown63Loading,
                Unknown79 = unknown79Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
