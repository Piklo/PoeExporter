using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="AreaTransitionInfoDat"/> related data and helper methods.
/// </summary>
public sealed class AreaTransitionInfoRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<AreaTransitionInfoDat> Items { get; }

    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown0;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown16;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown32;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown48;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown64;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown80;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown96;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown112;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown128;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown144;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown160;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown176;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown192;
    private Dictionary<int, List<AreaTransitionInfoDat>>? byUnknown196;

    /// <summary>
    /// Initializes a new instance of the <see cref="AreaTransitionInfoRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal AreaTransitionInfoRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown0(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown0(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown0 is null)
        {
            byUnknown0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown16 is null)
        {
            byUnknown16 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown16;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown16.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown16.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown16.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown32(int? key, out AreaTransitionInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown32(key, out var items))
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown32"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown32(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown32 is null)
        {
            byUnknown32 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown32;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown32.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown32.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown32.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown32"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown32(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown32(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown48(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown48"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown48(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown48 is null)
        {
            byUnknown48 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown48;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown48.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown48.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown48.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown48"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown48(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown48(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown64(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown64"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown64(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown64 is null)
        {
            byUnknown64 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown64;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown64.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown64.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown64.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown64"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown64(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown64(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown80(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown80"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown80(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
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
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown80"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown80(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown80(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown96(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown96"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown96(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown96 is null)
        {
            byUnknown96 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown96;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown96.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown96.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown96.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown96"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown96(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown96(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown112(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown112"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown112(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown112 is null)
        {
            byUnknown112 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown112;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown112.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown112.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown112.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown112"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown112(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown112(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown128(int? key, out AreaTransitionInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown128(key, out var items))
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown128"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown128(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown128 is null)
        {
            byUnknown128 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown128;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown128.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown128.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown128.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown128"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown128(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown128(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown144(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown144"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown144(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown144 is null)
        {
            byUnknown144 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown144;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown144.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown144.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown144.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown144"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown144(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown144(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown160(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown160"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown160(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown160 is null)
        {
            byUnknown160 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown160;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown160.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown160.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown160.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown160"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown160(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown160(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown176(int? key, out AreaTransitionInfoDat? item)
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown176"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown176(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown176 is null)
        {
            byUnknown176 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown176;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown176.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown176.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown176.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown176"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown176(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown176(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown192"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown192(int? key, out AreaTransitionInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown192(key, out var items))
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown192"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown192(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown192 is null)
        {
            byUnknown192 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown192;

                if (!byUnknown192.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown192.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown192.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown192"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown192(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown192(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown196"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown196(int? key, out AreaTransitionInfoDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown196(key, out var items))
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
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.Unknown196"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown196(int? key, out IReadOnlyList<AreaTransitionInfoDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        if (byUnknown196 is null)
        {
            byUnknown196 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown196;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown196.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown196.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown196.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<AreaTransitionInfoDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="AreaTransitionInfoDat"/> with <see cref="AreaTransitionInfoDat.byUnknown196"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, AreaTransitionInfoDat>> GetManyToManyByUnknown196(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, AreaTransitionInfoDat>>();
        }

        var items = new List<ResultItem<int, AreaTransitionInfoDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown196(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, AreaTransitionInfoDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private AreaTransitionInfoDat[] Load()
    {
        const string filePath = "Data/AreaTransitionInfo.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AreaTransitionInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown160
            (var unknown160Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown176
            (var tempunknown176Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown176Loading = tempunknown176Loading.AsReadOnly();

            // loading Unknown192
            (var unknown192Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown196
            (var tempunknown196Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown196Loading = tempunknown196Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AreaTransitionInfoDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown128 = unknown128Loading,
                Unknown144 = unknown144Loading,
                Unknown160 = unknown160Loading,
                Unknown176 = unknown176Loading,
                Unknown192 = unknown192Loading,
                Unknown196 = unknown196Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
