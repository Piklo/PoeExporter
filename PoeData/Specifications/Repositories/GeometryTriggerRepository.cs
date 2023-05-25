using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GeometryTriggerDat"/> related data and helper methods.
/// </summary>
public sealed class GeometryTriggerRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GeometryTriggerDat> Items { get; }

    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown0;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown4;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown20;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown36;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown40;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown44;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown48;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown52;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown56;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown60;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown64;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown68;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown72;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown76;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown80;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown84;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown88;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown92;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown96;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown97;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown101;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown102;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown106;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown110;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown111;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown115;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown131;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown135;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown136;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown140;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown141;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown142;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown146;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown147;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown148;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown164;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown165;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown166;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown167;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown171;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown175;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown191;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown207;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown211;
    private Dictionary<int, List<GeometryTriggerDat>>? byUnknown215;
    private Dictionary<bool, List<GeometryTriggerDat>>? byUnknown219;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeometryTriggerRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GeometryTriggerRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown4(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown4"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown4(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown4"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown4(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown4(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown20 is null)
        {
            byUnknown20 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown20;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown20.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown20.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown20.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown36(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown36"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown36(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown36"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown36(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown36(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown40(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown52(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown52"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown52(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown52"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown52(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown52(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown56(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown56"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown56(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown56"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown56(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown56(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown60(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown64(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;

                if (!byUnknown64.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown68(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown68(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown68"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown68(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown68 is null)
        {
            byUnknown68 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown68;

                if (!byUnknown68.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown68.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown68.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown68"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown68(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown68(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown72(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown72(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown72"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown72(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown72 is null)
        {
            byUnknown72 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown72;

                if (!byUnknown72.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown72.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown72.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown72"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown72(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown72(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown76(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown76"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown76(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown76"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown76(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown76(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown84(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown84"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown84(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown84"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown84(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown84(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown88(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown88"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown88(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown88"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown88(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown88(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown92(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown92"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown92(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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

        if (!byUnknown92.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown92"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown92(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown92(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown96(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown97(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown97(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown97"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown97(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown97 is null)
        {
            byUnknown97 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown97;

                if (!byUnknown97.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown97.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown97.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown97"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown97(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown97(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown101(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown101(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown101"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown101(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown101(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown102(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown102"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown102(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown102"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown102(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown102(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown106(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown106(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown106"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown106(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown106 is null)
        {
            byUnknown106 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown106;

                if (!byUnknown106.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown106.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown106.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown106"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown106(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown106(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown110(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown110"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown110(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown110"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown110(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown110(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown111(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown111"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown111(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown111"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown111(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown111(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown115(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown115"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown115(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown115 is null)
        {
            byUnknown115 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown115;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown115.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown115.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown115.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown115"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown115(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown115(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown131"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown131(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown131(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown131"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown131(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown131 is null)
        {
            byUnknown131 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown131;

                if (!byUnknown131.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown131.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown131.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown131"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown131(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown131(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown135"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown135(bool? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown135(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown135"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown135(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown135 is null)
        {
            byUnknown135 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown135;

                if (!byUnknown135.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown135.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown135.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown135"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown135(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown135(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown136(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown136(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown136"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown136(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown136 is null)
        {
            byUnknown136 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown136;

                if (!byUnknown136.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown136.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown136.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown136"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown136(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown136(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown140(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown140"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown140(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown140"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown140(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown140(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown141(bool? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown141(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown141"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown141(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown141 is null)
        {
            byUnknown141 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown141;

                if (!byUnknown141.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown141.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown141.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown141"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown141(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown141(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown142(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown142"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown142(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown142"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown142(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown142(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown146(bool? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown146(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown146"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown146(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown146 is null)
        {
            byUnknown146 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown146;

                if (!byUnknown146.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown146.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown146.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown146"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown146(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown146(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown147"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown147(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown147"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown147(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown147"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown147(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown147(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown148(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown148"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown148(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown148 is null)
        {
            byUnknown148 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown148;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown148.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown148.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown148.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown148"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown148(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown148(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown164(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown164"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown164(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
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
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown164"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown164(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown164(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown165(bool? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown165(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown165 is null)
        {
            byUnknown165 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown165;

                if (!byUnknown165.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown165.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown165.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown165"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown165(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown165(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown166"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown166(bool? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown166(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown166"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown166(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown166 is null)
        {
            byUnknown166 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown166;

                if (!byUnknown166.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown166.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown166.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown166"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown166(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown166(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown167"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown167(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown167(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown167"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown167(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown167 is null)
        {
            byUnknown167 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown167;

                if (!byUnknown167.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown167.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown167.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown167"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown167(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown167(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown171"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown171(int? key, out GeometryTriggerDat? item)
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown171"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown171(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown171 is null)
        {
            byUnknown171 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown171;

                if (!byUnknown171.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown171.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown171.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown171"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown171(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown171(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown175"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown175(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown175(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown175"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown175(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown175 is null)
        {
            byUnknown175 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown175;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown175.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown175.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown175.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown175"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown175(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown175(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown191"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown191(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown191(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown191"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown191(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown191 is null)
        {
            byUnknown191 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown191;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown191.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown191.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown191.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown191"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown191(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown191(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown207"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown207(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown207(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown207"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown207(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown207 is null)
        {
            byUnknown207 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown207;

                if (!byUnknown207.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown207.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown207.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown207"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown207(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown207(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown211"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown211(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown211(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown211"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown211(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown211 is null)
        {
            byUnknown211 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown211;

                if (!byUnknown211.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown211.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown211.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown211"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown211(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown211(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown215"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown215(int? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown215(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown215"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown215(int? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown215 is null)
        {
            byUnknown215 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown215;

                if (!byUnknown215.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown215.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown215.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown215"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GeometryTriggerDat>> GetManyToManyByUnknown215(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<int, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown215(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown219"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown219(bool? key, out GeometryTriggerDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown219(key, out var items))
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
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.Unknown219"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown219(bool? key, out IReadOnlyList<GeometryTriggerDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        if (byUnknown219 is null)
        {
            byUnknown219 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown219;

                if (!byUnknown219.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown219.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown219.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GeometryTriggerDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GeometryTriggerDat"/> with <see cref="GeometryTriggerDat.byUnknown219"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GeometryTriggerDat>> GetManyToManyByUnknown219(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GeometryTriggerDat>>();
        }

        var items = new List<ResultItem<bool, GeometryTriggerDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown219(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GeometryTriggerDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GeometryTriggerDat[] Load()
    {
        const string filePath = "Data/GeometryTrigger.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GeometryTriggerDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

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
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var tempunknown115Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown115Loading = tempunknown115Loading.AsReadOnly();

            // loading Unknown131
            (var unknown131Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown135
            (var unknown135Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown147
            (var unknown147Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown166
            (var unknown166Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown167
            (var unknown167Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown171
            (var unknown171Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown175
            (var tempunknown175Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown175Loading = tempunknown175Loading.AsReadOnly();

            // loading Unknown191
            (var tempunknown191Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown191Loading = tempunknown191Loading.AsReadOnly();

            // loading Unknown207
            (var unknown207Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown211
            (var unknown211Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown215
            (var unknown215Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown219
            (var unknown219Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GeometryTriggerDat()
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
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
                Unknown84 = unknown84Loading,
                Unknown88 = unknown88Loading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown97 = unknown97Loading,
                Unknown101 = unknown101Loading,
                Unknown102 = unknown102Loading,
                Unknown106 = unknown106Loading,
                Unknown110 = unknown110Loading,
                Unknown111 = unknown111Loading,
                Unknown115 = unknown115Loading,
                Unknown131 = unknown131Loading,
                Unknown135 = unknown135Loading,
                Unknown136 = unknown136Loading,
                Unknown140 = unknown140Loading,
                Unknown141 = unknown141Loading,
                Unknown142 = unknown142Loading,
                Unknown146 = unknown146Loading,
                Unknown147 = unknown147Loading,
                Unknown148 = unknown148Loading,
                Unknown164 = unknown164Loading,
                Unknown165 = unknown165Loading,
                Unknown166 = unknown166Loading,
                Unknown167 = unknown167Loading,
                Unknown171 = unknown171Loading,
                Unknown175 = unknown175Loading,
                Unknown191 = unknown191Loading,
                Unknown207 = unknown207Loading,
                Unknown211 = unknown211Loading,
                Unknown215 = unknown215Loading,
                Unknown219 = unknown219Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
