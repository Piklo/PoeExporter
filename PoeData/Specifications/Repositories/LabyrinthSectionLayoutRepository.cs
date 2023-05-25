using PoeData.Extensions;
using PoeData.Specifications.DatFiles;
using System.Collections.ObjectModel;
using Serilog;

namespace PoeData.Specifications.Repositories;

/// <summary>
/// Class containing <see cref="LabyrinthSectionLayoutDat"/> related data and helper methods.
/// </summary>
public sealed class LabyrinthSectionLayoutRepository
{
    private readonly ILogger logger;
    private readonly Specification specification;

    /// <summary>Gets items.</summary>
    public ReadOnlyCollection<LabyrinthSectionLayoutDat> Items { get; }

    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byLabyrinthSectionKey;
    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byUnknown16;
    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byLabyrinthSectionLayoutKeys;
    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byLabyrinthSecretsKey0;
    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byLabyrinthSecretsKey1;
    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byLabyrinthAreasKey;
    private Dictionary<float, List<LabyrinthSectionLayoutDat>>? byFloat0;
    private Dictionary<float, List<LabyrinthSectionLayoutDat>>? byFloat1;
    private Dictionary<int, List<LabyrinthSectionLayoutDat>>? byLabyrinthNodeOverridesKeys;

    /// <summary>
    /// Initializes a new instance of the <see cref="LabyrinthSectionLayoutRepository"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    /// <param name="specification">specification.</param>
    internal LabyrinthSectionLayoutRepository(ILogger logger, Specification specification)
    {
        this.logger = logger;
        this.specification = specification;
        Items = Load().AsReadOnly();
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSectionKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSectionKey(int? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSectionKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSectionKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSectionKey(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byLabyrinthSectionKey is null)
        {
            byLabyrinthSectionKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSectionKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLabyrinthSectionKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLabyrinthSectionKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthSectionKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byLabyrinthSectionKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByLabyrinthSectionKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSectionKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByUnknown16(int? key, out LabyrinthSectionLayoutDat? item)
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.Unknown16"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByUnknown16(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
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
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byUnknown16"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByUnknown16(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByUnknown16(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSectionLayoutKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSectionLayoutKeys(int? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSectionLayoutKeys(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSectionLayoutKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSectionLayoutKeys(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byLabyrinthSectionLayoutKeys is null)
        {
            byLabyrinthSectionLayoutKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSectionLayoutKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthSectionLayoutKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthSectionLayoutKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthSectionLayoutKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byLabyrinthSectionLayoutKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByLabyrinthSectionLayoutKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSectionLayoutKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSecretsKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretsKey0(int? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretsKey0(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSecretsKey0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretsKey0(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byLabyrinthSecretsKey0 is null)
        {
            byLabyrinthSecretsKey0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretsKey0;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLabyrinthSecretsKey0.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLabyrinthSecretsKey0.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthSecretsKey0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byLabyrinthSecretsKey0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByLabyrinthSecretsKey0(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretsKey0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSecretsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthSecretsKey1(int? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthSecretsKey1(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthSecretsKey1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthSecretsKey1(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byLabyrinthSecretsKey1 is null)
        {
            byLabyrinthSecretsKey1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthSecretsKey1;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLabyrinthSecretsKey1.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLabyrinthSecretsKey1.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthSecretsKey1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byLabyrinthSecretsKey1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByLabyrinthSecretsKey1(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthSecretsKey1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthAreasKey(int? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthAreasKey(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthAreasKey"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthAreasKey(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byLabyrinthAreasKey is null)
        {
            byLabyrinthAreasKey = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthAreasKey;
                if (itemKey is null)
                {
                    continue;
                }

                if (!byLabyrinthAreasKey.TryGetValue(itemKey.Value, out var list))
                {
                    list = new();
                    byLabyrinthAreasKey.TryAdd(itemKey.Value, list);
                }

                list.Add(item);
            }
        }

        if (!byLabyrinthAreasKey.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byLabyrinthAreasKey"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByLabyrinthAreasKey(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthAreasKey(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.Float0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFloat0(float? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFloat0(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.Float0"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFloat0(float? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byFloat0 is null)
        {
            byFloat0 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Float0;

                if (!byFloat0.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFloat0.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFloat0.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byFloat0"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LabyrinthSectionLayoutDat>> GetManyToManyByFloat0(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<float, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFloat0(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.Float1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByFloat1(float? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByFloat1(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.Float1"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByFloat1(float? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byFloat1 is null)
        {
            byFloat1 = new();
            foreach (var item in Items)
            {
                var itemKey = item.Float1;

                if (!byFloat1.TryGetValue(itemKey, out var list))
                {
                    list = new();
                    byFloat1.TryAdd(itemKey, list);
                }

                list.Add(item);
            }
        }

        if (!byFloat1.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byFloat1"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<float, LabyrinthSectionLayoutDat>> GetManyToManyByFloat1(IReadOnlyList<float>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<float, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<float, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByFloat1(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<float, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthNodeOverridesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="item">returned item if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetByLabyrinthNodeOverridesKeys(int? key, out LabyrinthSectionLayoutDat? item)
    {
        if (key is null)
        {
            item = null;
            return false;
        }

        if (!TryGetManyByLabyrinthNodeOverridesKeys(key, out var items))
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
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.LabyrinthNodeOverridesKeys"/> equal to a given key.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="items">returned items if found.</param>
    /// <returns>true if item with a given key was found, false otherwise.</returns>
    public bool TryGetManyByLabyrinthNodeOverridesKeys(int? key, out IReadOnlyList<LabyrinthSectionLayoutDat> items)
    {
        if (key is null)
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        if (byLabyrinthNodeOverridesKeys is null)
        {
            byLabyrinthNodeOverridesKeys = new();
            foreach (var item in Items)
            {
                var itemKey = item.LabyrinthNodeOverridesKeys;
                foreach (var listKey in itemKey)
                {
                    if (!byLabyrinthNodeOverridesKeys.TryGetValue(listKey, out var list))
                    {
                        list = new();
                        byLabyrinthNodeOverridesKeys.TryAdd(listKey, list);
                    }

                    list.Add(item);
                }
            }
        }

        if (!byLabyrinthNodeOverridesKeys.TryGetValue(key.Value, out var temp))
        {
            items = Array.Empty<LabyrinthSectionLayoutDat>();
            return false;
        }

        items = temp;
        return true;
    }

    /// <summary>
    /// Tries to get <see cref="LabyrinthSectionLayoutDat"/> with <see cref="LabyrinthSectionLayoutDat.byLabyrinthNodeOverridesKeys"/> equal to a given keys.
    /// </summary>
    /// <param name="keys">keys.</param>
    /// <returns>found items.</returns>
    public IReadOnlyList<ResultItem<int, LabyrinthSectionLayoutDat>> GetManyToManyByLabyrinthNodeOverridesKeys(IReadOnlyList<int>? keys)
    {
        if (keys is null || keys.Count == 0)
        {
            return Array.Empty<ResultItem<int, LabyrinthSectionLayoutDat>>();
        }

        var items = new List<ResultItem<int, LabyrinthSectionLayoutDat>>();

        foreach (var key in keys)
        {
            if (!TryGetManyByLabyrinthNodeOverridesKeys(key, out var tempItems))
            {
                continue;
            }

            foreach (var item in tempItems)
            {
                var resultItem = new ResultItem<int, LabyrinthSectionLayoutDat>(key, item);
                items.Add(resultItem);
            }
        }

        return items;
    }

    private LabyrinthSectionLayoutDat[] Load()
    {
        const string filePath = "Data/LabyrinthSectionLayout.dat64";
        var dataLoader = specification.DataLoader;
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthSectionLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading LabyrinthSectionKey
            (var labyrinthsectionkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LabyrinthSectionLayoutKeys
            (var templabyrinthsectionlayoutkeysLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthsectionlayoutkeysLoading = templabyrinthsectionlayoutkeysLoading.AsReadOnly();

            // loading LabyrinthSecretsKey0
            (var labyrinthsecretskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LabyrinthSecretsKey1
            (var labyrinthsecretskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading LabyrinthAreasKey
            (var labyrinthareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Float0
            (var float0Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Float1
            (var float1Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading LabyrinthNodeOverridesKeys
            (var templabyrinthnodeoverrideskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var labyrinthnodeoverrideskeysLoading = templabyrinthnodeoverrideskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthSectionLayoutDat()
            {
                LabyrinthSectionKey = labyrinthsectionkeyLoading,
                Unknown16 = unknown16Loading,
                LabyrinthSectionLayoutKeys = labyrinthsectionlayoutkeysLoading,
                LabyrinthSecretsKey0 = labyrinthsecretskey0Loading,
                LabyrinthSecretsKey1 = labyrinthsecretskey1Loading,
                LabyrinthAreasKey = labyrinthareaskeyLoading,
                Float0 = float0Loading,
                Float1 = float1Loading,
                LabyrinthNodeOverridesKeys = labyrinthnodeoverrideskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
