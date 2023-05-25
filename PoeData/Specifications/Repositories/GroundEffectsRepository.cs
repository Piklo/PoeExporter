using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="GroundEffectsDat"/> related data and helper methods.
/// </summary>
public sealed class GroundEffectsRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<GroundEffectsDat> Items { get; }

    private Dictionary<int, List<GroundEffectsDat>>? byGroundEffectTypesKey;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown16;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown20;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown24;
    private Dictionary<float, List<GroundEffectsDat>>? byUnknown40;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown44;
    private Dictionary<bool, List<GroundEffectsDat>>? byUnknown60;
    private Dictionary<string, List<GroundEffectsDat>>? byAOFile;
    private Dictionary<string, List<GroundEffectsDat>>? byUnknown77;
    private Dictionary<string, List<GroundEffectsDat>>? byEndEffect;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown101;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown117;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown133;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown149;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown165;
    private Dictionary<bool, List<GroundEffectsDat>>? byUnknown181;
    private Dictionary<bool, List<GroundEffectsDat>>? byUnknown182;
    private Dictionary<bool, List<GroundEffectsDat>>? byUnknown183;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown184;
    private Dictionary<int, List<GroundEffectsDat>>? byUnknown200;
    private Dictionary<bool, List<GroundEffectsDat>>? byUnknown216;
    private Dictionary<bool, List<GroundEffectsDat>>? byUnknown217;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroundEffectsRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal GroundEffectsRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.GroundEffectTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByGroundEffectTypesKey(int? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByGroundEffectTypesKey(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.GroundEffectTypesKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByGroundEffectTypesKey(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byGroundEffectTypesKey is null)
        {
            byGroundEffectTypesKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.GroundEffectTypesKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byGroundEffectTypesKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byGroundEffectTypesKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byGroundEffectTypesKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byGroundEffectTypesKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByGroundEffectTypesKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByGroundEffectTypesKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown20(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown20"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown20(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown20"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown20(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown20(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown24(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown24"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown24(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown24"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown24(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown24(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown40(float? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown40"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown40(float? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown40"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, GroundEffectsDat>> GetManyToManyByUnknown40(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<float, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown40(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown44(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown44"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown44(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown44 is null)
        {
            byUnknown44 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown44;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown44.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown44.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown44.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown44"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown44(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown44(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown60(bool? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown60"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown60(bool? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown60"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GroundEffectsDat>> GetManyToManyByUnknown60(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown60(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByAOFile(string? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByAOFile(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.AOFile"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByAOFile(string? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byAOFile is null)
        {
            byAOFile = new();
            foreach (var item in Items)
            {
                var itemKey = item.AOFile;
                foreach (var listKey in itemKey)
                {
                    if (!byAOFile.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byAOFile.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byAOFile.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byAOFile"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GroundEffectsDat>> GetManyToManyByAOFile(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<string, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByAOFile(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown77"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown77(string? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown77(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown77"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown77(string? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown77 is null)
        {
            byUnknown77 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown77;
                foreach (var listKey in itemKey)
                {
                    if (!byUnknown77.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byUnknown77.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byUnknown77.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown77"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GroundEffectsDat>> GetManyToManyByUnknown77(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<string, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown77(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.EndEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByEndEffect(string? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByEndEffect(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.EndEffect"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByEndEffect(string? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byEndEffect is null)
        {
            byEndEffect = new();
            foreach (var item in Items)
            {
                var itemKey = item.EndEffect;

                if (!byEndEffect.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byEndEffect.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byEndEffect.TryGetValue(key, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byEndEffect"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<string, GroundEffectsDat>> GetManyToManyByEndEffect(IReadOnlyList<string>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<string, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<string, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByEndEffect(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<string, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown101(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown101"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown101(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown101 is null)
        {
            byUnknown101 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown101;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown101.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown101.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown101.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown101"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown101(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown101(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown117(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown117"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown117(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown117 is null)
        {
            byUnknown117 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown117;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown117.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown117.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown117.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown117"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown117(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown117(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown133"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown133(int? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown133(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown133"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown133(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown133 is null)
        {
            byUnknown133 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown133;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown133.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown133.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown133.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown133"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown133(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown133(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown149(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown149"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown149(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown149 is null)
        {
            byUnknown149 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown149;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown149.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown149.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown149.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown149"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown149(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown149(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown165(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown165"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown165(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown165"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown165(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown165(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown181(bool? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown181"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown181(bool? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown181"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GroundEffectsDat>> GetManyToManyByUnknown181(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown181(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown182"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown182(bool? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown182"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown182(bool? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown182 is null)
        {
            byUnknown182 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown182;

                if (!byUnknown182.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown182.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown182.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown182"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GroundEffectsDat>> GetManyToManyByUnknown182(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown182(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown183"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown183(bool? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown183(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown183"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown183(bool? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown183 is null)
        {
            byUnknown183 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown183;

                if (!byUnknown183.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byUnknown183.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown183.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown183"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GroundEffectsDat>> GetManyToManyByUnknown183(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown183(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown184(int? key, out GroundEffectsDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByUnknown184(key, out var items))
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown184"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown184(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        if (byUnknown184 is null)
        {
            byUnknown184 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Unknown184;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byUnknown184.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byUnknown184.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byUnknown184.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown184"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown184(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown184(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown200"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown200(int? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown200"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown200(int? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown200"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, GroundEffectsDat>> GetManyToManyByUnknown200(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<int, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown200(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown216(bool? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown216"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown216(bool? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown216"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GroundEffectsDat>> GetManyToManyByUnknown216(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown216(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown217"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown217(bool? key, out GroundEffectsDat? item)
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
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.Unknown217"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown217(bool? key, out IReadOnlyList<GroundEffectsDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<GroundEffectsDat>();
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
            items = Array.Empty<GroundEffectsDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="GroundEffectsDat"/> with <see cref="GroundEffectsDat.byUnknown217"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<bool, GroundEffectsDat>> GetManyToManyByUnknown217(IReadOnlyList<bool>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<bool, GroundEffectsDat>>();
        }

        var items = new List<ResultItem<bool, GroundEffectsDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown217(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<bool, GroundEffectsDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private GroundEffectsDat[] Load()
    {
        const string filePath = "Data/GroundEffects.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GroundEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading GroundEffectTypesKey
            (var groundeffecttypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AOFile
            (var tempaofileLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofileLoading = tempaofileLoading.AsReadOnly();

            // loading Unknown77
            (var tempunknown77Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown77Loading = tempunknown77Loading.AsReadOnly();

            // loading EndEffect
            (var endeffectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown101
            (var unknown101Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown133
            (var unknown133Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown181
            (var unknown181Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown182
            (var unknown182Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown183
            (var unknown183Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown184
            (var unknown184Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown200
            (var unknown200Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown217
            (var unknown217Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GroundEffectsDat()
            {
                GroundEffectTypesKey = groundeffecttypeskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                AOFile = aofileLoading,
                Unknown77 = unknown77Loading,
                EndEffect = endeffectLoading,
                Unknown101 = unknown101Loading,
                Unknown117 = unknown117Loading,
                Unknown133 = unknown133Loading,
                Unknown149 = unknown149Loading,
                Unknown165 = unknown165Loading,
                Unknown181 = unknown181Loading,
                Unknown182 = unknown182Loading,
                Unknown183 = unknown183Loading,
                Unknown184 = unknown184Loading,
                Unknown200 = unknown200Loading,
                Unknown216 = unknown216Loading,
                Unknown217 = unknown217Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
